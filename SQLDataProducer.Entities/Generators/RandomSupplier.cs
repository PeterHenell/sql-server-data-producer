// Copyright 2012-2014 Peter Henell

//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//       http://www.apache.org/licenses/LICENSE-2.0

//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System;

namespace SQLDataProducer.Entities.Generators
{
    public sealed class RandomSupplier
    {
        static readonly RandomSupplier instance = new RandomSupplier();

        public static RandomSupplier Instance
        {
            get
            {
                return instance;
            }
        }

        static RandomSupplier()
        {
        }

        RandomSupplier()
        {
            _random = new Random((int)System.DateTime.Now.Ticks % System.Int32.MaxValue);
        }

        public int GetNextInt()
        {
            lock (_random)
            {
                return _random.Next();
            }
        }
        public int GetNextInt(int min, int max)
        {
            lock (_random)
            {
                return _random.Next(min, max);
            }
        }
        public long GetNextLong()
        {
            //http://social.msdn.microsoft.com/Forums/en-US/csharpgeneral/thread/2e08381b-1e2d-459f-a7c9-986954321958/
            lock (_random)
            {
                return (long)((_random.NextDouble() * 2.0 - 1.0) * long.MaxValue);
            }
        }

        public long GetNextLong(long min, long max)
        {
            lock (_random)
            {
                byte[] buf = new byte[8];
                _random.NextBytes(buf);
                long longRand = BitConverter.ToInt64(buf, 0);

                return (Math.Abs(longRand % (max - min)) + min);
            }
        }

        public decimal GetNextDecimal()
        {
            lock (_random)
            {
                return _random.NextDecimal();
            }
        }

        public decimal GetNextDecimal(decimal from, decimal to)
        {
            lock (_random)
            {
                // http://stackoverflow.com/questions/609501/generating-a-random-decimal-in-c-sharp?lq=1
                byte fromScale = new System.Data.SqlTypes.SqlDecimal(from).Scale;
                byte toScale = new System.Data.SqlTypes.SqlDecimal(to).Scale;
                
                byte scale = (byte)(fromScale + toScale);
                if (scale > 28)
                    scale = 28;

                decimal r = new decimal(_random.Next(), _random.Next(), _random.Next(), false, scale);
                if (Math.Sign(from) == Math.Sign(to) || from == 0 || to == 0)
                    return decimal.Remainder(r, to - from) + from;

                bool getFromNegativeRange = (double)from + _random.NextDouble() * ((double)to - (double)from) < 0;
                return getFromNegativeRange ? decimal.Remainder(r, -from) + from : decimal.Remainder(r, to);
            }
        }


        public double GetNextDouble()
        {
            lock (_random)
            {
                return _random.NextDouble();
            }
        }
        readonly Random _random;

    }

    public static class RandomExtensions
    {
        public static int NextInt32(this Random rng)
        {
            unchecked
            {
                int firstBits = rng.Next(0, 1 << 4) << 28;
                int lastBits = rng.Next(0, 1 << 28);
                return firstBits | lastBits;
            }
        }

        public static decimal NextDecimal(this Random rng)
        {
            byte scale = (byte)rng.Next(29);
            bool sign = rng.Next(2) == 1;
            return new decimal(rng.NextInt32(),
                               rng.NextInt32(),
                               rng.NextInt32(),
                               sign,
                               scale);
        }
    }
}
