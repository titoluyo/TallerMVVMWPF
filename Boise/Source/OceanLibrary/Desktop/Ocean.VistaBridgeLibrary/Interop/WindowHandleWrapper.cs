// Copyright (c) Sven Groot (Ookii.org) 2006
// See license.txt for details
using System;
using System.Windows.Forms;

namespace Ocean.VistaBridgeLibrary.Interop
{
    class WindowHandleWrapper : IWin32Window
    {
        private IntPtr _handle;

        public WindowHandleWrapper(IntPtr handle)
        {
            _handle = handle;
        }

        #region IWin32Window Members

        public IntPtr Handle
        {
            get { return _handle; }
        }

        #endregion
    }
}
