﻿using System;

namespace KybusEnigma.Lib.Hashing.MessageDigest
{
    public abstract class MessageDigestBase : Hasher
    {
        protected byte[] PadMd4Md5(byte[] buffer)
        {
            if (buffer == null)
                buffer = new byte[0];

            var newArrayLength = CalcNewArrayLength(buffer.Length); // Not including padding bytes
            var outputArray = new byte[newArrayLength + 8];

            // copy exisiting stuff into new array
            Array.Copy(buffer, 0, outputArray, 0, buffer.Length);

            // pad first with a 1 bit / 0x80 byte, rest is already filled with \0 bytes
            outputArray[buffer.Length] = 0x80;

            // append the length bytes to the output array in LE
            AppendLength(outputArray, buffer.GetLongLength(0), true);

            return outputArray;
        }

        private static int CalcNewArrayLength(int length)
        {
            // If array length is already 56 bytes, there is no space left for the padding byte, so append 64 bytes
            if (length % 64 == 56)
                return length + 64;

            while (length % 64 != 56) length++; // TODO: Improve
            return length;
        }

        #region Md5 Constants & Functions

        protected uint F(uint x, uint y, uint z) => (x & y) | (~x & z);
        protected uint G(uint x, uint y, uint z) => (x & z) | (y & ~z);
        protected uint H(uint x, uint y, uint z) => (x ^ y ^ z);
        protected uint I(uint x, uint y, uint z) => y ^ (x & ~z);

        protected uint[] K =
        {
            0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee,
            0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501,
            0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be,
            0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821,

            0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa,
            0xd62f105d, 0x2441453,  0xd8a1e681, 0xe7d3fbc8,
            0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed,
            0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a,

            0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c,
            0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70,
            0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x4881d05 ,
            0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665,

            0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039,
            0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1,
            0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1,
            0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391
        };

        protected int[] S_Md5 =
        {
            7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22,
            5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,
            4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,
            6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21,
        };

        protected int[] S_Md4 =
        {
            3, 7, 11, 19,  3, 7, 11, 19,  3, 7, 11, 19,  3, 7, 11, 19,
            3, 5,  9, 13,  3, 5,  9, 13,  3, 5,  9, 13,  3, 5,  9, 13,
            3, 9, 11, 15,  3, 9, 11, 15,  3, 9, 11, 15,  3, 9, 11, 15
        };

        protected int[] G_Md4 =
        {
            0, 1, 2,  3, 4,  5, 6,  7, 8, 9, 10, 11, 12, 13, 14, 15,
            0, 4, 8, 12, 1,  5, 9, 13, 2, 6, 10, 14,  3,  7, 11, 15,
            0, 8, 4, 12, 2, 10, 6, 14, 1, 9,  5, 13,  3, 11,  7, 15
        };

        #endregion
    }
}
