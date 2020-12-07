using SkeMapper.TypeContainer;
using System;

namespace SkeMapper.ResolveTypeLogic
{
    internal class CollectionTypeResolver : IResolver
    {
        private readonly IContainer container;

        public CollectionTypeResolver(IContainer container)
        {
            this.container = container;
        }

        public object ResolveTypeMap(object source)
        {
            throw new NotImplementedException();
        }
    }
}
