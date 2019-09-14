﻿using System;
using System.Collections.Generic;

namespace DataStructure
{
    /// <summary>
    /// 数据不重复集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HashSet<T>
    {
        private int _size;
        private IEqualityComparer<T> _equalityComparer;
        private Entity[] _buffer;
        private int[] _buckets;
        struct Entity
        {
            public T Value { get; set; }
            public int HashCode { get; set; }
            public int Next { get; set; }
        }

        public HashSet()
        {
            _buffer = new Entity[5];
            _equalityComparer = EqualityComparer<T>.Default;
            _buckets = new int[5];
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            var hashCode = value.GetHashCode() & 0x7FFFFFFF;
            var bucketIndex = hashCode % _buckets.Length;

            for (var i = _buckets[bucketIndex] - 1; i > 0; i = _buffer[i].Next)
            {
                if (hashCode == _buffer[i].HashCode && _equalityComparer.Equals(value, _buffer[i].Value))//已经存在
                {
                    return;
                }
            }

            if (_size == _buffer.Length)//扩容
            {
                var oldBuffer = _buffer;
                _buffer = new Entity[oldBuffer.Length * 2];
                _buckets = new int[_buffer.Length];

                for (int i = 0; i < oldBuffer.Length; i++)
                {
                    _buffer[i] = oldBuffer[i];
                    var midBucket = oldBuffer[i].HashCode % _buffer.Length;
                    _buffer[i].Next = _buckets[midBucket] - 1;
                    _buckets[midBucket] = i + 1;
                }
                bucketIndex = hashCode % _buckets.Length;
            }

            _buffer[_size].Value = value;
            _buffer[_size].HashCode = hashCode;
            _buffer[_size].Next = _buckets[bucketIndex] - 1;

            _size++;
            _buckets[bucketIndex] = _size;

        }
    }
}