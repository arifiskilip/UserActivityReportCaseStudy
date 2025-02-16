﻿using Core.Domain;

namespace Core.Security.Entitites
{
    public class OperationClaim : Entity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
