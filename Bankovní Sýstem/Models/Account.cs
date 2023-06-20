using System;
using System.Collections.Generic;

#nullable disable

namespace Bankovní_Sýstem.Views.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public string Education { get; set; }
        public decimal Income { get; set; }
        public decimal Balance { get; set; }
    }
}
