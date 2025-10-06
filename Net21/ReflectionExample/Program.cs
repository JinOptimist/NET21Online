using ReflectionExample;
using System.Reflection;

var ivan = new User();

ivan.Id = 1;
ivan.Name = "Ivan";
// ivan._birthday = DateTime.Now;

// Console.WriteLine(ivan.IsAdult);

var type = ivan.GetType();

Console.WriteLine($"****** GetProperties ********* ");
var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
foreach (var propertiy in properties)
{
    Console.WriteLine($"public {propertiy.PropertyType} {propertiy.Name}");
}

Console.WriteLine($"****** GetFields ********* ");
var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
foreach (var field in fields)
{
    Console.WriteLine($"private {field.FieldType} {field.Name}");
}

Console.WriteLine($"****** GetMethods Private ********* ");
var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
foreach (var method in methods)
{
    Console.WriteLine($"private {method.ReturnType} {method.Name}");
}

Console.WriteLine($"****** GetMethods Public ********* ");
var methodsPublic = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
foreach (var method in methodsPublic)
{
    Console.WriteLine($"public {method.ReturnType} {method.Name}");
}

var filed = type.GetField("_birthday");
filed.SetValue(ivan, DateTime.Now.AddDays(1));
filed.GetValue(ivan);

Console.ReadLine();