using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Context
{

    public interface IAppContextFactory
    {
        AppDataContext CreateDbContext();
    }
    public class MemoryAppContext : IAppContextFactory
    {
        public AppDataContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDataContext>();
            optionsBuilder.UseInMemoryDatabase("TestDB");

            return new AppDataContext(optionsBuilder.Options);
        }
    }
}
