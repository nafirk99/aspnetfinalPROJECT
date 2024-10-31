namespace DemoLib
{
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public double GetDiscount(double price, double discount)
        {
            return price * discount / 100;
        }
    }
}
