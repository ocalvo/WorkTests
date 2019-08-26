

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0622 */
/* at Mon Jan 18 19:14:07 2038
 */
/* Compiler settings for FocusMFC.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.01.0622 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */



/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 500
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif /* __RPCNDR_H_VERSION__ */


#ifndef __FocusMFC_h_h__
#define __FocusMFC_h_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IFocusMFC_FWD_DEFINED__
#define __IFocusMFC_FWD_DEFINED__
typedef interface IFocusMFC IFocusMFC;

#endif 	/* __IFocusMFC_FWD_DEFINED__ */


#ifndef __FocusMFC_FWD_DEFINED__
#define __FocusMFC_FWD_DEFINED__

#ifdef __cplusplus
typedef class FocusMFC FocusMFC;
#else
typedef struct FocusMFC FocusMFC;
#endif /* __cplusplus */

#endif 	/* __FocusMFC_FWD_DEFINED__ */


#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __FocusMFC_LIBRARY_DEFINED__
#define __FocusMFC_LIBRARY_DEFINED__

/* library FocusMFC */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_FocusMFC;

#ifndef __IFocusMFC_DISPINTERFACE_DEFINED__
#define __IFocusMFC_DISPINTERFACE_DEFINED__

/* dispinterface IFocusMFC */
/* [uuid] */ 


EXTERN_C const IID DIID_IFocusMFC;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("f392dfcc-a7e9-4d8f-aeef-8cb4177f8c03")
    IFocusMFC : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct IFocusMFCVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IFocusMFC * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IFocusMFC * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IFocusMFC * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IFocusMFC * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IFocusMFC * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IFocusMFC * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IFocusMFC * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        END_INTERFACE
    } IFocusMFCVtbl;

    interface IFocusMFC
    {
        CONST_VTBL struct IFocusMFCVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IFocusMFC_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IFocusMFC_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IFocusMFC_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IFocusMFC_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IFocusMFC_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IFocusMFC_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IFocusMFC_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* __IFocusMFC_DISPINTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_FocusMFC;

#ifdef __cplusplus

class DECLSPEC_UUID("2865d9ce-6fc3-4bea-9554-d4283a535a9c")
FocusMFC;
#endif
#endif /* __FocusMFC_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


