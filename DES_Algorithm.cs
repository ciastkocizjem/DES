using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    public static class DES_Algorithm
    {
        #region Tables
        private static readonly int[] PC_1 = {57, 49, 41, 33, 25, 17, 9,
                                                1, 58, 50, 42, 34, 26, 18,
                                                10, 2, 59, 51, 43, 35, 27,
                                                19, 11, 3, 60, 52, 44, 36,
                                                63, 55, 47, 39, 31, 23, 15,
                                                7, 62, 54, 46, 38, 30, 22,
                                                14, 6, 61, 53, 45, 37, 29,
                                                21, 13, 5, 28, 20, 12, 4};
        private static readonly int[] PC_2 = {14, 17, 11, 24, 1, 5,
                                                3, 28, 15, 6, 21, 10,
                                                23, 19, 12, 4, 26, 8,
                                                16, 7, 27, 20, 13, 2,
                                                41, 52, 31, 37, 47, 55,
                                                30, 40, 51, 45, 33, 48,
                                                44, 49, 39, 56, 34, 53,
                                                46, 42, 50, 36, 29, 32};

        private static readonly int[] IP = {58, 50, 42, 34, 26, 18, 10, 2,
                                            60, 52, 44, 36, 28, 20, 12, 4,
                                            62, 54, 46, 38, 30, 22, 14, 6,
                                            64, 56, 48, 40, 32, 24, 16, 8,
                                            57, 49, 41, 33, 25, 17,  9, 1,
                                            59, 51, 43, 35, 27, 19, 11, 3,
                                            61, 53, 45, 37, 29, 21, 13, 5,
                                            63, 55, 47, 39, 31, 23, 15, 7}, 
                                    IP_1 = {40, 8, 48, 16, 56, 24, 64, 32,
                                            39, 7, 47, 15, 55, 23, 63, 31,
                                            38, 6, 46, 14, 54, 22, 62, 30,
                                            37, 5, 45, 13, 53, 21, 61, 29,
                                            36, 4, 44, 12, 52, 20, 60, 28,
                                            35, 3, 43, 11, 51, 19, 59, 27,
                                            34, 2, 42, 10, 50, 18, 58, 26,
                                            33, 1, 41, 9, 49, 17, 57, 25 };


        private static readonly int[] leftShift = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        private static readonly int[] EBit_Selection = {32, 1, 2, 3, 4, 5,
                                                        4, 5, 6, 7, 8, 9,
                                                        8, 9, 10, 11, 12, 13,
                                                        12, 13, 14, 15, 16, 17,
                                                        16, 17, 18, 19, 20, 21,
                                                        20, 21, 22, 23, 24, 25,
                                                        24, 25, 26, 27, 28, 29,
                                                        28, 29, 30, 31, 32, 1};
        private static readonly int[,] S1 = {{14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
                                            { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
                                            { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
                                            { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 }}, 
                                        S2 = {{15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 },
                                            { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
                                            { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
                                            { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 } }, 
                                        S3 = {{10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
                                            { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
                                            { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
                                            { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12} },
                                        S4 = {{7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
                                            { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 },
                                            { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
                                            { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14} }, 
                                        S5 = { {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
                                            { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 },
                                            { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
                                            { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3} }, 
                                        S6 = {{12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
                                            { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 },
                                            { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
                                            { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 } }, 
                                        S7 = {{4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
                                            { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 },
                                            { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
                                            { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0 ,15, 14, 2, 3, 12 } }, 
                                        S8 = {{13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
                                            { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 },
                                            { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
                                            { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 } };

        private static readonly int[] P = {16, 7, 20, 21,
                                            29, 12, 28, 17,
                                            1, 15, 23, 26,
                                            5, 18, 31, 10,
                                            2, 8, 24, 14,
                                            32, 27, 3, 9,
                                            19, 13, 30, 6,
                                            22, 11, 4, 25};

        #endregion

        #region Utilities
        private static int[] ToArray(string s)
        {
            int[] ints = s.ToCharArray().Where(x => int.TryParse(x.ToString(), out int myInt)).Select(x => int.Parse(x.ToString())).ToArray();
            return ints;
        }

        private static string FromArrayToString(int[] array)
        {
            return string.Join("", array);
        }

        private static int[] ShiftToLeft(int[] toShift, int shiftCount)
        {
            int[] shifted = new int[toShift.Length];
            for (int j = 0; j < shiftCount; j++)
            {
                if (j == 1)
                {
                    shifted.CopyTo(toShift, 0);
                }

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

            // Split 56-bit key into two 28-bit arrays
            int[] C = new int[28], 
                D = new int[28], 
                CD = new int[56];
            Array.Copy(key56B, 0, C, 0, 28);
            Array.Copy(key56B, 28, D, 0, 28);

            List<int[]> joinedPermutatedKeys = new List<int[]>(); // List of combined C and D permuted with PC_2 (Kn)

            // Shifting C and D, joining them and permuting 
            for (int i = 0; i < 16; i++)
            {
                C = ShiftToLeft(C, leftShift[i]);
                D = ShiftToLeft(D, leftShift[i]);

                CD = C.Concat(D).ToArray();
                int[] CD48B = new int[48];
                for (int j = 0; j < CD48B.Length; j++)
                {
                    CD48B[j] = CD[PC_2[j] - 1];
                }

                joinedPermutatedKeys.Add(CD48B);
            }

            // Permuting message
            int[] messageIP = new int[64];
            for (int i = 0; i < messageIP.Length; i++)
            {
                messageIP[i] = binaryMessage[IP[i] - 1];
            }

            // Splitting message into two 32-bit arrays
            int[] Lprev = new int[32],  // Ln-1 (initially L0)
                Rprev = new int[32];    // Rn-1 (initially R0)
                
            Array.Copy(messageIP, 0, Lprev, 0, 32);
            Array.Copy(messageIP, 32, Rprev, 0, 32);

            int[] E = new int[48]; // E(Rn-1)
            for (int i = 0; i < 16; i++)
            {
                int[] L = new int[32], R = new int[32], xored = new int[48]; // Kn+E(Rn-1)
                L = Rprev;

                // Computing E(Rn-1)
                for (int j = 0; j < E.Length; j++)
                {
                    E[j] = L[EBit_Selection[j] - 1];
                }

                // Xoring E(Rn-1) with Kn
                for (int j = 0; j < E.Length; j++)
                {
                    xored[j] = E[j] ^ joinedPermutatedKeys.ElementAt(i)[j];
                }

                int packageIndex = 0, valueDecimal = 0;
                string valueBinary = "", sString = "";
                int[] S = new int[32]; // xored after converting based on Sn tables
                // Converting 48-bit xored to 32-bit
                for (int j = 0; j < xored.Length; j+=6)
                {
                    // Getting row and column index in binary from package of 6 bits
                    string rowIndexBin = xored[j].ToString() + xored[j + 5].ToString(), 
                        columnIndexBin = FromArrayToString(xored.Skip(j + 1).Take(4).ToArray());
                    // Converting indexes to decimal
                    int rowIndex = Convert.ToInt32(rowIndexBin, 2), 
                        columnIndex = Convert.ToInt32(columnIndexBin, 2);

                    // Selecting approprate array
                    switch (packageIndex)
                    {
                        case 0:
                            valueDecimal = S1[rowIndex, columnIndex];
                            break;
                        case 1:
                            valueDecimal = S2[rowIndex, columnIndex];
                            break;
                        case 2:
                            valueDecimal = S3[rowIndex, columnIndex];
                            break;
                        case 3:
                            valueDecimal = S4[rowIndex, columnIndex];
                            break;
                        case 4:
                            valueDecimal = S5[rowIndex, columnIndex];
                            break;
                        case 5:
                            valueDecimal = S6[rowIndex, columnIndex];
                            break;
                        case 6:
                            valueDecimal = S7[rowIndex, columnIndex];
                            break;
                        case 7:
                            valueDecimal = S8[rowIndex, columnIndex];
                            break;
                    }
                    valueBinary = Convert.ToString(valueDecimal, 2).PadLeft(4, '0');
                    sString += valueBinary;
                    packageIndex++;
                    valueBinary = "";
                }
                S = ToArray(sString);

                // R = L + .... 
            }

            return null;
        }
    }
}
