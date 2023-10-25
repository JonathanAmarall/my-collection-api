using MyCollection.Core.Models;

namespace MyCollection.Domain.Entities
{
    public class Borrower : EntityBase
    {
        protected Borrower() { }
        public Borrower(string firstName, string lastName, string email, string phone, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string FullName => $"{FirstName} {LastName}";

        public Address Address { get; private set; }
    }
}