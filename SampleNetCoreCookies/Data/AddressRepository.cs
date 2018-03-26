using LiteDB;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace SampleNetCoreCookies.Data
{
    public class AddressRepository : IRepository<Address>
    {
        private const string TableName = "Addresses";
        private readonly CustomLiteDbContextOptions _options;
        private readonly IRandomGenerator _randomGenerator;

        public readonly ApplicationOptions _appOptions;

        public AddressRepository(IOptions<CustomLiteDbContextOptions> options,
            IRandomGenerator randomGenerator,
            IOptions<ApplicationOptions> appOptions,
            IDateProvider dateProvider)
        {
            _options = options.Value;
            _randomGenerator = randomGenerator;
            _appOptions = appOptions.Value;
        }

        public bool Exists(string sid)
        {
            var address = Get(sid);
            if (address == null)
                return false;
            return true;
        }

        public Address Get(string sid)
        {
            using (var db = new LiteDatabase(_options.DbFileName))
            {
                var addresses = db.GetCollection<Address>(TableName);
                var address = addresses.Find(x => x.Cookie == sid).FirstOrDefault();
                return address;
            }
        }

        public Address Create()
        {
            using (var db = new LiteDatabase(_options.DbFileName))
            {
                var col = db.GetCollection<Address>(TableName);
                var address = CreateNewAddress();
                col.Insert(address);
                col.EnsureIndex(x => x.Cookie);
                return address;
            }
        }

        public Address CreateNewAddress()
        {
            var now = DateTime.Now;
            var expire = now.AddMinutes(_appOptions.MinutesToExpire);
            var sid = $"{now}||{Guid.NewGuid().ToString()}||{expire}";
            var address = _randomGenerator.RandomString(12, true);
            var domain = _appOptions.Domain;
            var item = new Address
            {
                Cookie = sid,
                CreatedAt = now,
                ExpireAt = expire,
                EmailAddress = $"{address}@{domain}"
            };
            return item;
        }

        public void Save(Address address)
        {
            throw new NotImplementedException();
        }

        public void Update(Address address)
        {
            throw new NotImplementedException();
        }

        public void Delete(string sid)
        {
            using (var db = new LiteDatabase(_options.DbFileName))
            {
                var addresses = db.GetCollection<Address>(TableName);
                addresses.Delete(x => x.Cookie == sid);
           }
        }
    }
}
