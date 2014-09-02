using System.Collections.ObjectModel;
using System.Linq;
using Wpf.DataBinding.Model;

namespace Wpf.DataBinding.Data {

    public static class PeopleService {

        static readonly ObservableCollection<Person> _people;

        public static ObservableCollection<Person> People {
            get { return _people; }
        }

        public static Person Person {
            get { return People.FirstOrDefault(); }
        }

        static PeopleService() {
            _people = new ObservableCollection<Person> {
                new Person {Birthday = new System.DateTime(1960, 11, 1), FavoriteChairThumbnail = "/Wpf.DataBinding;component/Resources/image01.png", FirstName = "Aaberg", LastName = "Jesper", IsActive = true, Profession = "Developer"}, 
                new Person {Birthday = new System.DateTime(1980, 1, 11), FavoriteChairThumbnail = "/Wpf.DataBinding;component/Resources/image02.png", FirstName = "Ellen", LastName = "Adams", IsActive = false, Profession = "Designer"}, 
                new Person {Birthday = new System.DateTime(1984, 1, 11), FavoriteChairThumbnail = "/Wpf.DataBinding;component/Resources/image03.png", FirstName = "Lori", LastName = "Penor", IsActive = true, Profession = "Designer"}
            };
        }
    }
}
