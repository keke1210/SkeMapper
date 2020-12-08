using SkeMapper.ResolveTypeLogic;
using SkeMapper.TypeContainer;
using System;
using System.Collections;

namespace SkeMapper
{
    public sealed class SkeMapper : IMapper
    {
        public Lazy<IContainer> MapperContainer { get; } = new Lazy<IContainer>(() => Container.Instance, true);
        private SkeMapper() { }
        public static SkeMapper Instance => new SkeMapper();


        public TDestination Map<TDestination>(object source)
        {
            var factory = new ResolverFactory(MapperContainer.Value);

            var resolveType = source is IEnumerable ? ResolveType.Collection : ResolveType.Single;
            IResolver resolver = factory.Create(resolveType);

            var destination = resolver.ResolveTypeMap(source);

            return (TDestination)destination;
        }
    }
}
