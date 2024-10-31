using DesignPattern.AbstractFactory;
using DesignPattern.Builder;
using DesignPattern.Prototype;
using DesignPattern.Singleton;

Logger logger1 = Logger.Instance;
Logger logger2 = Logger.Instance;

if(logger1 == logger2)
    Console.WriteLine("Same");


ConnectionStringBuilder connectionStringBuilder = new ConnectionStringBuilder("Localhost");
connectionStringBuilder.AddUsernameAndPassword("demo", "123456");
connectionStringBuilder.AddTrustedConnection();
ConnectionString connectionString = connectionStringBuilder.GetConnectionString();

Console.WriteLine(connectionString.ConnectionStringItem.ToString());


Product p = new Product { Name = "Camera", Color = "Black", Description = "Cannon", Price = 30000 };
Product p2 = p.Copy();



FighterJetFactory factory = new MigFactory();
FighterJet jet1 = factory.GetJet();
Weapon weapon1 = factory.GetWeapon();


FighterJetFactory factory2 = new F16Factory();
FighterJet jet2 = factory2.GetJet();
Weapon weapon2 = factory2.GetWeapon();