using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CourseWork_2022_STEP
{
    public class MyContextFactory : IDesignTimeDbContextFactory<MyBooksShopContext>
    {
        public MyBooksShopContext CreateDbContext(string[] args)
        {
            //builder jakij stroit` nashi options
            var optionsBuilder = new DbContextOptionsBuilder<MyBooksShopContext>();
            IConfigurationBuilder builder = new ConfigurationBuilder(); //Microsoft.Extensions.Configuration NuGet
            builder.SetBasePath(Directory.GetCurrentDirectory()); //Microsoft.Extensions.Configuration.Json NuGet
            builder.AddJsonFile("appsettings.json");//Microsoft.Extensions.Configuration.Json NuGet
            IConfigurationRoot config = builder.Build();
            string connStr = config.GetConnectionString("sqlConnStr");
            optionsBuilder.UseSqlServer(connStr);//Microsoft.EntityFrameworkCore.SqlServer NuGet
            var options = optionsBuilder.Options;
            return new MyBooksShopContext(options);
        }
    }
}