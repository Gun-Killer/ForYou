using System;
using System.Collections.Generic;

namespace AddTwoNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var l1 = Create(new[] { 2, 4, 9 });
            var l2 = Create(new[] { 5, 6, 4, 9 });
            var res = AddTwoNumbers(l1, l2);
            Print(res);
            Console.ReadKey();
        }

        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            List<ListNode> cahe1 = new List<ListNode>();
            ListNode next = l1;
            do
            {
                cahe1.Add(new ListNode(next.val));
                next = next.next;
            } while (next != null);

            List<ListNode> cahe2 = new List<ListNode>();
            next = l2;
            do
            {
                cahe2.Add(new ListNode(next.val));
                next = next.next;
            } while (next != null);

            var len = Math.Max(cahe1.Count, cahe2.Count);
            var mid = 0;
            List<ListNode> nodes = new List<ListNode>();
            for (int i = 0; i < len; i++)
            {
                var sum = (cahe1.Count > i ? cahe1[i].val : 0) +
                          (cahe2.Count > i ? cahe2[i].val : 0) +
                          mid;
                if (sum > 9)
                {
                    sum = sum - 10;
                    mid = 1;
                }
                else
                {
                    mid = 0;
                }
                nodes.Add(new ListNode(sum));
            }

            if (mid > 0)
            {
                nodes.Add(new ListNode(mid));
            }

            var res = new ListNode();
            var cacheNex = res;
            for (int i = 0; i < nodes.Count; i++)
            {
                cacheNex.val = nodes[i].val;
                if (i < (nodes.Count - 1))
                {
                    cacheNex.next = new ListNode();
                    cacheNex = cacheNex.next;
                }
                else
                {
                    cacheNex.next = null;
                }
            }

            return res;
        }

        private static ListNode Create(int[] input)
        {
            ListNode res = new ListNode();
            var mid = res;
            for (int i = 0; i < input.Length; i++)
            {
                mid.val = input[i];
                if (i != (input.Length - 1))
                {
                    mid.next = new ListNode();
                    mid = mid.next;
                }
            }

            return res;
        }

        private static void Print(ListNode input)
        {
            ListNode next = input;
            do
            {
                Console.WriteLine(next.val);
                next = next.next;
            } while (next != null);
        }
    }


    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

}
