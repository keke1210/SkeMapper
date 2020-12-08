using SkeMapper.TypeContainer;
using System.Collections;

namespace SkeMapper.Settings
{
    public class MapperOptions 
    {
        private readonly IContainer container;

        internal MapperOptions(IContainer container)
        {
            this.container = container;
        }

        public void CreateMap<TSource, TDestination>() 
            where TSource : class
            where TDestination : class
        {
            container.CreateMap(typeof(TSource), typeof(TDestination));
        }
    }
}
