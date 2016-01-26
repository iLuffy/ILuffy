using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// This attribute is retrieve the member name on compile time
    /// 
    /// This attribute is only available in 4.5 framework or above,
    /// so we need to create the same one for unsupported framework
    /// </summary>
    sealed class CallerMemberNameAttribute : Attribute
    {
    }
}
