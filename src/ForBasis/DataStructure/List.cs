using System;

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

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity
        {
            get => _capacity;
            //set => _capacity = value;
        }

        /// <summary>
        /// 实际数量
        /// </summary>
        public int Count
        {
            get => _size;
        }


        public List()
        {
            _capacity = 5;
            _size = 0;
            _buffer = new T[_capacity];
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
    }
}
