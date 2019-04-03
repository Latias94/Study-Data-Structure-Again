using System;
using System.Text;

namespace Array
{
    public class Array<T>
    {
        /// <summary>
        /// 存储数据的数组
        /// </summary>
        private T[] data;

        /// <summary>
        /// 数组的容量(最多能容纳多少个元素)
        /// </summary>
        private int capacity;

        /// <summary>
        /// 数组目前有多少个元素
        /// </summary>
        private int size;

        /// <summary>
        /// 根据传入的容量量初始化
        /// </summary>
        /// <param name="capacity">数组容量</param>
        public Array(int capacity)
        {
            this.capacity = capacity;
            data = new T[capacity];
            size = 0;
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Array() : this(10)
        {
        }

        public int GetSize()
        {
            return size;
        }

        /// <summary>
        /// 获取数组中的数据容量(最多可以容纳多少元素)
        /// </summary>
        /// <returns>数组容量</returns>
        public int GetCapacity()
        {
            return capacity;
        }

        /// <summary>
        /// 判断数组中数据是否为空
        /// </summary>
        public bool IsEmpty()
        {
            return size == 0;
        }

        /// <summary>
        /// 做一些预检查，比如范围、临界啊等等
        /// </summary>
        public void PreCheck()
        {
            if (size == data.Length)
            {
                throw new ArgumentException("容量已满！不能再添加新元素！");
            }
        }


        /// <summary>
        /// 向 data 中的 index 位置插入一个元素 e
        /// </summary>
        public void Insert(int index, T element)
        {
            // 动态扩容就不需要检查了
//            PreCheck();
            if (index < 0 || index > size)
            {
                throw new ArgumentException("插入元素失败！index 的范围必须在[0, size)");
            }

            // 数组满了扩容一倍
            if (size == data.Length)
            {
                Resize(2 * capacity);
            }

            // index 后面的所有元素后移
            for (int i = size - 1; i >= index; i--)
            {
                data[i + 1] = data[i];
            }

            data[index] = element;
            size++;
        }

        /// <summary>
        /// 数组大小进行调整，根据数组元素数目决定数组是扩容还是缩容
        /// </summary>
        /// <param name="newCapacity">新容量</param>
        private void Resize(int newCapacity)
        {
            // 新的容量赋值
            capacity = newCapacity;
            T[] newData = new T[newCapacity];
            for (int i = 0; i < size; i++)
            {
                newData[i] = data[i];
            }

            // 释放原来的 data,指向新的 data
            data = newData;
        }

        /// <summary>
        /// 向所有元素最后加一个元素
        /// </summary>
        public void AddLast(T element)
        {
            Insert(size, element);
        }

        /// <summary>
        /// 向所有元素开始位置加一个元素
        /// </summary>
        public void AddFirst(T element)
        {
            Insert(0, element);
        }
        
        /// <summary>
        /// 获取最后的元素
        /// </summary>
        public T GetLast() {
            return Get(size - 1);
        }

        /// <summary>
        /// 获取第一个元素
        /// </summary>
        public T GetFirst() {
            return Get(0);
        }

        /// <summary>
        /// 获取指定位置的元素
        /// </summary>
        public T Get(int index)
        {
            if (index < 0 || index > size)
            {
                throw new ArgumentException("index 的范围必须在[0, size)");
            }

            return data[index];
        }

        /// <summary>
        /// 更新指定位置的元素
        /// </summary>
        /// <param name="index">要更新的索引位置</param>
        /// <param name="element">待更新元素</param>
        public void Set(int index, T element)
        {
            if (index < 0 || index > size)
            {
                throw new ArgumentException("index的范围必须在[0, size)");
            }

            data[index] = element;
        }

        /// <summary>
        /// 是否包含指定元素
        /// </summary>
        /// <param name="element">待查询元素</param>
        public bool Contain(T element)
        {
            for (int i = 0; i < size; i++)
            {
                if (data[i].Equals(element))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 查找指定元素
        /// </summary>
        /// <param name="element">待查询元素</param>
        /// <returns>所在索引</returns>
        public int Find(T element)
        {
            for (int i = 0; i < size; i++)
            {
                if (data[i].Equals(element))
                {
                    return i;
                }
            }

            // 没找到就返回-1
            return -1;
        }

        /// <summary>
        /// 从数组中删除 index 位置的元素，并返回删除的元素
        /// </summary>
        /// <param name="index">待删除所在的索引</param>
        /// <returns>删除的元素</returns>
        public T Remove(int index)
        {
            if (index < 0 || index > size)
            {
                throw new ArgumentException("index的范围必须在[0, size)");
            }

            T value = data[index];
            // index 位置开始，每个元素往前挪一位
            for (int i = index + 1; i < size; i++)
            {
                data[i - 1] = data[i];
            }

            size--;
            data[size] = default(T);
            // 当数组中元素数小于容量的 1/4 时，自动缩容为原来的一半
            // 之所以选 1/4 是为了防止频繁扩容和缩容引起性能下降
            if (size == capacity / 4 && data.Length / 2 != 0) {
                Resize(capacity / 2);
            }
            return value;
        }

        /// <summary>
        /// 删除数组头的元素
        /// </summary>
        /// <returns>删除的元素</returns>
        public T RemoveFirst()
        {
            return Remove(0);
        }

        /// <summary>
        /// 删除数组尾的元素
        /// </summary>
        /// <returns>删除的元素</returns>
        public T RemoveLast()
        {
            return Remove(size - 1);
        }

        /// <summary>
        /// 删除指定值的元素，不适用于存在重复元素的情况
        /// </summary>
        /// <param name="element">要删除的元素</param>
        public void RemoveElement(T element)
        {
            int index = Find(element);
            if (index != -1)
            {
                Remove(index);
            }
        }

        public override string ToString()
        {
            return $"Array: capacity={capacity}, size={size}, data=[{ArrayToString()}]";
        }

        private string ArrayToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length - 1; i++)
            {
                sb.Append($"{data[i]}, ");
            }

            sb.Append($"{data[data.Length - 1]}");
            return sb.ToString();
        }
    }
}