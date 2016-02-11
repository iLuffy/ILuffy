using System;
using ILuffy.IOP.I18N;

namespace ILuffy.IOP
{
    public static class InstanceInfoExtension
    {
        /// <summary>
        /// Apply the copy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static T Apply<T>(this InstanceInfo info, InstanceElement config) where T : InstanceInfo
        {
            if (config == null)
            {
                throw new ArgumentNullException(CoreRS.ArgumentIsNullFormat(nameof(config)));
            }
            if (info == null)
            {
                throw new ArgumentNullException(CoreRS.ArgumentIsNullFormat(nameof(info)));
            }

            info.Name = config.Name;
            info.TypeFullName = config.TypeFullName;

            return (T)info;
        }

        /// <summary>
        /// Create instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <param name="objs"></param>
        /// <returns></returns>
        public static T CreateInstance<T>(this InstanceInfo info, params object[] objs)
        {
            if (info == null)
            {
                throw new ArgumentNullException(CoreRS.ArgumentIsNullFormat(nameof(info)));
            }

            if (string.IsNullOrEmpty(info.TypeFullName))
            {
                throw new Exception(CoreRS.TypeFullNameIsEmptyFormat(info.Name));
            }

            var type = Type.GetType(info.TypeFullName, true, false);

            return (T)Activator.CreateInstance(type, objs);
        }
    }
}