﻿using System;

namespace Manualfac.Activators
{
    class DelegatedInstanceActivator : IInstanceActivator
    {
        #region Please modify the following code to pass the test

        /*
         * We have create an interface for activators so that we can extend them easily.
         * Please migrate the delegate activator to this class.
         * 
         * No public members are allowed to create.
         */
        Func<IComponentContext, object> func;
        public DelegatedInstanceActivator(Func<IComponentContext, object> func)
        {
            if(func == null) throw new ArgumentNullException(nameof(func));
            this.func = func;
        }

        public object Activate(IComponentContext componentContext)
        {
           try
           {
               return func(componentContext);
           }
            catch
            {
                throw new DependencyResolutionException();
            }
        }

        #endregion
    }
}