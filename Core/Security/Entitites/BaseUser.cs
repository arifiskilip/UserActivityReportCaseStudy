﻿using Core.Domain;

namespace Core.Security.Entitites
{
    public class BaseUser : Entity<int>
    {

        public string? FirstName
        {
            get; set;
        }

        public string? LastName
        {
            get; set;
        }

        public string? Email
        {
            get; set;
        }


        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
