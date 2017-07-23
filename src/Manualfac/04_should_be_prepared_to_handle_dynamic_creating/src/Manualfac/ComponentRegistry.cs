﻿using System;
using System.Collections.Generic;

namespace Manualfac
{
    public class ComponentRegistry
    {
        readonly Dictionary<Service, ComponentRegistration> serviceInfos =
            new Dictionary<Service, ComponentRegistration>();

        public void Register(ComponentRegistration registration)
        {
            #region Please implement the code to pass the test

            /*
             * We have moved the odd method from Container to ComponentRegistry. Please
             * implement the method.
             */
            if(registration == null) throw new ArgumentNullException(nameof(registration));
            serviceInfos[registration.Service] = registration;

            #endregion
        }

        public bool TryGetRegistration(Service service, out ComponentRegistration registration)
        {
            #region Please implement the code to pass the test

            /*
             * Please implement the method to get registration from the registered services.
             */
            if(service == null) throw new ArgumentNullException(nameof(service));
            registration = null;
            return serviceInfos.TryGetValue(service, out registration);

            #endregion
        }
    }
}