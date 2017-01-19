- [.NET Core and Nancy example code](#org94c5fde)
- [Step 1: Install .NET Core](#org320aa52)
- [Step 2: Create the files needed for the example](#orgd0bf219)
  - [project.json](#org9ac7c6b)
  - [Startup.cs](#org960fdef)
  - [Main.cs](#orgd2e2385)
  - [HomeModule.cs](#orgb9af79d)
- [Step 3: Run the example](#orgff68563)


<a id="org94c5fde"></a>

# .NET Core and Nancy example code

This is a short writeup of what it took for me to get a basic working "hello world" application working with .NET and Nancy on my Mac.

This is based on Scott Hanselman's blog post "[Exploring a minimal WebAPI with .NET Core and NancyFX](http://www.hanselman.com/blog/ExploringAMinimalWebAPIWithNETCoreAndNancyFX.aspx)", with the modifications that I needed to get it working for myself.


<a id="org320aa52"></a>

# Step 1: Install .NET Core

First step is to [install .NET core](https://www.microsoft.com/net/core), this is pretty easy to do, just follow the instructions. You'll know that the install worked if you can type this in your terminal and see a result:

```sh
dotnet --version
```


<a id="orgd0bf219"></a>

# Step 2: Create the files needed for the example

To get the example working, you'll need to create four files:

1.  project.json
2.  Startup.cs
3.  Main.cs
4.  HomeModule.cs

See below for what each file should contain.


<a id="org9ac7c6b"></a>

## project.json

```javascript
{
  "version": "1.0.0-*",
  "buildOptions": {
    "debugType": "portable",
    "emitEntryPoint": true
  },
  "dependencies": {
    "Microsoft.NETCore.App": {
      "version": "1.1.0",
      "type": "platform"
    },
    "Microsoft.AspNetCore.Server.Kestrel": "1.1.0",
    "Microsoft.AspNetCore.Owin": "1.1.0",
    "Nancy": "2.0.0-barneyrubble"
  },
  "commands": {
    "web": "Microsoft.AspNet.Server.Kestrel"
  },
  "frameworks": {
    "netcoreapp1.0": {}
  }
}
```

*Note:* I had to change the version numbers from "1.0.0" to "1.1.0" to get things to work.


<a id="org960fdef"></a>

## Startup.cs

```java
using Microsoft.AspNetCore.Builder;
using Nancy.Owin;

namespace NancyApplication
{
   public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy());
        }
   }
}
```


<a id="orgd2e2385"></a>

## Main.cs

```java
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace NancyApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
```


<a id="orgb9af79d"></a>

## HomeModule.cs

```java
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
```


<a id="orgff68563"></a>

# Step 3: Run the example

Once you have created the four files needed for this sample, run the following command to "restore" the .NET dependencies listed in `project.json`:

```sh
dotnet restore
```

Once that is complete, start up the example Nancy web server with this command:

```
dotnet run
```
