namespace SetAndMap
{
    public interface IMap<TKey, TValue>
    {
        /// <summary>
        /// 添加/更新键值对
        /// </summary>
        void Set(TKey key, TValue value);

        /// <summary>
        /// 删除指定键的键值对
        /// </summary>
        void Delete(TKey key);

        /// <summary>
        /// 是否包含某指定键的键值对
        /// </summary>
        bool Contains(TKey key);

        /// <summary>
        /// 根据键获取键值对
        /// </summary>
        TValue Get(TKey key);

        /// <summary>
        /// 获取Map的大小
        /// </summary>
        int GetSize();

        /// <summary>
        /// 判断Map是否为空
        /// </summary>
        bool IsEmpty();
    }
}