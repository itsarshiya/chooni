using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        using (var db = new ChooniContext())
        {
            // Note: This sample requires the database to be created before running.
            Console.WriteLine($"Database path: {db.DbPath}.");
        }

        var host = CreateHostBuilder(args).Build();

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
