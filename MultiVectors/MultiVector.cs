using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MultiVectors {
    public class MultiVector : IEnumerable<KVector> {
    #region Fields
        protected readonly KVector[] KVectors;
        public int N { get; }
        public int Length { get; }

        public KVector this[int k] {
            get => KVectors[k];
            set => KVectors[k] = value;
        }
    #endregion

    #region Ctor
        // Multivector in G^n
        public MultiVector(int n) {
            if (n <= 0)
                throw new ArgumentException("n should be positive", nameof(n));
            N = n;
            Length = n + 1;
            KVectors = new KVector[Length];
            for (var i = 0; i < Length; i++)
                KVectors[i] = new KVector(i, n);
        }
    #endregion

    #region Methods
        private MultiVector Clone() {
            var tmp = new MultiVector(N);
            for (var i = 0; i < tmp.Length; i++) {
                var tmpVector = tmp[i];
                var kVector = KVectors[i];
                kVector.CopyTo(tmpVector);
            }

            return tmp;
        }

        public static MultiVector operator +(MultiVector a, KVector b) {
            var tmp = a.Clone();
            for (var i = 0; i < b.N; i++)
                tmp[b.K][i] += b[i];
            return tmp;
        }

        public static MultiVector operator -(MultiVector a, KVector b) {
            var tmp = a.Clone();
            for (var i = 0; i < b.N; i++)
                tmp[b.K][i] -= b[i];
            return tmp;
        }

        public static MultiVector operator +(MultiVector a, MultiVector b) {
            var n = Math.Max(a.N, b.N);
            var tmp = new MultiVector(n);
            for (var i = 0; i < n + 1; i++) {
                var tmpVector = tmp[i];
                KVector aVector;
                KVector bVector;
                if (i >= b.Length) {
                    aVector = a[i];
                    for (var j = 0; j < tmpVector.Length; j++)
                        if (j < aVector.Length)
                            tmpVector[j] += aVector[j];
                } else if (i >= a.Length) {
                    bVector = b[i];
                    for (var j = 0; j < tmpVector.Length; j++)
                        if (j < bVector.Length)
                            tmpVector[j] += bVector[j];
                } else {
                    aVector = a[i];
                    bVector = b[i];

                    for (var j = 0; j < tmpVector.Length; j++) {
                        if (j < aVector.Length)
                            tmpVector[j] += aVector[j];
                        if (j < bVector.Length)
                            tmpVector[j] += bVector[j];
                    }
                }
            }

            return tmp;
        }

        public static MultiVector operator -(MultiVector a, MultiVector b) {
            var n = Math.Max(a.N, b.N);
            var tmp = new MultiVector(n);
            for (var i = 0; i < n + 1; i++) {
                var tmpVector = tmp[i];
                KVector aVector;
                KVector bVector;
                if (i >= b.Length) {
                    aVector = a[i];
                    for (var j = 0; j < tmpVector.Length; j++)
                        if (j < aVector.Length)
                            tmpVector[j] += aVector[j];
                } else if (i >= a.Length) {
                    bVector = b[i];
                    for (var j = 0; j < tmpVector.Length; j++)
                        if (j < bVector.Length)
                            tmpVector[j] -= bVector[j];
                } else {
                    aVector = a[i];
                    bVector = b[i];

                    for (var j = 0; j < tmpVector.Length; j++) {
                        if (j < aVector.Length)
                            tmpVector[j] += aVector[j];
                        if (j < bVector.Length)
                            tmpVector[j] -= bVector[j];
                    }
                }
            }

            return tmp;
        }

        public static MultiVector operator *(decimal a, MultiVector b) => b * a;

        public static MultiVector operator *(MultiVector a, decimal b) {
            var tmp = new MultiVector(a.N);
            for (var i = 0; i < a.Length; i++)
                tmp[i] = a[i] * b;

            return tmp;
        }

        public static MultiVector operator *(MultiVector a, MultiVector b) {
            var n = Math.Max(a.N, b.N);
            var tmp = new MultiVector(n);

            foreach (var av in a)
            foreach (var bv in b)
                tmp += av * bv;

            return tmp;
        }

        public static MultiVector operator *(MultiVector a, KVector bv) {
            var n = Math.Max(a.N, bv.N);
            var tmp = new MultiVector(n);

            foreach (var av in a)
                tmp += av * bv;

            return tmp;
        }

        public static MultiVector operator *(KVector av, MultiVector b) {
            var n = Math.Max(av.N, b.N);
            var tmp = new MultiVector(n);

            foreach (var bv in b)
                tmp += av * bv;

            return tmp;
        }

        public override string ToString() => string.Join(" ", KVectors.SelectMany(x => x.ToStringComponents()));
    #endregion

        public IEnumerator<KVector> GetEnumerator() => KVectors.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}