namespace ILuffy.Halo
{
    public class Nullable<T>
    {
        public Nullable()
        {
            HasValue = false;
        }

        public Nullable(T value)
        {
            HasValue = true;
            Value = value;
        }

        public bool HasValue { get; set; }

        public T Value { get; set; }
    }
}