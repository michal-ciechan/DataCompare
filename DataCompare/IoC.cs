using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightInject;

namespace DataCompare
{
    public class IoC
    {
        private static ServiceContainer _container;

        public static ServiceContainer Container => 
            _container ?? (_container = CreateContainer());


        private static ServiceContainer CreateContainer()
        {
            var container = new ServiceContainer();

            container.RegisterInstance(DataComparerConfig.CreateDefault());

            
            return container;
        }
    }
}
