// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

using System.Threading;
using System.Threading.Tasks;

namespace System.Numerics
{
    public struct PI
    {
        public static bool operator ==(PI left, PI right) => true;
        public static bool operator !=(PI left, PI right) => false;

        public override string ToString() => "3.14...";

        public PIDigits Digits => new PIDigits();
    }

    public static class NumerocsExtensions
    {
        public static PI DivideBy(this C left, D right) => new PI();
    }

    // circumference of a circle
    public ref struct C
    {
        public static PI operator /(C c, D d) => new PI();
    }

    // diameter of a circle
    public ref struct D
    { }

    public class PIDigits
    {
        int _digit;
        CancellationToken _cancellation;

        public async ValueTask<bool> MoveNextAsync() {
            if(_digit < 3) {
                _digit++;
                return true;
            }
            else {
                var task = Task.Delay(TimeSpan.FromTicks(int.MaxValue), _cancellation);
                await task;
                if (task.IsCanceled) throw new OperationCanceledException();
                else throw new NotImplementedException();
            }
        }

        public byte Current {
            get {
                if (_digit == 1) return 3;
                if (_digit == 2) return 1;
                if (_digit == 3) return 4;
                throw new NotImplementedException();
            }
        }

        public PIDigits WithCancellation(CancellationToken cancellation)
        {
            _cancellation = cancellation;
            return this;
        }

        public PIDigits GetAsyncEnumerator(CancellationToken cancellationToken = default) => this;
    }
}
