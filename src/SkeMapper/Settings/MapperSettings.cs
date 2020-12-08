using System;

namespace SkeMapper.Settings
{
    public class MapperSettings : IMapperSettings
    {
        public MapperSettings(Action<MapperOptions> config = null)
        {
            Config = config;
        }

        public Action<MapperOptions> Config { get; }
    }
}
