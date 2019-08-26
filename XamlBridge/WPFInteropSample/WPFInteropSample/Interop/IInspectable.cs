using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Interop
{
    public class IInspectable : InterfacePointer
    {
        private delegate UInt32 Fn_IInspectable_QueryInterface(IntPtr thisPtr, [In] ref Guid iid, out IntPtr pvObject);
        private delegate UInt32 Fn_IInspectable_AddRef(IntPtr thisPtr);
        private delegate UInt32 Fn_IInspectable_Release(IntPtr thisPtr);
        private delegate UInt32 Fn_IInspectable_GetIids(IntPtr thisPtr, out ulong iidCount, out IntPtr iids);
        private delegate UInt32 Fn_IInspectable_GetRuntimeClassName(IntPtr thisPtr, out IntPtr className);
        private delegate UInt32 Fn_IInspectable_GetTrustLevel(IntPtr thisPtr, out int trustLevel);

        private Fn_IInspectable_QueryInterface IInspectable_QueryInterface { get { return GetDelegateForVirtualFunction<Fn_IInspectable_QueryInterface>(0); } }
        private Fn_IInspectable_AddRef IInspectable_AddRef { get { return GetDelegateForVirtualFunction<Fn_IInspectable_AddRef>(1); } }
        private Fn_IInspectable_Release IInspectable_Release { get { return GetDelegateForVirtualFunction<Fn_IInspectable_Release>(2); } }
        private Fn_IInspectable_GetIids IInspectable_GetIids { get { return GetDelegateForVirtualFunction<Fn_IInspectable_GetIids>(3); } }
        private Fn_IInspectable_GetRuntimeClassName IInspectable_GetRuntimeClassName { get { return GetDelegateForVirtualFunction<Fn_IInspectable_GetRuntimeClassName>(4); } }
        private Fn_IInspectable_GetTrustLevel IInspectable_GetTrustLevel { get { return GetDelegateForVirtualFunction<Fn_IInspectable_GetTrustLevel>(5); } }

        public IInspectable(IntPtr ptr) : base(ptr)
        {
        }

        public IntPtr QueryInterface(Guid iid)
        {
            IntPtr pvObject;
            UInt32 hr = IInspectable_QueryInterface(_ptr, ref iid, out pvObject);

            if (0x80004002 == hr) // E_NOINTERFACE
            {
                return IntPtr.Zero;
            }

            if (0 != hr)
            {
                throw new Exception(String.Format("IInspectable::QueryInterface({0}) failed with 0x{1:X}", iid, hr));
            }

            return pvObject;
        }

        protected override void OnDispose()
        {
            IInspectable_Release(_ptr);
        }
    }
}
