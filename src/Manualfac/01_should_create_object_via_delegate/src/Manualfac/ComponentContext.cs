using System;
using System.Collections.Generic;

namespace Manualfac
{
    public class ComponentContext : IComponentContext
    {
        readonly Dictionary<Type, Func<IComponentContext, object>> registerDictionary;

        internal ComponentContext(Dictionary<Type, Func<IComponentContext, object>> registerDictionary)
        {
            this.registerDictionary = registerDictionary;
        }

        #region Please modify the following code to pass the test

        /*
         * A ComponentContext is used to resolve a component. Since the component
         * is created by the ContainerBuilder, it brings all the registration
         * information. 
         * 
         * You can add non-public member functions or member variables as you like.
         */

        public object ResolveComponent(Type type)
        {
            if(type == null) throw new ArgumentNullException(nameof(type));

            if(registerDictionary.ContainsKey(type))
            {
                return registerDictionary[type].Invoke(this);
            }
            throw new DependencyResolutionException();
        }

        #endregion
    }
}