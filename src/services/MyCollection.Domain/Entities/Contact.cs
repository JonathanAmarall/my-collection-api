using MyCollection.Core.Models;

namespace MyCollection.Domain.Entities
{
    public class Contact : EntityBase
    {
        public Contact(string fullName, string email, string phone)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;

        }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
    }
}
