using LiteDB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleNetCoreCookies.Data
{
    public interface ICustomLiteDbContextOptions
    {
        string DbFileName { get; }
    }

    public class CustomLiteDbContextOptions : ICustomLiteDbContextOptions
    {
        public string DbFileName { get; set; }
    }

    public interface IApplicationOptions
    {
        int MinutesToExpire { get; }
        string Domain { get; }
    }

    public class ApplicationOptions : IApplicationOptions
    {
        public int MinutesToExpire { get; set; }
        public string Domain { get; set; }
    }

    public class CustomLiteDbContext 
    {
        private readonly ICustomLiteDbContextOptions _options;

        public CustomLiteDbContext(ICustomLiteDbContextOptions options)
        {
            _options = options;
        }

        public LiteCollection<Address> Addresses { get; set; }
    }
}
