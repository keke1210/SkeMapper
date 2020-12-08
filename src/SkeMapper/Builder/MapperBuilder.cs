using SkeMapper.Settings;
using System;

namespace SkeMapper.Builder
{
    public class MapperBuilder : IApplySettings, IBuildMapper
    {
        private readonly Lazy<SkeMapper> mapperInstance = new Lazy<SkeMapper>(() => SkeMapper.Instance, true);

        private MapperBuilder() { }

        public static IApplySettings Instance => new MapperBuilder();

        public IBuildMapper ApplySettings(IMapperSettings mapperSettings)
        {
            mapperSettings.Config.Invoke(new MapperOptions(mapperInstance.Value.MapperContainer.Value));
            return this;
        }

        public IMapper Build()
        {
            return mapperInstance.Value;
        }
    }
}
