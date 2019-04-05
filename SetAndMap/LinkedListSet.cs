using LinkedList;

namespace SetAndMap
{
    public class LinkedListSet<T> : ISet<T>
    {
        private LinkedList<T> list;


        public LinkedListSet()
        {
            list = new LinkedList<T>();
        }

        public void Add(T element)
        {
            if (!list.Contain(element))
            {
                list.AddFirst(element);
            }
        }

        public void Delete(T element)
        {
            list.DeleteElement(element);
        }

        public bool Contain(T element)
        {
            return list.Contain(element);
        }

        public int GetSize()
        {
            return list.GetSize();
        }

        public bool IsEmpty()
        {
            return list.IsEmpty();
        }
    }
}