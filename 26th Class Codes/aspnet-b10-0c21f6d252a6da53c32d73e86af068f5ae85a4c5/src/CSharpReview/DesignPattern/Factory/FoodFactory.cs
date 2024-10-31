using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Factory
{
    internal class FoodFactory
    {
        public Food PrepareFood(string foodType)
        {
            if (foodType == "Burger")
                return Burger.Prepare();
            else
                return Pizza.Prepare();
        }
    }
}
