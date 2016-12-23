using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace MusicStore.Models
{
    public class MusicStoreContextFactory
    {
        private readonly IConfiguration _configuration;

        public MusicStoreContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MusicStoreContext CreateApplicationDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MusicStoreContext>();
            string connectionString = GetConStrVcapServicesCleardbMysql("MySQLConnection"); //add line
            //string connectionString = _configuration.GetConnectionString("MySQLConnection"); DELETE line
            if (connectionString != null)
            {
                optionsBuilder.UseMySql(connectionString);
            }
            return new MusicStoreContext(optionsBuilder.Options);
        }

        private string GetConStrVcapServicesCleardbMysql(string nameDefaultConStr) //add this metod
        {
            string connectionString;
            if (_configuration["cleardb:0:credentials:hostname"] != null)
            {
                connectionString = string.Format(
                    "Server={0},{1};Database={2};Userid={3};Pwd={4};", _configuration["cleardb:0:credentials:hostname"],
                    _configuration["cleardb:0:credentials:port"], _configuration["cleardb:0:credentials:name"],
                    _configuration["cleardb:0:credentials:username"], _configuration["cleardb:0:credentials:password"]);
            }
            else
            {
                connectionString = _configuration.GetConnectionString(nameDefaultConStr);
            }
            return connectionString;
        }
    }
}
