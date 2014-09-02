using System;
using System.Windows.Forms;
using Ocean.VistaBridgeLibrary.Dialog;
using Ocean.Wpf.Properties;

namespace Ocean.Wpf.CommonDialog {

    /// <summary>
    /// Represents SaveFileDialog
    /// </summary>
    public class SaveFileDialog {

        #region  Declarations

        Boolean _addExtension = true;
        Boolean _checkFileExists = true;
        Boolean _checkPathExists = true;
        String _defaultExtension = String.Empty;
        Boolean _overWritePrompt = true;
        Boolean _supportMultiDottedExtensions = true;
        Boolean _validateNames = true;
        String _fileName = String.Empty;
        String _filter = String.Empty;
        String _initialDirectory = String.Empty;
        String _title = Resources.SaveFileDialog__title_Select_File_To_Save;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box automatically adds an extension to a file name if the user omits the extension. The default is true.
        /// </summary>
        public Boolean AddExtension {
            get {
                return _addExtension;
            }
            set {
                _addExtension = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a file dialog displays a warning if the user specifies a filename that does not exist. The default is true.
        /// </summary>
        public Boolean CheckFileExists {
            get {
                return _checkFileExists;
            }
            set {
                _checkFileExists = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether warnings are displayed if the user types invalid paths and filenames. The default is true.
        /// </summary>
        public Boolean CheckPathExists {
            get {
                return _checkPathExists;
            }
            set {
                _checkPathExists = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box prompts the user for permission to create a file if the user specifies a file that does not exist. The default is false.
        /// </summary>
        public bool CreatePrompt { get; set; }

        /// <summary>
        /// Gets or sets the default file name extension. 
        /// </summary>
        public String DefaultExtension {
            get {
                return _defaultExtension;
            }
            set {
                _defaultExtension = value;
            }
        }

        /// <summary>
        /// Gets a String that is the full path of the file selected in the file dialog. The default is Empty. 
        /// </summary>
        public String FileName {
            get {
                return _fileName;
            }
        }

        /// <summary>
        /// Gets or sets the current filename filter String, which determines the choices that appear in the "Save as file type" box at the bottom of a file dialog.  Filter String consists of one or more pairs of String values, all separated by the pipe symbol ( | ).  Example : "Saved Report PDF Files(*.pdf)|*.pdf|All Files (*.*)|*.*"
        /// </summary>
        public String Filter {
            get {
                return _filter;
            }
            set {
                _filter = value;
            }
        }

        /// <summary>
        /// Gets or sets the initial directory displayed by the file dialog box. 
        /// </summary>
        public String InitialDirectory {
            get {
                return _initialDirectory;
            }
            set {
                _initialDirectory = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Save As dialog box displays a warning if the user specifies a file name that already exists. The default is true.
        /// </summary>
        public Boolean OverWritePrompt {
            get {
                return _overWritePrompt;
            }
            set {
                _overWritePrompt = value;
            }
        }

        /// <summary>
        /// Gets or sets whether the dialog box supports displaying and saving files that have multiple file name extensions.  The default is true.
        /// </summary>
        public Boolean SupportMultiDottedExtensions {
            get {
                return _supportMultiDottedExtensions;
            }
            set {
                _supportMultiDottedExtensions = value;
            }
        }

        /// <summary>
        /// Gets or sets the text that appears in the title bar of a file dialog. The default value is "Select File To Save"
        /// </summary>
        public String Title {
            get {
                return _title;
            }
            set {
                _title = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog accepts only valid Win32 filenames. The default values is true.
        /// </summary>
        public Boolean ValidateNames {
            get {
                return _validateNames;
            }
            set {
                _validateNames = value;
            }
        }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveFileDialog"/> class.
        /// </summary>
        public SaveFileDialog() {
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Displays the save file dialog for the current operating system.  Returns true if the user selected or entered a file name.  Read the FileName property for the selected file.
        /// </summary>
        public Boolean ShowDialog() {

            using(var dialogSaveFile = new VistaSaveFileDialog()) {
                dialogSaveFile.AddExtension = this.AddExtension;
                dialogSaveFile.CheckFileExists = this.CheckFileExists;
                dialogSaveFile.CheckPathExists = this.CheckPathExists;
                dialogSaveFile.CreatePrompt = this.CreatePrompt;
                dialogSaveFile.DefaultExt = this.DefaultExtension;
                dialogSaveFile.Filter = this.Filter;
                dialogSaveFile.InitialDirectory = this.InitialDirectory;
                dialogSaveFile.OverwritePrompt = this.OverWritePrompt;
                dialogSaveFile.ShowHelp = false;
                dialogSaveFile.SupportMultiDottedExtensions = this.SupportMultiDottedExtensions;
                dialogSaveFile.Title = this.Title;
                dialogSaveFile.ValidateNames = this.ValidateNames;

                DialogResult result = dialogSaveFile.ShowDialog();

                if(result == DialogResult.OK) {
                    _fileName = dialogSaveFile.FileName;
                    return true;
                }
                //user cancelled
                return false;
            }
        }

        #endregion
    }
}