﻿using System;
using System.IO;
using System.Linq;
using Kybus.Enigma.Padding;

namespace Kybus.Enigma.Hashing.RipeMd
{
    public class RipeMd160 : RipeMdBase
    {
        public override string Name => "RIPEMD-160";

        public override int HashLength => 160;

        public override byte[] Hash(byte[] data)
        {
            byte[] paddedInput = LengthPadding.PadToBlockSize(data, 64, 8, true);

            // Convert bytes to uint array for processing
            uint[] arr = paddedInput.UInt8ArrToUInt32ArrLE();

            // Initial Hash values
            uint[] hash =
            {
                0x67452301,
                0xEFCDAB89,
                0x98BADCFE,
                0x10325476,
                0xC3D2E1F0
            };

            // amount of blocks
            int amountOfBlocks = arr.Length / 16;

            uint[] block = new uint[16]; // Message Block
            for (int n = 0; n < amountOfBlocks; n++)
            {
                // Copy data into current message block
                Array.Copy(arr, 16 * n, block, 0, block.Length);

                // 1. Initialize the working variables:
                uint a = hash[0];
                uint b = hash[1];
                uint c = hash[2];
                uint d = hash[3];
                uint e = hash[4];

                uint _a = hash[0];
                uint _b = hash[1];
                uint _c = hash[2];
                uint _d = hash[3];
                uint _e = hash[4];

                // 2. Perform the main hash computation:
                foreach (int j in Enumerable.Range(0, 80))
                {
                    int s = _s160[j];
                    int sdash = _sDash160[j];

                    uint t = (a + F(j, b, c, d) + block[_r160[j]] + K_160(j)).RotL(s) + e;

                    a = e;
                    e = d;
                    d = c.RotL(10);
                    c = b;
                    b = t;

                    t = (_a + F(79 - j, _b, _c, _d) + block[_rDash160[j]] + KDash_160(j)).RotL(sdash) + _e;

                    _a = _e;
                    _e = _d;
                    _d = _c.RotL(10);
                    _c = _b;
                    _b = t;
                }

                // 3. Compute the intermediate hash value H(i)
                uint temp = hash[1] + c + _d;
                hash[1] = hash[2] + d + _e;
                hash[2] = hash[3] + e + _a;
                hash[3] = hash[4] + a + _b;
                hash[4] = hash[0] + b + _c;
                hash[0] = temp;
            }

            return hash.UInt32ArrToUInt8ArrLE();
        }

        public override byte[] Hash(Stream stream)
        {
            if (!stream.CanRead)
            {
                throw new IOException("Cannot read stream.");
            }

            // Initial Hash values
            uint[] hash =
            {
                0x67452301,
                0xEFCDAB89,
                0x98BADCFE,
                0x10325476,
                0xC3D2E1F0
            };

            uint[] block = new uint[16]; // Message Block

            bool lengthAppended = false;
            bool hasBeenPadded = false;
            int readByteCount;

            // Read and compute as long as the final length bytes have not yet been appended
            while (!lengthAppended)
            {
                // Read in current block and pad if necessary
                readByteCount = ReadInBlock(stream, out byte[] buffer);

                // Only add the 0x80 byte when it's not already been added
                if (readByteCount != buffer.Length && !hasBeenPadded)
                {
                    buffer[readByteCount] = 0x80; // Padding byte
                    hasBeenPadded = true;
                }

                // If there is room for the length bytes, append them ...
                // (including the padding byte in the case of the padding consists of only the padding byte)
                if (readByteCount <= 48)
                {
                    AppendLength(buffer, stream.Length, littleEndian: true);
                    lengthAppended = true; // ... and mark this block as the last
                }

                uint[] m = buffer.UInt8ArrToUInt32ArrLE(); // M_0 -> M_15, Current Block

                // Copy data into current message block
                Array.Copy(m, 0, block, 0, block.Length);

                // 1. Initialize the working variables:
                uint a = hash[0];
                uint b = hash[1];
                uint c = hash[2];
                uint d = hash[3];
                uint e = hash[4];

                uint _a = hash[0];
                uint _b = hash[1];
                uint _c = hash[2];
                uint _d = hash[3];
                uint _e = hash[4];

                // 2. Perform the main hash computation:
                foreach (int j in Enumerable.Range(0, 80))
                {
                    int s = _s160[j];
                    int sdash = _sDash160[j];

                    uint t = (a + F(j, b, c, d) + block[_r160[j]] + K_160(j)).RotL(s) + e;

                    a = e;
                    e = d;
                    d = c.RotL(10);
                    c = b;
                    b = t;

                    t = (_a + F(79 - j, _b, _c, _d) + block[_rDash160[j]] + KDash_160(j)).RotL(sdash) + _e;

                    _a = _e;
                    _e = _d;
                    _d = _c.RotL(10);
                    _c = _b;
                    _b = t;
                }

                // 3. Compute the intermediate hash value H(i)
                uint temp = hash[1] + c + _d;
                hash[1] = hash[2] + d + _e;
                hash[2] = hash[3] + e + _a;
                hash[3] = hash[4] + a + _b;
                hash[4] = hash[0] + b + _c;
                hash[0] = temp;
            }

            return hash.UInt32ArrToUInt8ArrLE();
        }
    }
}
