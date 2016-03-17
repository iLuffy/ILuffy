namespace ILuffy.IOP
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public static class SecureStringExtension
    {
        public static string ConvertToString(this SecureString secureString)
        {
            if (secureString == null)
            {
                return null;
            }

            IntPtr stringPtr = IntPtr.Zero;
            string plainString = null;
            try
            {
                stringPtr = Marshal.SecureStringToBSTR(secureString);
                plainString = Marshal.PtrToStringBSTR(stringPtr);
            }
            finally
            {
                if (stringPtr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(stringPtr);
                }
            }

            return plainString;
        }

        public static SecureString ConvertToSecureString(this String plainText)
        {
            if(!string.IsNullOrEmpty(plainText))
            {
                var secureString = new SecureString();

                foreach(var item in plainText)
                {
                    secureString.AppendChar(item);
                }

                return secureString;
            }

            return null;
        }
    }
}
