using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Factory
{
    internal class Pizza : Food
    {
        public bool IsSpicy { get; set; }
        public bool IsItalian { get; set; }

        public static Food Prepare()
        {
            return new Pizza
            {
                Name = "Italian Pizza",
                Price = 200,
                IsItalian = true,
                IsSpicy = true
            };
        }
    }
}
