namespace ILuffy.IOP.Ioc
{
    using System;
    using Castle.Windsor;
    public static class IocContainer
    {

        private static readonly IWindsorContainer container = new WindsorContainer();

        public static IWindsorContainer Container
        {
            get { return container; }
        }
    }
}
