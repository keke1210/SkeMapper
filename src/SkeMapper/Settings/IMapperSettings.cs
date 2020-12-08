using System;

namespace SkeMapper.Settings
{
    public interface IMapperSettings
    {
        Action<MapperOptions> Config { get; }
    }
}
