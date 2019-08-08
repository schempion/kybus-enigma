﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KybusEnigma.Lib.Hashing.RipeMd
{
    public class RipeMd160 : RipeMdBase
    {
        public override string Name => "RIPEMD-160";

        public override int HashLength => 160;

        public override byte[] Hash(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override byte[] Hash(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
