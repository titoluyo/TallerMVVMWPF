using System;

namespace WPFPrismv4Navigation.Infrastructure {

    public class LogItem {

        public Int32 Order { get; private set; }
        public String Scenario { get; private set; }
        public String TypeName { get; private set; }
        public String MethodName { get; private set; }
        public String Notes { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is first in group.
        /// This property is used by a DataTrigger to change the presentation of the row
        /// in the DataGrid.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is first in group; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsFirstInGroup { get; private set; }

        public LogItem(Int32 order, String scenario, String typeName, String methodName, String notes, Boolean isFirstInGroup) {
            this.Order = order;
            this.Scenario = scenario;
            this.TypeName = typeName;
            this.MethodName = methodName;
            this.Notes = notes;
            this.IsFirstInGroup = isFirstInGroup;
        }
    }
}
