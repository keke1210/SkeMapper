using Microsoft.Extensions.DependencyInjection;
using SkeMapper.TypeContainer;

namespace SkeMapper.Extensions
{
    public static class MapperStartupExtensions
    {
        public static void AddSkeMapper(this IServiceCollection services)
        {
            services.AddSingleton<IContainer>(Container.Instance);
            services.AddSingleton<IMapper>(SkeMapper.Instance);
        }
    }
}
