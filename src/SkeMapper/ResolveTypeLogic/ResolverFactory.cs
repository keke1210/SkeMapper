using SkeMapper.TypeContainer;

namespace SkeMapper.ResolveTypeLogic
{
    internal sealed class ResolverFactory
    {
        internal IResolver Create(ResolveType resolveType)
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
