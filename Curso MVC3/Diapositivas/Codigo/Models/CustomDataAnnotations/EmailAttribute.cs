using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.CustomDataAnnotations
{
    using System.ComponentModel.DataAnnotations;

    public class EmailAttribute : RegularExpressionAttribute
    {
        private const string EmailPattern =@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*
                            @([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
        public EmailAttribute(): base(EmailPattern){}
    }
}