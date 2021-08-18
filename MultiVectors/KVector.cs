using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace MultiVectors {
    public class KVector : IEnumerable<decimal>, IEquatable<KVector> {
    #region Fields
        protected readonly Basis[] Basises;
        protected readonly decimal[] Vector;
        public int K { get; }
        public int N { get; }
        public int Length { get; }

        public decimal this[int i] {
            get => Vector[i];
            set => Vector[i] = value;
        }
    #endregion

    #region Ctor
        // k-vector in R^n
        public KVector(int k, int n) {
            if (n <= 0)
                throw new ArgumentException("n should be positive", nameof(n));
            if (k > n)
                throw new ArgumentException("k should be lesser than n", nameof(k));

            N = n;
            K = k;
            if (k == 0) {
                Basises = Array.Empty<Basis>();
                Length = 1;
                Vector = new decimal[1];
            } else {
                Basises = Basis.EnumerateFromKVectorAccessor(n, k).ToArray();
                Length = Basises.Length;
                Vector = new decimal[Length];
            }
        }

        public KVector(int k, int n, params decimal[] vector) : this(k, n) {
            if (Length != vector.Length)
                throw new ArgumentException("Length of vector is wrong", nameof(vector));
            vector.AsSpan().CopyTo(Vector.AsSpan());
        }
    #endregion

    #region Methods
        public void CopyTo(KVector vector) => AsSpan().CopyTo(vector.AsSpan());
        public Span<decimal> AsSpan() => new(Vector);
        public Basis GetBasis(int i) => Basises[i];
        public static MultiVector operator +(KVector a, KVector b) => a.AsMultiVector() + b;
        public static MultiVector operator -(KVector a, KVector b) => a.AsMultiVector() - b;

        public static MultiVector operator *(KVector a, KVector b) {
            var n = Math.Max(a.N, b.N);
            var tmp = new MultiVector(n);

            for (var i = 0; i < a.N; i++) {
                var aBasis = a.GetBasis(i);
                for (var j = 0; j < b.N; j++) {
                    var bBasis = b.GetBasis(j);
                    var abBasis = aBasis * bBasis;

                    var (k, component) = abBasis.ToKVectorAccessor();

                    var coeficient = a[i] * b[j];
                    if (abBasis.InvertSign)
                        tmp[k][component] -= coeficient;
                    else
                        tmp[k][component] += coeficient;
                }
            }

            return tmp;
        }

        public static KVector operator *(decimal a, KVector b) => b * a;

        public static KVector operator *(KVector a, decimal b) {
            var tmp = new KVector(a.K, a.N);

            for (var i = 0; i < tmp.Length; i++) tmp[i] *= b;

            return tmp;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((KVector)obj);
        }

        public override int GetHashCode() => HashCode.Combine(Vector, K, N);

        public KVector Clone() => new(K, N, Vector);

        public override string ToString() => string.Join(" ", ToStringComponents());

        public IEnumerable<string> ToStringComponents() {
            if (K == 0) {
                yield return Vector[0].ToString("+#.#;-#.#");
                yield break;
            }

            for (var i = 0; i < Vector.Length; i++) {
                var v = Vector[i];
                if (v == 0m)
                    continue;
                yield return $"{v:+#.#;-#.#}{Basises[i]}";
            }
        }
    #endregion

        public IEnumerator<decimal> GetEnumerator() => Vector.AsEnumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Equals(KVector other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Vector.SequenceEqual(other.Vector) && K == other.K && N == other.N;
        }
    }
}