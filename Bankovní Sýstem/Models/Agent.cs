using System;
using System.Collections.Generic;

#nullable disable

namespace Bankovní_Sýstem.Views.Models
{
    public partial class Agent
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
