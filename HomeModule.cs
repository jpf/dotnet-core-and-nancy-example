
using Nancy;
namespace NancyApplication
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", args => "Hello World. This is pretty cool!");
            Get("/test/{name}", args => new Person() { Name = args.name });
        }
    }

    public class Person
    {
        public string Name { get; set; }
    }
}
