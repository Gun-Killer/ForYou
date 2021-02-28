using System;
using System.Collections.Generic;

namespace AddTwoNumbers
{
    class Program
    {
        /// <summary>
        /// https://leetcode.com/problems/add-two-numbers/
        /// </summary>
        /// <param name="args"></param>
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
            ListNode res = null;
            ListNode next = null;
            var l1Nex = l1;
            var l2Nex = l2;
            var pre = 0;
            while (l1Nex != null || l2Nex != null || pre != 0)
            {
                var sum = (l1Nex?.val ?? 0) + (l2Nex?.val ?? 0) + pre;
                if (sum > 9)
                {
                    sum = sum - 10;
                    pre = 1;
                }
                else
                {
                    pre = 0;
                }

                if (next == null)
                {
                    res = new ListNode(sum, null);
                    next = res;
                }
                else
                {
                    next.next = new ListNode(sum, null);
                    next = next.next;
                }


                l1Nex = l1Nex?.next;
                l2Nex = l2Nex?.next;
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
