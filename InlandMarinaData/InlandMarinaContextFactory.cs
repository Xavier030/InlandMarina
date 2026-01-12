using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    public class InlandMarinaContextFactory : IDesignTimeDbContextFactory<InlandMarinaContext>
    {
        public InlandMarinaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InlandMarinaContext>();

            optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=InlandMarina;Trusted_Connection=True;");

            return new InlandMarinaContext(optionsBuilder.Options);
        }
    }
}
