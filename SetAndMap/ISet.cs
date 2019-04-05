namespace SetAndMap
{
    public interface ISet<T>
    {
        /// <summary>
        /// 添加元素
        /// </summary>
        void Add(T element);

        /// <summary>
        /// 删除元素
        /// </summary>
        void Delete(T element);

        /// <summary>
        /// 判断是否包含某元素
        /// </summary>
        bool Contain(T element);

        /// <summary>
        /// 获取集合的元素数量
        /// </summary>
        int GetSize();

        /// <summary>
        /// 判断集合是否为空
        /// </summary>
        bool IsEmpty();
    }
}