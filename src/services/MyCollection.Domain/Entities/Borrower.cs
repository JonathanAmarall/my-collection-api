﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyCollection.Core.Contracts;
using MyCollection.Core.Models;
using MyCollection.Domain.ValueObjects;

namespace MyCollection.Domain.Entities
{
    public class Borrower : EntityBase, IAuditableEntity
    {
        protected Borrower()
        {
        }

        public Borrower(string firstName, string lastName, Email email, string phone, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public string Phone { get; private set; }
        public string FullName => $"{FirstName} {LastName}";

        public Address Address { get; private set; }


        public CollectionItem CollectionItem { get; set; }

        public DateTime CreatedAt { get; }
        public DateTime? UpdateAt { get; }
    }
}