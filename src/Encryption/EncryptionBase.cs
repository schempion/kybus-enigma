﻿using System.IO;

namespace KybusEnigma.Encryption
{
    public abstract class EncryptionBase
    {
        public abstract byte[] Encrypt(byte[] plainText);
        public abstract byte[] Decrypt(byte[] cipherText);

        public abstract byte[] Encrypt(Stream stream);
        public abstract byte[] Decrypt(Stream stream);
    }
}
