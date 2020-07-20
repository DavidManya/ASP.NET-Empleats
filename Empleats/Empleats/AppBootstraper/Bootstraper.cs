using Common.Lib.Core;
using Common.Lib.Core.Context;
using Common.Lib.Infrastructure;
using Common.Lib.Infrastructure.Interfaces;
using Empleats.DAL;
using Empleats.Lib.DAL;
using Empleats.Lib.Models;
using EmpleatsASPNET.DbContextFactory;
using System;

namespace EmpleatsASPNET.AppBootstraper
{
    public class Bootstraper
    {
        public IDependencyContainer Init()
        {
            var depCon = new SimpleDependencyContainer();

            RegisterRepositories(depCon);

            Entity.DepCon = depCon;

            return depCon;
        }

        public void RegisterRepositories(IDependencyContainer depCon)
        {
            var empleatRepoBuilder = new Func<object[], object>((parameters) =>
            {
                return new GenericRepository<Empleat>(GetDbConstructor());
            });

            depCon.Register<IRepository<Empleat>, GenericRepository<Empleat>>(empleatRepoBuilder);
        }
        private static ProjectDBContext GetDbConstructor()
        {
            var factory = new EmpleatsDbContextFactory();
            return factory.CreateDbContext(null);
        }
    }
}
