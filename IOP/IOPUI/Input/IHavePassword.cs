namespace ILuffy.IOP.UI.Input
{
    using System;
    using System.Security;

    public interface IHavePassword : IDisposable
    {
        /// <summary>
        /// Secure Password
        /// </summary>
        SecureString SecurePassword { get; set; }
    }
}
