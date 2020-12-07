using SkeMapper.Settings;

namespace SkeMapper.Builder
{
    public interface IApplySettings
    {
        IBuildMapper ApplySettings(MapperSettings mapperSettings);
    }
}
