using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WpfModelValidation 
{
    class Person : ViewModelBase
    {
        [Required(ErrorMessage = "Cannot be null.")]
        [StringLength(30, ErrorMessage = "Name is required and less than 30 characters.")]
        public string PersonName
        {
            get { return GetValue(() => PersonName); }
            set { SetValue(() => PersonName, value); }
        }

        [Range(10, 50, ErrorMessage = "Age must between 10 and 50.")]
        public int Age
        {
            get { return GetValue(() => Age); }
            set { SetValue(() => Age, value); }
        }

        public override string ModelValidate()
        {
            if (PersonName == "Tom" && Age!=30)
            {
                return "Tom must be 30";
            }
            return null;
        }
    }
}
