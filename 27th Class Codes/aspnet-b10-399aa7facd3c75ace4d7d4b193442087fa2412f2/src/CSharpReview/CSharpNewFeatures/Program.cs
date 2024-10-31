using CSharpNewFeatures;

double Longitude = 3.33333;
int Latitude = 5;

var location = $"""
   You are at {Longitude}, {Latitude}
   """;

Console.WriteLine(location);

Person person = new Person() { FirstName = "Jalal", LastName = "Uddin"  };

Product product = new Product(200, 10);
Product p2 = new Product();

