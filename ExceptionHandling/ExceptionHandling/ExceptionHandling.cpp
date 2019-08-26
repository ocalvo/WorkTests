// ExceptionHandling.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#define ARRAY_SIZE(x) (sizeof(x) / sizeof(x[0]))

// INSTRUCTION_ADDRESS represents frame addresses in a stack trace.
typedef void* INSTRUCTION_ADDRESS;

extern "C" {
    // Forward declaration of PSTOWED_EXCEPTION_INFORMATION_V2. This is needed
    // by GetStowedExceptionsForFailFast().
    typedef struct _STOWED_EXCEPTION_INFORMATION_V2 *PSTOWED_EXCEPTION_INFORMATION_V2;

    // Declare the _ReturnAddress intrinsic.
    void * _ReturnAddress(void);
}

struct CBar
{
    int m = 4;
};

class CFoo
{
    CBar* p = nullptr;
public:
    CFoo()
    {
        p = new CBar();
    }
    ~CFoo()
    {
        Reset();;
    }

    void Reset()
    {
        delete p;
        p = nullptr;
    }

};

__declspec(noinline) INSTRUCTION_ADDRESS GetCallerReturnAddressFromDirectCaller(INSTRUCTION_ADDRESS directCallerReturnAddress)
{
    // Default to directCallerReturnAddress, which would be wrong, but at least would let an error context get generated anyway.
    // We expect to find directCallerReturnAddress in the current stack backtrace, so this default will rarely if ever stand.
    INSTRUCTION_ADDRESS callerReturnAddress = directCallerReturnAddress;

    // A total of 5 return addresses are needed max.  The return addresses will point to an instruction within each of the
    // functions in this list, max:
    //   * this function
    //   * the On[New]FailureEncountered
    //   * the UniqueError::On[New]Failure (optional frame)
    //   * the "caller" of the IFC() macro
    //   * the caller of the "caller" of the IFC() macro (this is the one we actually want)
    // Some may be missing due to unpredictable inlining however (and whether UniqueError::On[New]Failure is used).

    // Finding the callerReturnAddress this way makes the module notably smaller at the cost of just a little speed when errors
    // are propagating up a stack that has a series of IFC() macros on the way up.

    INSTRUCTION_ADDRESS returnAddresses[5];
    USHORT count;
    count = CaptureStackBackTrace(
        0, // skip 0 frames, since inlining can be unpredictable.
        ARRAY_SIZE(returnAddresses),
        returnAddresses,
        NULL);
    if (count != 0)
    {
        for (USHORT iter = 0; iter < count - 1; iter++)
        {
            if (returnAddresses[iter] == directCallerReturnAddress)
            {
                callerReturnAddress = returnAddresses[iter + 1];
            }
        }
    }

    return callerReturnAddress;
}

extern "C"
__declspec(noinline) bool OnFailureEncountered(HRESULT failedFrameHR, INSTRUCTION_ADDRESS directCallerReturnAddress, _In_opt_ CONTEXT* contextRecord)
{
    if (directCallerReturnAddress == NULL)
    {
        directCallerReturnAddress = _ReturnAddress();
    }

    INSTRUCTION_ADDRESS callerReturnAddress = GetCallerReturnAddressFromDirectCaller(directCallerReturnAddress);

    {
        CONTEXT context0;
        RtlCaptureContext(&context0);
        RtlUnwindEx(directCallerReturnAddress, callerReturnAddress, nullptr, nullptr, contextRecord, nullptr);
    }

    //RtlUnwind(directCallerReturnAddress /*targetFrame*/, callerReturnAddress /*targetIp*/, nullptr /*exceptionRecord*/, nullptr /*returnValue*/);
    RaiseException(1, 0, 0, nullptr);

    return true;
}

__declspec(noinline)
static bool OnFailure(HRESULT failedFrameHR)
{
    CONTEXT context;
    RtlCaptureContext(&context);
    return OnFailureEncountered(failedFrameHR, _ReturnAddress(), &context);
}

int i = 0;

void goo()
{
    if (i++ > 2)
    {
        throw 6;
        OnFailure(E_FAIL);
    }
}

void foo()
{
    CFoo foo;
    try
    {
        while (true)
        {
            goo();
        }
    }
    catch (...)
    {

    }
}

int main()
{
    CONTEXT context0;
    RtlCaptureContext(&context0);
    __try
    {
        CONTEXT context1;
        RtlCaptureContext(&context1);

        foo();
        return 0;
    }
    __except (1)
    {
        //RtlUnwind(nullptr /*targetFrame*/, nullptr /*targetIp*/, nullptr /*exceptionRecord*/, nullptr /*returnValue*/);
        return 7;
    }
}
