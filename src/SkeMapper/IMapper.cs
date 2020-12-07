namespace SkeMapper
{
    public interface IMapper
    {
        TDestination Map<TDestination>(object source);
    }
}
