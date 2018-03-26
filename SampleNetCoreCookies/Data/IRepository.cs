namespace SampleNetCoreCookies.Data
{
    public interface IRepository<T>
    {
        bool Exists(string sid);

        T Get(string sid);
        T Create();
        void Save(T item);

        void Update(T item);

        void Delete(string sid);
    }
}
