// Copyright (c) Sven Groot (Ookii.org) 2006
// See license.txt for details
using System;

namespace Ocean.VistaBridgeLibrary.Interop
{
    static class ComDlgResources
    {
        public enum ComDlgResourceId
        {
            OpenButton = 370,
            Open = 384,
            FileNotFound = 391,
            CreatePrompt = 402,
            ReadOnly = 427,
            ConfirmSaveAs = 435
        }

        static readonly Win32Resources _Resources = new Win32Resources("comdlg32.dll");

        public static String LoadString(ComDlgResourceId id)
        {
            return _Resources.LoadString((uint)id);
        }

        public static String FormatString(ComDlgResourceId id, params string[] args)
        {
            return _Resources.FormatString((uint)id, args);
        }
    }
}
