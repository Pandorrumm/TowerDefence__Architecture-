
namespace TowerDefence.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices instance;

        public static AllServices container => instance ?? (instance = new AllServices());

        public void RegisterSingle<TService>(TService _implementation) where TService : IService
        {
            Implementation<TService>.ServiceInstance = _implementation;
        }

        public TService Single<TService>() where TService : IService
        {
           return Implementation<TService>.ServiceInstance;
        }

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}
