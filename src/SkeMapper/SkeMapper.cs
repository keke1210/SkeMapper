using SkeMapper.ResolveTypeLogic;
using System.Collections;

namespace SkeMapper
{
    public sealed class SkeMapper : IMapper
    {
        private SkeMapper() { }
        public static SkeMapper Instance => new SkeMapper();

        public TDestination Map<TDestination>(object source)
        {
            var resolveType = source is IEnumerable ? ResolveType.Collection : ResolveType.Single;

            var factory = new ResolverFactory();
            IResolver resolver = factory.Create(resolveType);

            var destination = resolver.ResolveTypeMap(source);

            return (TDestination)destination;
        }
    }
}
