using System;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using Wpf.Common.Data;
using Wpf.Common.Events;
using Wpf.Common.Infrastructure;
using Wpf.Common.Model;

namespace Wpf.Presentation.Views {

   public class NavigationViewModel : ObservableObject {

       Lessons _lessons;
       readonly IRegionManager _regionManager;
       readonly IEventResolver<LessonSelectedEvent> _lessonSelectedResolver;

       ICommand _lessonSelectedCommand;
       
       public ICommand LessonSelectedCommand {
           get { return _lessonSelectedCommand ?? (_lessonSelectedCommand = new RelayCommand<Lesson>(LessonSelectedExecute)); }
       }

       public Lessons Lessons {
           get { return _lessons; }
           set {
               _lessons = value;
               RaisePropertyChanged("Lessons");
           }
       }

       public NavigationViewModel(
            Lessons lessons, 
            IRegionManager regionManager, 
            IEventResolver<LessonSelectedEvent> lessonSelectedResolver) {
           
           if (lessons == null) throw new ArgumentNullException("lessons");
           if (lessons.Count == 0) throw new InvalidOperationException("lessons collection was empty.");
           if (regionManager == null) throw new ArgumentNullException("regionManager");
           if (lessonSelectedResolver == null) throw new ArgumentNullException("lessonSelectedResolver");
           
           _lessons = lessons;
           _regionManager = regionManager;
           _lessonSelectedResolver = lessonSelectedResolver;

           // navigate to the inital view, HomeView
           LessonSelectedExecute(Lessons[0]);
       }

       void LessonSelectedExecute(Lesson selectedLesson) {
           if (selectedLesson == null) throw new ArgumentNullException("selectedLesson");

           // ************************************************************
           // this block of code is one way to work around the fact that
           // the button that was clicked ate the mouse click, preventing
           // the ListBox from getting it.
           foreach (var lesson in Lessons.Where(l => l.IsSelected)) {
               lesson.IsSelected = false;
           }
           selectedLesson.IsSelected = true;
           // ************************************************************

           // navigate to the selected lesson
           // if sucessful set the title, otherwise, display an error message
           _regionManager.Regions[Constants.ContentRegionName].RequestNavigate(selectedLesson.View,
               result => {
                   if (result.Error == null) {
                       _lessonSelectedResolver.Resolve().Publish(selectedLesson.Title);
                   } else {
                       _lessonSelectedResolver.Resolve().Publish(result.Error.Message);
                   }
               });
       }
   }
}
