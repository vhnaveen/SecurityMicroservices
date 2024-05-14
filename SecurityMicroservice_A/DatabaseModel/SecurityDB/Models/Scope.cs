using System;
using System.Collections.Generic;

namespace SecurityMicroservice_A.DatabaseModel.SecurityDB.Models
{
    public partial class Scope
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? PolicyId { get; set; }

        public virtual Policy? Policy { get; set; }
    }
}
