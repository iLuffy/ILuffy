using System;
using ILuffy.Halo.I18N;

namespace ILuffy.Halo
{
    public abstract class InstanceInfo : BaseProperties
    {
        public string Name { get; set; }

        public string TypeFullName { get; set; }

        public T CreateInstance<T>(params object[] objs)
        {
            if (string.IsNullOrEmpty(TypeFullName))
            {
                throw new Exception(CoreRS.TypeFullNameIsEmptyFormat(Name));
            }

            var type = Type.GetType(TypeFullName, true, false);

            return (T)Activator.CreateInstance(type, objs);
        }
    }
}