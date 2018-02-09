using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankSystem.Models
{
    public class Credit
    {
        public int Id { get; set; }
        public double Percent { get; set; }
        public int NumberOfMonths { get; set; }
        public int AmmountOfMoney { get; set; }
    }
}