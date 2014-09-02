using System;
using Wpf.Common.Events;
using Wpf.Common.Infrastructure;

namespace Wpf.Demonstrations {

    public class ShellViewModel : ObservableObject {

        String _lessonTitle;
        
        public String LessonTitle {
            get { return _lessonTitle; }
            private set {
                _lessonTitle = value;
                RaisePropertyChanged("LessonTitle");
            }
        }
	
        public ShellViewModel(IEventResolver<LessonSelectedEvent> lessonSelectedResolver) {
            if (lessonSelectedResolver == null) throw new ArgumentNullException("lessonSelectedResolver");
            lessonSelectedResolver.Resolve().Subscribe(t => this.LessonTitle = t);
        }
    }
}
