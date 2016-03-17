using System;
using System.Text;
using System.Windows;
using ILuffy.IOP.I18N;

namespace ILuffy.IOP.UI
{
    public static class I18NUtility
    {
        public static string GetString(string key, params object[] objs)
        {
            var keyFormat = Application.Current.TryFindResource(key);

            if (keyFormat == null)
            {
                var builder = new StringBuilder();
                builder.Append(CoreRS.ResourceKeyFormatFormat(key));

                if (objs != null)
                {
                    foreach (var item in objs)
                    {
                        builder.Append(item);
                        builder.Append(';');
                    }
                }

                return builder.ToString();
            }
            else
            {
                if (objs != null && objs.Length > 0)
                {
                    try
                    {
                        return string.Format(keyFormat as string, objs);
                    }
                    catch (Exception ex)
                    {
                        var builder = new StringBuilder();

                        if (objs != null)
                        {
                            foreach (var item in objs)
                            {
                                builder.Append(item);
                                builder.Append(';');
                            }
                        }

                        return CoreRS.FormatResourceKeyFailedFormat(keyFormat, builder.ToString(), ex);
                    }
                }

                return keyFormat as string;
            }
        }
    }
}