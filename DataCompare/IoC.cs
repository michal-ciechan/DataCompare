
using SimpleInjector;

namespace DataCompare
{
    public class IoC
    {
        private static Container _container;

        public static Container Container => 
            _container ?? (_container = CreateContainer());


        private static Container CreateContainer()
        {
            var container = new Container();

            container.Register(() => DataComparerConfig.Default);
            container.Register<DataComparer>();
            container.Register<ISorter, Sorter>();
            container.Register<IRowComparerFactory, RowComparerFactory>();
            container.Register<IKeyMapperFactory, KeyMapperFactory>();
            container.Register<IValueMapperFactory, ValueMapperFactory>();
            
            return container;
        }
    }
}
