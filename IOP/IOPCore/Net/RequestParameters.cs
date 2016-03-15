namespace ILuffy.IOP.Net
{
    using System;
    using System.Collections.Generic;

    public sealed class RequestParameters
    {
        public string Url { get; set; }

        public Dictionary<string, string> Header { get; set; }

        public Dictionary<string, string> Cookie { get; set; }

        public int? Timeout { get; set; }
    }
}
