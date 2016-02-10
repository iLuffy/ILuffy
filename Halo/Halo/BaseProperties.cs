using System;
using System.Collections.Generic;
using ILuffy.Halo.I18N;

namespace ILuffy.Halo
{
    public abstract class BaseProperties
    {
        /// <summary>
        /// Custom Properties
        /// </summary>
        public Dictionary<string, string> KeyValues { get; set; }

        /// <summary>
        /// Get Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Nullable<T> Get<T>(string key)
        {
            var keyValues = KeyValues;

            string value;
            if (keyValues != null && keyValues.TryGetValue(key, out value))
            {
                return value.Get<T>();
            }

            return new Nullable<T>();
        }

        /// <summary>
        /// Get Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key, T defaultValue)
        {
            var keyValues = KeyValues;

            string value;
            if (keyValues != null && keyValues.TryGetValue(key, out value))
            {
                var nullableValue = value.Get<T>();

                if (nullableValue.HasValue)
                {
                    return nullableValue.Value;
                }
            }

            return defaultValue;
        }

        public T GetByJavaScript<T>(string key, T defaultValue)
        {
            var content = Get<string>(key, null);

            if (!string.IsNullOrEmpty(content))
            {
                return SerializerHelper.DeserializeByJavaScript<T>(content);
            }

            return defaultValue;
        }

        public T GetByJavaScript<T>(string key, T defaultValue, bool throwExceptionWhenNotFound)
        {
            var content = Get<string>(key, null);

            if (!string.IsNullOrEmpty(content))
            {
                return SerializerHelper.DeserializeByJavaScript<T>(content);
            }

            if (throwExceptionWhenNotFound)
            {
                throw new Exception(CoreRS.EntryNotFoundFormat(key));
            }

            return defaultValue;
        }

        /// <summary>
        /// set value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set<T>(string key, T value)
        {
            Dictionary<string, string> keyValues = null;

            if (KeyValues == null)
            {
                keyValues = KeyValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                keyValues = KeyValues;
            }

            if (value == null)
            {
                keyValues[key] = null;
            }
            else
            {
                keyValues[key] = StringExtension.Get<T>(value);
            }
        }

        public void SetByJavaScript<T>(string key, T value)
        {
            if (value != null)
            {
                var content = SerializerHelper.SerializeByJavaScript(value);
                Set<string>(key, content);
            }
        }
    }
}