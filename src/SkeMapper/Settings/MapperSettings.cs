using System;

namespace SkeMapper.Settings
{
    public class MapperSettings
    {
        public MapperSettings(Action<MapperOptions> config = null)
        {
            Config = config;
        }

        public Action<MapperOptions> Config { get; }
    }
}
