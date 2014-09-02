using System;
using System.Windows.Input;
using CookMe.Common.Infrastructure;

namespace CookMe.Recipe.Views {
   public class SearchViewModel : ObservableObject{

       Boolean _isFavoritesSelected;
       private ICommand _addCommand;
       
       public ICommand AddCommand {
           get { return _addCommand ?? (_addCommand = new RelayCommand(AddExecute)); }
       }
	
       public Boolean IsFavoritesSelected {
           get { return _isFavoritesSelected; }
           set {
               _isFavoritesSelected = value;
               RaisePropertyChanged("IsFavoritesSelected");
           }
       }
	
 
       public SearchViewModel() {


       }

       void AddExecute() {
           
       }
    }
}
