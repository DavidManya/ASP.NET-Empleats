﻿using Empleats.Lib.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EmpleatsASPNET.DbContextFactory
{
    public class EmpleatsDbContextFactory : IDesignTimeDbContextFactory<ProjectDBContext>
    {
        //    public ProjectDBContext CreateDbContext(string[] args)
        //    {
        //        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        //            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


        //        var configuration = builder.Build();
        //        var dbConnection = configuration.GetConnectionString("EmpleatsDbContext");

        //        var optionsBuilder = new DbContextOptionsBuilder<ProjectDBContext>();
        //        optionsBuilder.UseSqlServer(dbConnection, x => x.MigrationsAssembly("ASPNET Application"));

        //        return new ProjectDBContext(optionsBuilder.Options);
        //    }
        public ProjectDBContext CreateDbContext(string[] args)
        {
            throw new System.NotImplementedException();
        }
    }
}
