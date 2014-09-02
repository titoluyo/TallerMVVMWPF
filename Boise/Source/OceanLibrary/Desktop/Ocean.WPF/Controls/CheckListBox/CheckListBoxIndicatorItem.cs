
using System;
using System.Windows.Controls;

namespace Ocean.Wpf.Controls {

    /// <summary>
    /// Represents CheckListBoxIndicatorItem
    /// </summary>
    public class CheckListBoxIndicatorItem {

        #region  Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsSelected { get; set; }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>The offset.</value>
        public Double Offset { get; set; }

        /// <summary>
        /// Gets or sets the related list box item.
        /// </summary>
        /// <value>The related list box item.</value>
        public ListBoxItem RelatedListBoxItem { get; set; }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckListBoxIndicatorItem"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="isSelected">if set to <c>true</c> [is selected].</param>
        /// <param name="relatedListBoxItem">The related list box item.</param>
        public CheckListBoxIndicatorItem(Double offset, Boolean isSelected, ListBoxItem relatedListBoxItem) {
            this.Offset = offset;
            this.IsSelected = isSelected;
            this.RelatedListBoxItem = relatedListBoxItem;
        }

        #endregion
    }
}

