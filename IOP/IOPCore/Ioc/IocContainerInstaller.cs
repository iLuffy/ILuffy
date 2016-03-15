namespace ILuffy.IOP.Ioc
{
    using System;
    using Castle.Windsor.Installer;

    public class IocContainerInstaller
    {
        public IocContainerInstaller InstallFromConfiguration()
        {
            IocContainer.Container.Install(Configuration.FromAppConfig());

            return this;
        }
    }
}
