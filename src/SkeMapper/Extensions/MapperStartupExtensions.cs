using Microsoft.Extensions.DependencyInjection;
using SkeMapper.Builder;
using SkeMapper.Settings;
using SkeMapper.TypeContainer;
using System;

namespace SkeMapper.Extensions
{
    public static class MapperStartupExtensions
    {
        public static void AddSkeMapper(this IServiceCollection services, MapperSettings mapperSettings)
        {
            services.AddSingleton((sp) => MapperBuilder.Instance.ApplySettings(mapperSettings).Build());
        }
        
        public static void AddSkeMapper(this IServiceCollection services, Action<MapperOptions> options)
        {
            services.AddSingleton((sp) => MapperBuilder.Instance.ApplySettings(new MapperSettings(options)).Build());
        }
    }
}
