﻿using Home.Journal.Common;
using Home.Journal.Common.Model;
using Home.Journal.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;

namespace Home.Journal.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Console.WriteLine(" ╦┌─┐┬ ┬┬─┐┌┐┌┌─┐┬    ╔═╗╔═╗╔═╗");
            Console.WriteLine(" ║│ ││ │├┬┘│││├─┤│    ╠═╣╠═╝╠═╝");
            Console.WriteLine("╚╝└─┘└─┘┴└─┘└┘┴ ┴┴─┘  ╩ ╩╩  ╩  ");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("         by Manoir.app         ");
            Console.WriteLine();
            Console.WriteLine();
            var host = Environment.GetEnvironmentVariable("MONGODB_SERVICE_HOST");
            var port = Environment.GetEnvironmentVariable("MONGODB_SERVICE_PORT");

            if (string.IsNullOrEmpty(port))
                port = "27017";
            if(string.IsNullOrEmpty(host) || string.IsNullOrEmpty(port))
            {
                Console.WriteLine($" no MongoDB service defined");
                return;
            }
            Console.WriteLine($" using MongoDB at {host}:{port}");


            MongoDbHelper.CreateCollection<Page>();
            MongoDbHelper.CreateCollection<PageSection>();
            MongoDbHelper.CreateCollection<User>();

            Console.WriteLine($" MongoDB database is up-to-date");

            //PageDbHelper.TestCreate();

            Console.WriteLine($" starting web service");

            var builder = WebApplication.CreateBuilder(args);


            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseJournalPageMiddleware();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.AddAuthorization();
            //app.UseAuthorization();

            app.Run();
        }
    }
}