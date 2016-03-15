namespace ILuffy.IOP
{
    using System;

    public static class IOPEnv
    {
        static IOPEnv()
        {
            RootFolder = System.IO.Path.GetDirectoryName(typeof(IOPEnv).Assembly.Location);
        }

        public static string RootFolder { get; set; }

    }
}
