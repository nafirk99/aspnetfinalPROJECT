using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNewFeatures
{
    public class Product(int price, int discount)
    {
        private bool _status;
        public Product() : this(0, 0)
        {

        }

        public Product(int price, int discount, bool status) : this(price, discount)
        {
            _status = status;
        }
    }
}
