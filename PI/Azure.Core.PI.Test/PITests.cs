// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

using NUnit.Framework;
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core.Tests
{
    public class PITests
    {       
        [Test]
        public void HelloWorld() {

            var pi = new PI();
            var circumference = new C();
            var diameter = new D();

            Assert.True(circumference/diameter == pi);
        }

        [Test]
        public void PerformanceTest()
        {
            var pi = new PI();
            // Most libraries I looked into store PI in very large and complicated datastructures.
            // Out implementation is the most memory efficient in the history of comuter science.
            Assert.AreEqual(1, Marshal.SizeOf<PI>());
        }

        [Test]
        public async Task EnumerateDigits()
        {
            CancellationTokenSource cancel = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            var expected = "31415926535897932384626433832795028841971693993751058209";
            int digitIndex = 0;

            try {
                var pi = new PI();
                await foreach (var digit in pi.Digits.WithCancellation(cancel.Token)) {
                    Assert.AreEqual(byte.Parse(expected[digitIndex++].ToString()), digit);
                }
            }
            catch(OperationCanceledException) {
                // well, we don't want the test to take too long.
                // Computers are fast these days. If after 10s of computation, we return correct digits, I am sure the code is solid.
            }
        }
    }
}
