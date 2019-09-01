using System;
using System.Collections.Generic;

namespace DataStructure
{
    /// <summary>
    /// list
    /// </summary>
    public class List<T>
    {
        private int _capacity;
        private int _size;
        private T[] _buffer;
        private IEqualityComparer<T> _equalityComparer;

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity => _capacity;

        /// <summary>
        /// 实际数量
        /// </summary>
        public int Count => _size;

        /// <summary>
        /// 初始化
        /// </summary>
        public List()
        {
            _capacity = 5;
            _size = 0;
            _buffer = new T[_capacity];
            _equalityComparer = EqualityComparer<T>.Default;
        }

        /// <summary>
        /// 初始化带参数容量大小
        /// </summary>
        /// <param name="capacity">初始化容量大小</param>
        public List(int capacity)
        {
            if (capacity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "must > 0");
            }

            _capacity = capacity;
            _size = 0;
            _buffer = new T[_capacity];
            _equalityComparer = EqualityComparer<T>.Default;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _size)
                {
                    throw new IndexOutOfRangeException($"index must >=0 and <={_size - 1}");
                }
                return _buffer[index];
            }
            set => Insert(index, value);
        }
        /// <summary>
        /// 添加一个数据
        /// </summary>
        /// <param name="value">新数据</param>
        public void Add(T value)
        {
            if (_size >= _buffer.Length)//重新分配
            {
                _capacity = _capacity * 2;
                var oldBuffer = _buffer;
                _buffer = new T[_capacity];
                for (var i = 0; i < _size; i++)
                {
                    _buffer[i] = oldBuffer[i];
                }
            }
            _buffer[_size] = value;
            _size++;
        }

        /// <summary>
        /// 移除一个元素
        /// </summary>
        /// <param name="value"></param>
        public void Remove(T value)
        {
            var index = GetIndex(value);
            RemoveAt(index);
        }

        /// <summary>
        /// 移除指定位置元素
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if (index >= _size)
            {
                throw new IndexOutOfRangeException("index out");
            }

            var lastIndex = _size - 1;
            for (int i = index; i < _size; i++)
            {
                if (i >= lastIndex)
                {
                    _buffer[i] = default(T);
                }
                else
                {
                    _buffer[i] = _buffer[i + 1];
                }
            }

            _size--;
        }

        /// <summary>
        /// 在特定位置插入新值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert(int index, T value)
        {
            if (index < 0 || index > _size)
            {
                throw new IndexOutOfRangeException($"index must >= 0 and <= {_size }");
            }

            if (_size >= _buffer.Length)
            {
                _capacity = _capacity * 2;
                var oldBuffer = _buffer;
                _buffer = new T[_capacity];
                for (var i = 0; i < _size; i++)
                {
                    _buffer[i] = oldBuffer[i];
                }
            }

            if (index != _size)
            {
                for (int i = _size; i > index; i--)
                {
                    _buffer[i] = _buffer[i - 1];
                }
            }
            _buffer[index] = value;
            _size++;
        }

        private int GetIndex(T value)
        {
            if (_size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "not found value");
            }

            if (value is IEquatable<T> equatable)
            {
                for (int i = 0; i < _size; i++)
                {
                    if (equatable.Equals(_buffer[i]))
                    {
                        return i;
                    }
                }
            }
            else
            {
                for (int i = 0; i < _size; i++)
                {
                    if (_equalityComparer.Equals(value, _buffer[i]))
                    {
                        return i;
                    }
                }
            }
            throw new ArgumentOutOfRangeException(nameof(value), "not found value");
        }
    }
}
