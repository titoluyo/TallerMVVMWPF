using System;
using Wpf.Common.Infrastructure;

namespace Wpf.Common.Model {

    public class Lesson : ObservableObject {

        public String Section { get; set; }
        public String Title { get; set; }
        public String View { get; set; }
        Boolean _isSelected;
        
        public Boolean IsSelected {
            get { return _isSelected; }
            set {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }
	
        public Lesson() {
        }
    }
}
