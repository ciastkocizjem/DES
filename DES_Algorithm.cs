using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    public static class DES_Algorithm
    {
        private static readonly int[] PC_1 = {57, 49, 41, 33, 25, 17, 9,
                                                1, 58, 50, 42, 34, 26, 18,
                                                10, 2, 59, 51, 43, 35, 27,
                                                19, 11, 3, 60, 52, 44, 36,
                                                63, 55, 47, 39, 31, 23, 15,
                                                7, 62, 54, 46, 38, 30, 22,
                                                14, 6, 61, 53, 45, 37, 29,
                                                21, 13, 5, 28, 20, 12, 4};

        private static readonly int[] leftShift = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        #region Utilities
        private static int[] ToArray(string s)
        {
            int[] ints = s.ToCharArray().Where(x => int.TryParse(x.ToString(), out int myInt)).Select(x => int.Parse(x.ToString())).ToArray();
            return ints;
        }

        private static int[] ShiftToLeft(int[] toShift, int shiftCount)
        {
            int[] shifted = new int[toShift.Length];
            for (int j = 0; j < shiftCount; j++)
            {
                for (int i = 0; i < toShift.Length; i++)
                {
                    if (i == toShift.Length - 1)
                    {
                        shifted[i] = toShift.First();
                    }
                    else
                    {
                        shifted[i] = toShift[i + 1];
                    }
                }

                if (shiftCount > 1)
                {
                    toShift = shifted;
                }
            }
            return shifted;
        }

        #endregion

        public static string Encoding(string message, string key)
        {
            // Convert hexadecimal input to binary 
            string binaryMsg = "", binaryKy = "";
            foreach(char c in message)
            {
                binaryMsg += Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0');
            }

            foreach(char c in key)
            {
                binaryKy += Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0');
            }

            int[] binaryMessage = ToArray(binaryMsg), // M
                binaryKey = ToArray(binaryKy); // K

            // Create 56-bit long key
            int[] key56B = new int[56]; // 56-bit long permutated key (K+)

            for(int i = 0; i < key56B.Length; i++)
            {
                key56B[i] = binaryKey[PC_1[i] - 1];
            }

            // Split 56-bit key into to 28-bit arrays
            int[] C = new int[28], D = new int[28];
            Array.Copy(key56B, 0, C, 0, 28);
            Array.Copy(key56B, 28, D, 0, 28);

            // Shift C i D
            for (int i = 0; i < 16; i++)
            {
                C = ShiftToLeft(C, leftShift[i]);
                D = ShiftToLeft(D, leftShift[i]);
            }

            return null;
        }
    }
}
