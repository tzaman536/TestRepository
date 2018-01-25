using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.FindMedian
{
    public class FindMedian
    {

        static void AddToMaxHeap(int number, ref LinkedList<int> heap)
        {
            if(heap.Count == 0)
            {
                heap.AddFirst(number);
                return;
            }

            LinkedListNode<int> current = heap.Find(heap.First());
            while(current != null && number <current.Value)
            {
                current = current.Next;
            }
            if (current == null)
            {
                heap.AddLast(number);
            }
            else
                heap.AddBefore(current, number);
          
        }

        static void AddToMinHeap(int number, ref LinkedList<int> heap)
        {
            if (heap.Count == 0)
            {
                heap.AddFirst(number);
                return;
            }

            LinkedListNode<int> current = heap.Find(heap.First());
            while (current != null && number > current.Value)
            {
                current = current.Next;
            }
            if (current == null)
            {
                heap.AddLast(number);
            }
            else
                heap.AddBefore(current, number);

        }


        public static void Start()
        {
            LinkedList<int> maxHeap = new LinkedList<int>();
            LinkedList<int> minHeap = new LinkedList<int>();


            Random rnd = new Random();
            int nextNumber;
            int maxHeapFirstNumber = 0;
            int minHeapFirstNumber = 0;
            for(int i=0; i< 10; i++)
            {
                nextNumber = rnd.Next(100);
                //AddToMinHeap(nextNumber, ref minHeap);


                if(maxHeap.Count == 0 )
                {
                    maxHeap.AddFirst(nextNumber);
                    continue;
                }

                if(minHeap.Count == 0)
                {
                    if (maxHeap.First.Value > nextNumber)
                    {
                        minHeap.AddFirst(maxHeap.First.Value);
                        maxHeap.RemoveFirst();
                        maxHeap.AddFirst(nextNumber);
                    }
                    else
                        minHeap.AddFirst(nextNumber);

                    continue;
                }

                maxHeapFirstNumber = maxHeap.First.Value;
                minHeapFirstNumber = minHeap.First.Value;


                if (nextNumber > maxHeapFirstNumber)
                {
                    AddToMinHeap(nextNumber,ref minHeap);
                }
                else
                {
                    AddToMaxHeap(nextNumber, ref maxHeap);
                }

                if (maxHeap.Count == minHeap.Count)
                    continue;

                if(maxHeap.Count > minHeap.Count && maxHeap.Count - minHeap.Count > 1 )
                {
                    // Time to move firs item from maxHeap to minHeap
                    minHeap.AddFirst(maxHeap.First.Value);
                    maxHeap.RemoveFirst();
                    continue;
                }

                if(minHeap.Count > maxHeap.Count && minHeap.Count - maxHeap.Count > 1)
                {
                    maxHeap.AddFirst(minHeap.First.Value);
                    minHeap.RemoveFirst();
                    continue;
                }

              
            }

            Console.ReadLine();
        }
    }
}
