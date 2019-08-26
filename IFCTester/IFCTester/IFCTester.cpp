// IFCTester.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>

class Bar
{
public:
    HRESULT Foo();
};

HRESULT Bar::Foo() try
{
    return E_FAIL;
}
catch (HRESULT hr) { return hr; }

int foo()
{
    int g = 5;
CleanUp:
    return g;
}

int main()
{
    foo();
    return 0;
}

