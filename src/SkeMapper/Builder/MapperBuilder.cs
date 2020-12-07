using SkeMapper.Settings;
using SkeMapper.TypeContainer;

namespace SkeMapper.Builder
{
    public class MapperBuilder : IApplySettings, IBuildMapper
    {
        private MapperBuilder() { }

        public static IApplySettings Instance => new MapperBuilder();

        public IBuildMapper ApplySettings(MapperSettings mapperSettings)
        {
            mapperSettings.Config.Invoke(new MapperOptions(Container.Instance));
            return this;
        }

        public IMapper Build()
        {
            return SkeMapper.Instance;
        }
    }
}
