namespace ILuffy.IOP.Net
{
    using System;

    public interface IJsonRequest
    {
        T Get<T>(RequestParameters parameter);
    }
}
