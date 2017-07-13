using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalApi
{
    class DefaultControllerFactory : IControllerFactory
    {
        public HttpController CreateController(
            string controllerName,
            ICollection<Type> controllerTypes,
            IDependencyResolver resolver)
        {
            #region Please modify the following code to pass the test.

            /*
             * The controller factory will create controller by its name. It will search
             * form the controllerTypes collection to get the correct controller type,
             * then create instance from resolver.
             */

            Type type;
            try
            {
                type = controllerTypes
                    .SingleOrDefault(t => string.Equals(t.Name, controllerName, StringComparison.OrdinalIgnoreCase));

                if (type == null) return null;  
            }
            
            catch
            {
                throw new ArgumentException(); 
            }

            return  (HttpController)resolver.GetService(type);

            #endregion
        }
    }
}