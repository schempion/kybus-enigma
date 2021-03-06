﻿using System.IO;
using Kybus.Enigma.Hashing;
using Kybus.Enigma.Hashing.SecureHashingAlgorithm.Sha1;
using Kybus.Enigma.Tests.Helper;
using NUnit.Framework;

namespace Kybus.Enigma.Tests.Hashing.SecureHashingAlgorithm
{
    public class UnitTestSha1
    {
        /*
         * Test from https://www.di-mgt.com.au/sha_testvectors.html
         */

        #region Global Variables

        public Hasher Sha1;
        public TestVectorContainer<string, byte[], string, byte[]> TestVectors;
        public TestVectorContainer<string, Stream, string, byte[]> TestVectorsStream;

        #endregion

        #region Setup / Initialization

        public UnitTestSha1()
        {
            Sha1 = new Sha1();

            var oneMillionAs = new string('a', (int)1e6);

            TestVectors = new TestVectorContainer<string, byte[], string, byte[]>(Converter.Text2Bytes, Converter.HexByteDecode)
            {
                {"Sha1 abc"           , "abc", "a9993e36 4706816a ba3e2571 7850c26c 9cd0d89d"},
                {"Sha1 Empty String"  , "", "da39a3ee 5e6b4b0d 3255bfef 95601890 afd80709"},
                {"Sha1 448 Bits"      , "abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq", "84983e44 1c3bd26e baae4aa1 f95129e5 e54670f1"},
                {"Sha1 896 Bits"      , "abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmnoijklmnopjklmnopqklmnopqrlmnopqrsmnopqrstnopqrstu", "a49b2446 a02c645b f419f995 b6709125 3a04a259"},
                {"Sha1 One Million As", oneMillionAs, "34aa973c d4c4daa4 f61eeb2b dbad2731 6534016f"},
            };

            TestVectorsStream = new TestVectorContainer<string, Stream, string, byte[]>(Converter.GenerateStreamFromString, Converter.HexByteDecode)
            {
                {"Sha1-Stream abc"           , "abc", "a9993e36 4706816a ba3e2571 7850c26c 9cd0d89d"},
                {"Sha1-Stream Empty String"  , "", "da39a3ee 5e6b4b0d 3255bfef 95601890 afd80709"},
                {"Sha1-Stream 448 Bits"      , "abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq", "84983e44 1c3bd26e baae4aa1 f95129e5 e54670f1"},
                {"Sha1-Stream 896 Bits"      , "abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmnoijklmnopjklmnopqklmnopqrlmnopqrsmnopqrstnopqrstu", "a49b2446 a02c645b f419f995 b6709125 3a04a259"},
                {"Sha1-Stream One Million As", oneMillionAs, "34aa973c d4c4daa4 f61eeb2b dbad2731 6534016f"},
            };
        }

        #endregion

        #region SHA1

        [Test(Description = "SHA-1: \"abc\"")]
        public void Sha1_abc()
        {
            var (data, expected) = TestVectors["Sha1 abc"];

            var hash = Sha1.Hash(data);
            CustomAssert.MatchArrays(hash, expected);
        }

        [Test(Description = "SHA-1: Empty String")]
        public void Sha1_EmptyString()
        {
            var (data, expected) = TestVectors["Sha1 Empty String"];

            var hash = Sha1.Hash(data);
            CustomAssert.MatchArrays(hash, expected);
        }

        [Test(Description = "SHA-1: 448 Bits")]
        public void Sha1_448Bits()
        {
            var (data, expected) = TestVectors["Sha1 448 Bits"];

            var hash = Sha1.Hash(data);
            CustomAssert.MatchArrays(hash, expected);
        }

        [Test(Description = "SHA-1: 896 Bits")]
        public void Sha1_896Bits()
        {
            var (data, expected) = TestVectors["Sha1 896 Bits"];

            var hash = Sha1.Hash(data);
            CustomAssert.MatchArrays(hash, expected);
        }

        [Test(Description = "SHA-1: One Million times 'a'")]
        public void Sha1_OneMillionSmallAs()
        {
            var (data, expected) = TestVectors["Sha1 One Million As"];

            var hash = Sha1.Hash(data);
            CustomAssert.MatchArrays(hash, expected);
        }

        #endregion

        #region SHA1-Stream

        [Test(Description = "SHA-1-Stream: \"abc\"")]
        public void Sha1Stream_abc()
        {
            var (data, expected) = TestVectorsStream["Sha1-Stream abc"];

            var hash = Sha1.Hash(data);
            CustomAssert.MatchArrays(hash, expected);
        }

        [Test(Description = "SHA-1-Stream: Empty String")]
        public void Sha1Stream_EmptyString()
        {
            var (data, expected) = TestVectorsStream["Sha1-Stream Empty String"];

            var hash = Sha1.Hash(data);
            CustomAssert.MatchArrays(hash, expected);
        }

        [Test(Description = "SHA-1-Stream: 448 Bits")]
        public void Sha1Stream_448Bits()
        {
            var (data, expected) = TestVectorsStream["Sha1-Stream 448 Bits"];

            var hash = Sha1.Hash(data);
            CustomAssert.MatchArrays(hash, expected);
        }

        [Test(Description = "SHA-1-Stream: 896 Bits")]
        public void Sha1Stream_896Bits()
        {
            var (data, expected) = TestVectorsStream["Sha1-Stream 896 Bits"];

            var hash = Sha1.Hash(data);
            CustomAssert.MatchArrays(hash, expected);
        }

        [Test(Description = "SHA-1-Stream: One Million times 'a'")]
        public void Sha1Stream_OneMillionSmallAs()
        {
            var (data, expected) = TestVectorsStream["Sha1-Stream One Million As"];

            var hash = Sha1.Hash(data);
            CustomAssert.MatchArrays(hash, expected);
        }

        #endregion
    }
}
