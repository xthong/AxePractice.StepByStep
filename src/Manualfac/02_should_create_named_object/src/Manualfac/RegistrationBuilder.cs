using System;
using Manualfac.Services;

namespace Manualfac
{
    class RegistrationBuilder : IRegistrationBuilder
    {
        public Service Service { get; set; }
        public Func<IComponentContext, object> Activator { get; set; }

        public IRegistrationBuilder As<TService>()
        {
            #region Please modify the code to pass the test

            /*
             * Please support registration by type.
             */

            Type serviceType = typeof(TService);
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
            Service = new TypedService(serviceType);
            return this;

            #endregion
        }

        public IRegistrationBuilder Named<TService>(string name)
        {
            #region Please modify the code to pass the test

            /*
             * Please support registration by both type and name.
             */
            Type serviceType = typeof(TService);
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));

            Service = new TypedNameService(serviceType, name);
            return this;
            #endregion
        }

        public ComponentRegistration Build()
        {
            return new ComponentRegistration(Service, Activator);
        }
    }
}