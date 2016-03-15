namespace ILuffy.IOP.UI.Input
{
    using System;
    using System.Security;

    public sealed class PasswordProvider : IHavePassword
    {
        private SecureString password;

        public SecureString SecurePassword
        {
            get { return password; }
            set { DisposeSecurePassword(); password = value; }
        }

        private void DisposeSecurePassword()
        {
            if (password != null)
            {
                password.Dispose();
                password = null;
            }
        }

        public void Dispose()
        {
            DisposeSecurePassword();
        }
    }
}
