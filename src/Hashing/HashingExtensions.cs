﻿using System;
using System.Linq;

namespace KybusEnigma.Hashing
{
    internal static class HashingExtensions
    {
        #region ULong Operations

        public static ulong RotR  (this ulong x, int n) => (x >> n) | (x << (64 - n));
        public static ulong RotL  (this ulong x, int n) => (x << n) | (x >> (64 - n));
        public static ulong ShiftR(this ulong x, int n) => (x >> n);
        public static ulong ShiftL(this ulong x, int n) => (x << n);

        #endregion

        #region UInt Operations

        public static uint RotR   (this uint x, int n) => (x >> n) | (x << (32 - n));
        public static uint RotL   (this uint x, int n) => (x << n) | (x >> (32 - n));
        public static uint ShiftR (this uint x, int n) => (x >> n);
        public static uint ShiftL (this uint x, int n) => (x << n);

        #endregion

        #region Long Operations

        public static long RotR  (this long x, int n) => (x >> n) | (x << (64 - n));
        public static long RotL  (this long x, int n) => (x << n) | (x >> (64 - n));
        public static long ShiftR(this long x, int n) => (x >> n);
        public static long ShiftL(this long x, int n) => (x << n);

        #endregion

        #region Int Operations

        public static int RotR  (this int x, int n) => (x >> n) | (x << (32 - n));
        public static int RotL  (this int x, int n) => (x << n) | (x >> (32 - n));
        public static int ShiftR(this int x, int n) => (x >> n);
        public static int ShiftL(this int x, int n) => (x << n);

        #endregion

        #region Byte Operations

        public static byte RotR  (this byte x, int n) => (byte) ((x >> n) | (x << (8 - n)));
        public static byte RotL  (this byte x, int n) => (byte) ((x << n) | (x >> (8 - n)));
        public static byte ShiftR(this byte x, int n) => (byte) (x >> n);
        public static byte ShiftL(this byte x, int n) => (byte) (x << n);

        #endregion

        #region Conversion

        public static uint[] BytesArr2UIntArr(this byte[] arr)
        {
            var length = (int)Math.Ceiling(arr.Length / 4.0);
            return Enumerable
                .Range(0, length)
                .Select(i => arr.BytesArr2UInt(i * 4))
                .ToArray();
        }

        public static uint[] BytesArr2UIntArrLittleEndian(this byte[] arr)
        {
            var length = (int)Math.Ceiling(arr.Length / 4.0);
            return Enumerable
                .Range(0, length)
                .Select(i => BitConverter.ToUInt32(arr, i * 4))
                .ToArray();
        }

        public static ulong[] BytesArr2ULongArr(this byte[] arr)
        {
            var length = (int)Math.Ceiling(arr.Length / 8.0);
            return Enumerable
                .Range(0, length)
                .Select(i => arr.BytesArr2ULong(i * 8))
                .ToArray();
        }

        public static ulong BytesArr2ULong(this byte[] buffer, int offset)
        {
            if (buffer.Length % 8 != 0)
                throw new ArgumentException("Input array's length is not a multiple of 8!");

            var output = 0ul;

            for (var i = 0; i < 8; i++)
            {
                output <<= 8;
                output |= buffer[offset + i];
            }

            return output;
        }

        public static uint BytesArr2UInt(this byte[] buffer, int offset)
        {
            if (buffer.Length % 4 != 0)
                throw new ArgumentException("Input array's length is not a multiple of 4!");

            var output = 0u;

            for (var i = 0; i < 4; i++)
            {
                output <<= 8;
                output |= buffer[offset + i];
            }

            return output;
        }

        public static byte[] Long2BytesArr(this long l) => BitConverter.GetBytes(l).Reverse().ToArray();

        public static byte[] Long2BytesArrLittleEndian(this long l) => BitConverter.GetBytes(l).ToArray();

        public static byte[] UInt2BytesArr(this uint i) => BitConverter.GetBytes(i).Reverse().ToArray();

        public static byte[] UInt2BytesArrLittleEndian(this uint i) => BitConverter.GetBytes(i).ToArray();

        public static byte[] UIntsArr2BytesArr(this uint[] arr)
        {
            var output = new byte[arr.Length * 4];

            for (var i = 0; i < arr.Length; i++)
            {
                var bytes = UInt2BytesArr(arr[i]);
                Array.Copy(bytes, 0, output, i * 4, bytes.Length);
            }

            return output;
        }

        public static byte[] UIntsArr2BytesArrLittleEndian(this uint[] arr)
        {
            var output = new byte[arr.Length * 4];

            for (var i = 0; i < arr.Length; i++)
            {
                var bytes = UInt2BytesArrLittleEndian(arr[i]);
                Array.Copy(bytes, 0, output, i * 4, bytes.Length);
            }

            return output;
        }

        public static byte[] ULongsArr2BytesArr(this ulong[] arr)
        {
            var output = new byte[arr.Length * 8];

            for (var i = 0; i < arr.Length; i++)
            for (var offset = 0; offset < 8; offset++)
                output[8 * i + offset] = (byte) (arr[i] >> (7 - offset) * 8);

            return output;
        }

        #endregion
    }
}
