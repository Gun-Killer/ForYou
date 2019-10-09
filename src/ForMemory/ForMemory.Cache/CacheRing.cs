using System;
using System.Collections.Generic;

namespace ForMemory.Cache
{
    public class CacheRing<T>
    {
        private T[] _rings;
        private readonly int _capacity;
        private int _count;

        public int Capacity => _capacity;

        public CacheRing()
        {
            _rings = new T[100];
            _capacity = 100;
        }

        public CacheRing(int capacity)
        {
            _rings = new T[capacity];
            _capacity = capacity;
        }

        public void Add(T value)
        {
            if (_count >= _capacity)
            {
                throw  new ArgumentOutOfRangeException();
            }

            var hashCode = value.GetHashCode() & 0X7FFFFFFF;
            var index = hashCode % _capacity; 

            if (IsDefaultValue(index))
            {
                _rings[index] = value;
            }
            else
            {
                do
                {
                    index++;
                    if (index >= _capacity)
                    {
                        index = 0;
                    }

                    if (IsDefaultValue(index))
                    {
                        _rings[index] = value;
                        break;
                    }
                } while (true);
            }

            _count++;
        }


        private bool IsDefaultValue(int index)
        {
            var existValue = _rings[index];
            var mark = false;
            if (typeof(T).IsClass)
            {
                mark = existValue == null;
            }
            else
            {
                var equals = existValue as IEquatable<T>;
                if (equals == null)
                {
                    mark = EqualityComparer<T>.Default.Equals(existValue, default);
                }
                else
                {
                    mark = equals.Equals(default);
                }
            }

            return mark;
        }
    }
}