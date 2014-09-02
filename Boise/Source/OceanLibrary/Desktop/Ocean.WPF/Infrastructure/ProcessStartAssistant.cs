using System;

namespace Ocean.Wpf.Infrastructure {

    /// <summary>
    /// Represents ProcessStartAssistant
    /// </summary>
    public static class ProcessStartAssistant {

        /// <summary>
        /// Initializes the <see cref="ProcessStartAssistant"/> class.
        /// </summary>
        static ProcessStartAssistant() { }

        /// <summary>
        /// Starts a process using a file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static void StartProcessWithFileName(String fileName) {
            try {
                var psi = new System.Diagnostics.ProcessStartInfo { FileName = fileName, UseShellExecute = true };
                System.Diagnostics.Process.Start(psi);
                // ReSharper disable EmptyGeneralCatchClause
            } catch {
                // ReSharper restore EmptyGeneralCatchClause
            }
        }
    }
}
