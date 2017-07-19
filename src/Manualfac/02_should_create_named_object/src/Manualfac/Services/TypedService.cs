using System;

namespace Manualfac.Services
{
    class TypedService : Service, IEquatable<TypedService>
    {
        readonly Type serviceType;

        #region Please modify the following code to pass the test

        /*
         * This class is used as a key for registration by type.
         */

        public TypedService(Type serviceType)
        {
            this.serviceType = serviceType;
        }
        
        public bool Equals(TypedService other)
        {
            if(null == other) return false;
            if(ReferenceEquals(this, other)) return true;
            return serviceType == other.serviceType;
        }

        public override bool Equals(object obj)
        {
            if(obj == null) return false;
            if(ReferenceEquals(this, obj)) return true;
            if(obj.GetType()!= GetType()) return false;
            return Equals((TypedService) obj);
        }

        public override int GetHashCode()
        {
            return serviceType != null ? serviceType.GetHashCode() : 0;
        }

        #endregion
    }
}