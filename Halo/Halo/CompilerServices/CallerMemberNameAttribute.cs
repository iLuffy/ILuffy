#if NetFramework45
#else
namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// This attribute is retrieve the member name on compile time
    ///
    /// This attribute is only available in 4.5 framework or above,
    /// so we need to create the same one for unsupported framework
    /// </summary>
    internal sealed class CallerMemberNameAttribute : Attribute
    {
    }
}
#endif