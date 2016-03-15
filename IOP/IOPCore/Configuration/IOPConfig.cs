namespace ILuffy.IOP
{
    using System;
    using System.Configuration;
    using System.IO;
    using Configuration;
    using I18N;
    public static class IOPConfig
    {
        private static IOPConfigurationSection configuration;

        public static IOPConfigurationSection Configuration
        {
            get
            {
                if (configuration == null)
                {
                    var configurationFile = Path.Combine(IOPEnv.RootFolder, IOPConstants.IOPConfigurationName);
                    var exeConfiguration = ConfigurationManager.OpenExeConfiguration(configurationFile);

                    configuration = (IOPConfigurationSection)(exeConfiguration.GetSection(
                        IOPConfigurationSection.IOPConfigurationSectionSectionPath));

                    if (configuration == null)
                    {
                        throw new IOPException(IOPErrorCode.IOPConfigIsNotAvailable,
                            CoreRS.IOPConfigIsNotAvailableFormat(configurationFile));
                    }
                }

                return configuration;
            }
        }
    }
}
