using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using Ocean.Wpf.Properties;

namespace Ocean.Wpf.Aero {
    /// <summary>
    /// Represents VistaAeroApi
    /// </summary>
    public class VistaAeroApi {

        #region  API Declarations

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("dwmapi.dll", CharSet = CharSet.Auto)]
        extern static void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MarginStruct pMargins);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("dwmapi.dll", CharSet = CharSet.Auto)]
        extern static void DwmIsCompositionEnabled(ref bool isIt);

        #endregion

        #region  Private Declarations

        /// <summary>
        /// Margin Struct
        /// </summary>
        public struct MarginStruct {

            /// <summary>
            /// Gets and sets Left
            /// </summary>
            public Int32 Left;
            /// <summary>
            /// Gets and sets Right
            /// </summary>
            public Int32 Right;
            /// <summary>
            /// Gets and sets Top
            /// </summary>
            public Int32 Top;
            /// <summary>
            /// Gets and sets Bottom
            /// </summary>
            public Int32 Bottom;

            /// <summary>
            /// Initializes a new instance of the <see cref="MarginStruct"/> struct.
            /// </summary>
            /// <param name="left">The left.</param>
            /// <param name="right">The right.</param>
            /// <param name="top">The top.</param>
            /// <param name="bottom">The bottom.</param>
            public MarginStruct(Int32 left, Int32 right, Int32 top, Int32 bottom) {
                this.Left = left;
                this.Right = right;
                this.Top = top;
                this.Bottom = bottom;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="MarginStruct"/> struct.
            /// </summary>
            /// <param name="t">The t.</param>
            public MarginStruct(Thickness t) {
                this.Left = Convert.ToInt32(t.Left);
                this.Right = Convert.ToInt32(t.Right);
                this.Top = Convert.ToInt32(t.Top);
                this.Bottom = Convert.ToInt32(t.Bottom);
            }
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Makes any WPF window a full Aero Glass window
        /// </summary>
        /// <param name="window">Pass the window object.  (Me)</param>
        /// <returns>Boolan</returns>
        /// <remarks>Very cool</remarks>
        public static Boolean ExtendGlassFrame(Window window) {
            return ExtendGlassFrame(window, new Thickness(-1));
        }

        /// <summary>
        /// Makes any WPF window an Aero Glass window
        /// </summary>
        /// <param name="window">Pass the window object.  (Me)</param>
        /// <param name="margin">Set margins to extend the glass into or to make the window all glass pass : New Thickness(-1)</param>
        /// <returns>Boolan</returns>
        /// <remarks>Very cool</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static Boolean ExtendGlassFrame(Window window, Thickness margin) {

            if(!(IsDwmIsCompositionEnabled())) {
                return false;
            }

            IntPtr hwnd = new WindowInteropHelper(window).Handle;

            if(hwnd == IntPtr.Zero) {
                throw new InvalidOperationException(Resources.VistaAeroAPI_ExtendGlassFrame_The_Window_must_be_shown_before_extending_glass_);
            }

            window.Background = Brushes.Transparent;
// ReSharper disable PossibleNullReferenceException
            HwndSource.FromHwnd(hwnd).CompositionTarget.BackgroundColor = Colors.Transparent;
// ReSharper restore PossibleNullReferenceException

            var margins = new MarginStruct(margin);
            DwmExtendFrameIntoClientArea(hwnd, ref margins);
            return true;
        }

        /// <summary>
        /// Checks if DWM Composition is enabled.
        /// </summary>
        public static bool IsDwmIsCompositionEnabled() {

            Boolean bolResult = false;
            DwmIsCompositionEnabled(ref bolResult);
            return bolResult;
        }

        #endregion
    }
}
