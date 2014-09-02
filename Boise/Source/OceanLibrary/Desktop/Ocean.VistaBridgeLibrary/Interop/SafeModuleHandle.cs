using System;

namespace Ocean.VistaBridgeLibrary.Interop
{
    class SafeModuleHandle : System.Runtime.InteropServices.SafeHandle
    {
        public SafeModuleHandle()
            : base(IntPtr.Zero, true)
        {
        }

        public override bool IsInvalid
        {
            get { return handle == IntPtr.Zero; }
        }

        protected override bool ReleaseHandle()
        {
            return NativeMethods.FreeLibrary(handle);
        }
    }
}
