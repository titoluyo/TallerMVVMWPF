using System;
using System.Windows.Forms;
using Ocean.VistaBridgeLibrary.Dialog;

namespace Ocean.Wpf.CommonDialog {

    /// <summary>
    /// Represents FolderBrowserDialog
    /// </summary>
    public class FolderBrowserDialog {

        #region  Declarations

        Boolean _showNewFolderButton = true;
        String _description = string.Empty;
        String _selectedPath = string.Empty;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the descriptive text displayed above the tree view control in the dialog box. 
        /// </summary>
        public String Description {
            get {
                return _description;
            }
            set {
                _description = value;
            }
        }

        /// <summary>
        /// Gets or sets the path selected by the user. 
        /// </summary>
        public String SelectedPath {
            get {
                return _selectedPath;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the New Folder button appears in the folder browser dialog box. 
        /// </summary>
        public Boolean ShowNewFolderButton {
            get {
                return _showNewFolderButton;
            }
            set {
                _showNewFolderButton = value;
            }
        }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderBrowserDialog"/> class.
        /// </summary>
        public FolderBrowserDialog() {
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Displays the folder browser dialog for the current operating system.  Returns true if the user selected a folder.  Read the SelectedPath property for the selected path.
        /// </summary>
        public bool ShowDialog() {

            using(var folderBrowserDialog = new VistaFolderBrowserDialog()) {
                folderBrowserDialog.Description = this.Description;
                folderBrowserDialog.ShowNewFolderButton = this.ShowNewFolderButton;
                folderBrowserDialog.UseDescriptionForTitle = false;

                DialogResult result = folderBrowserDialog.ShowDialog();

                if(result == DialogResult.OK) {
                    _selectedPath = folderBrowserDialog.SelectedPath;
                    return true;

                }
                return false;
            }
        }

        #endregion
    }
}