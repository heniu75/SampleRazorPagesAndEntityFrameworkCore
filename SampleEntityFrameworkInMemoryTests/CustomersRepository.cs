using System.Linq;

namespace SampleEntityFrameworkInMemoryTests
{
    public interface IRepository<T>
    {
        void Add(T entity);
        IQueryable<T> Get();
        int SaveChanges();
    }

    public class CustomerRepository : IRepository<Customer> 
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public IQueryable<Customer> Get()
        {
            return _context.Customers;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
