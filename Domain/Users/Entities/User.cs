using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace finance_api.Domain.Users.Entities
{
    public class User : IdentityUser<int>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }

        public User(string firstName, string lastName, string email)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            CreationDate = DateTime.Now;
            UserName = email;
            Email = email;
            SecurityStamp = Guid.NewGuid().ToString();
        }

        public void SetFirstName(string firstName)
        {
            FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(firstName);
        }

        public void SetLastName(string lastName)
        {
            LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lastName);
        }
    }
}
