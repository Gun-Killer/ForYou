using System;

namespace TwoSum
{
    /// <summary>
    /// https://leetcode.com/problems/two-sum/
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var nums = new int[]{ 3,3 };
            int target = 6;
            var result = TwoSum(nums, target);
            foreach (var i in result)
            {
                Console.Write(i);
            }
        }

        private static int[] TwoSum(int[] nums, int target)
        {
            var result = new int[2];
            var len = nums.Length;
            for (int i = 0; i < len-1; i++)
            {
                for (int j = i+1; j < len; j++)
                {
                    if ((nums[i] + nums[j]) == target)
                    {
                        result[0] = i;
                        result[1] = j;
                    }
                }
            }

            return result;
        }
    }
}
