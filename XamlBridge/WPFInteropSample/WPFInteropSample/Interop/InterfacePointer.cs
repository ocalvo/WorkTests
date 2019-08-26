using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Interop
{
    public abstract class InterfacePointer : IDisposable
    {
        protected IntPtr _ptr;
        private Dictionary<int, object> _virtualFunctionDelegates = new Dictionary<int, object>();

        public InterfacePointer(IntPtr ptr)
        {
            _ptr = ptr;
        }

        public IntPtr Value
        {
            get
            {
                return _ptr;
            }
        }

        public void Dispose()
        {
            OnDispose();
        }

        protected abstract void OnDispose();

        protected TDelegate GetDelegateForVirtualFunction<TDelegate>(int virtualFunctionIndex)
        {
            // check cache
            if (_virtualFunctionDelegates.ContainsKey(virtualFunctionIndex))
            {
                return (TDelegate)_virtualFunctionDelegates[virtualFunctionIndex];
            }

            // Dereference the interface pointer to get a pointer to the start of the v-table.
            IntPtr vtable = Marshal.ReadIntPtr(_ptr);

            // Get the v-ptr corresponding to the (0-based) v-table slot index passed in
            IntPtr vptr = vtable + (virtualFunctionIndex * Marshal.SizeOf<IntPtr>());

            // Dereference the v-ptr to get the address of the function itself
            IntPtr functionPtr = Marshal.ReadIntPtr(vptr);

            // Convert to a delegate of the specified type
            TDelegate result = Marshal.GetDelegateForFunctionPointer<TDelegate>(functionPtr);

            // cache
            _virtualFunctionDelegates[virtualFunctionIndex] = result;

            return result;
        }
    }
}
