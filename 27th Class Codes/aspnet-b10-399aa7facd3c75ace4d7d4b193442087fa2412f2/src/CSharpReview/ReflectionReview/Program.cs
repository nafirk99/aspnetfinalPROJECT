using ReflectionReview;
using System.Reflection;

var path = "C:\\Training\\AspDotNet\\aspnet-b10\\src\\CSharpReview\\DemoLib\\bin\\Debug\\net8.0\\DemoLib.dll";
Assembly a = Assembly.LoadFile(path);

Type[] t2 = a.GetTypes();
for (int i = 0; i < t2.Length; i++)
    Console.WriteLine(t2[i].Name);

foreach(var item in a.DefinedTypes) Console.WriteLine(item.Name);

Type t = typeof(List<int>);

List<int> ints = new List<int>();
Type t3 = ints.GetType();

MethodInfo[] x = t3.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
Console.WriteLine("-----------------------");
foreach (var x2 in x)
{
    Console.Write(x2.Name);
    Console.Write(" (");
    ParameterInfo[] parameters = x2.GetParameters();
    foreach (var p in parameters)
    {
        Console.Write(p.Name);
        Console.Write(",");
    }
    Console.WriteLine(")");
}
Console.WriteLine("-----------------------");
//0 1 1
//1 0 1

Fruits f = Fruits.Banana | Fruits.Mango | Fruits.Apple;
Console.WriteLine((int)f);

foreach(var g in t3.GetGenericArguments())
    Console.WriteLine(g.Name);

foreach (var i in t3.GetInterfaces())
{
    Console.WriteLine(i.Name);

    foreach(var v in i.GetGenericArguments())
        Console.WriteLine(v.Name);
}

foreach(var product in t2)
{
    if(product.Name == "Product")
    {
        Console.WriteLine(product.BaseType.Name);

        double price = 2000;
        double discount = 15;

        ConstructorInfo constructor = product.GetConstructor(new Type[] { });
        object p = constructor.Invoke(new object[] { });
        MethodInfo method = product.GetMethod("GetDiscount", new Type[] { typeof(double), typeof(double) });
        object result = method.Invoke(p, new object[] { price, discount });
        Console.WriteLine(result);
    }
}

