using LiteDB;
using Microsoft.Extensions.Options;

namespace SampleNetCoreCookies.Data
{
    public class AddressJanitor : IJanitor<Address>
    {
        private const string TableName = "Addresses";
        private readonly CustomLiteDbContextOptions _options;
        private readonly IDateProvider _dateProvider;

        public AddressJanitor(IOptions<CustomLiteDbContextOptions> options,
            IDateProvider dateProvider)
        {
            _options = options.Value;
            _dateProvider = dateProvider;
        }

        public void Run()
        {
            // removes old address records from the database
            using (var db = new LiteDatabase(_options.DbFileName))
            {
                var addresses = db.GetCollection<Address>(TableName);
                var now = _dateProvider.Now.AddMinutes(-1);
                addresses.Delete(x => x.ExpireAt < now);
            }
        }
    }
}
