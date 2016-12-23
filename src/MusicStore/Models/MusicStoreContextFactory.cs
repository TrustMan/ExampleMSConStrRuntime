using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; //Add line
using Microsoft.Extensions.Configuration; //Add line

namespace MusicStore.Models
{
    public class MusicStoreContextFactory //Add this context of class
    {
        private readonly IConfiguration _configuration; //add line

        public MusicStoreContextFactory(IConfiguration configuration) //add constructor
        {
            _configuration = configuration;
        }
        public MusicStoreContext CreateApplicationDbContext() //Add method returning new application context 
        {
            var optionsBuilder = new DbContextOptionsBuilder<MusicStoreContext>();
            string connectionString = _configuration.GetConnectionString("MySQLConnection"); 
            if (connectionString != null)
            {
                optionsBuilder.UseMySql(connectionString);
            }
            return new MusicStoreContext(optionsBuilder.Options);
        }
    }
}
