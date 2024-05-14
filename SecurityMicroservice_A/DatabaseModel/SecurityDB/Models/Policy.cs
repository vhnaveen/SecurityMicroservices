using System;
using System.Collections.Generic;

namespace SecurityMicroservice_A.DatabaseModel.SecurityDB.Models
{
    public partial class Policy
    {
        public Policy()
        {
            Scopes = new HashSet<Scope>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Scope> Scopes { get; set; }
    }
}
