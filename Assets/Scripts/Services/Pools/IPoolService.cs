
namespace Services.Pools
{
    public interface IPoolService
    {
        public void Get<T>();
        public void Release();
    }
}