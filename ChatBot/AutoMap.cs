namespace ChatBot
{
    public class AutoMap <TKey, TValue>
        where TKey : notnull
    {
        private readonly Dictionary<TKey, TValue> _map = new();
        private readonly Func<TKey, TValue> _generator;

        public TValue this[TKey key]
        {
            get
            { 
                if (_map.TryGetValue(key, out var value))
                    return value;

                var result = _generator(key);
                _map[key] = result;
                return result;
            }
            set
            {
                _map[key] = value;
            }
        }

        public AutoMap(Func<TKey, TValue> generator)
        {
            _generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }
    }
}
