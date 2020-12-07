using SkeMapper.TypeContainer;

namespace SkeMapper.ResolveTypeLogic
{
    public sealed class ResolverFactory
    {
        public IResolver Create(ResolveType resolveType)
        {
            var container = Container.Instance;
            switch (resolveType)
            {
                case ResolveType.Single:
                    return new SingleTypeResolver(container);
                case ResolveType.Collection:
                    return new CollectionTypeResolver(container);
            }
            return null;
        }
    }
}
