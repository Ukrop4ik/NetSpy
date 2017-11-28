using System;
using System.Collections.Generic;
using System.Linq;


    public class Heap<TItemType>
    {
        private readonly SortedDictionary<float, Queue<TItemType>> _heap = new SortedDictionary<float, Queue<TItemType>>();

        public void Push(float weight, TItemType item)
        {
            Queue<TItemType> queue;

            if (!_heap.ContainsKey(weight))
            {
                queue = new Queue<TItemType>();
                _heap.Add(weight, queue);
            }
            else
            {
                queue = _heap[weight];
            }

            queue.Enqueue(item);
        }

        public TItemType Pop()
        {
            var r = _heap.First();
            var item = r.Value.Dequeue();
            if (r.Value.Count == 0)
                _heap.Remove(r.Key);
            return item;
        }

        public int Count { get { return _heap.Count; } }
        public bool Empty { get { return _heap.Count == 0; } }
        public void Clear() { _heap.Clear(); }
    }



