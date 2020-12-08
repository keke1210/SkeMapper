using SkeMapper.TypeContainer;

namespace SkeMapper.ResolveTypeLogic
{
    internal sealed class ResolverFactory
    {
        private readonly IContainer container;
        public ResolverFactory(IContainer container)
        {
            this.container = container ?? throw new System.ArgumentNullException(nameof(container));
        }

        internal IResolver Create(ResolveType resolveType)
        {
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
