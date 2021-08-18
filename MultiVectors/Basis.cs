using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MultiVectors {
    public class Basis : IEquatable<Basis> {
    #region Fields
        protected readonly BitArray BitMap;
        public int N { get; }
        public int K { get; protected set; }
        public int Length { get; }
        public bool InvertSign { get; protected set; }

        public IEnumerable<int> UnitBasisIndexes {
            get {
                for (var i = 0; i < BitMap.Count; i += 2)
                    if (BitMap[i])
                        yield return i >> 1;
            }
        }

        protected (bool, bool) this[int i] {
            get {
                var ind = i << 1;
                return (BitMap[ind], BitMap[ind + 1]);
            }
            set {
                var ind = i << 1;
                (BitMap[ind], BitMap[ind + 1]) = value;
            }
        }

        protected bool this[int i, AccessorType type] {
            get {
                var ind = (i << 1) + (int)type;
                return BitMap[ind];
            }
            set {
                var ind = (i << 1) + (int)type;
                BitMap[ind] = value;
            }
        }
    #endregion

    #region Ctor
        public Basis(int n) {
            N = n;
            Length = N << 1;
            BitMap = new BitArray(Length);
        }

        protected Basis(int n, int k, bool invertSign, BitArray bitMap) {
            N = n;
            K = k;
            InvertSign = invertSign;
            Length = N << 1;
            BitMap = bitMap;
        }

        public Basis(int n, params int[] basis) : this(n) {
            foreach (var v in basis) Toggle(v);
        }
    #endregion

    #region Methods
        public static Basis operator *(Basis b1, Basis b2) {
            var n = Math.Max(b1.N, b2.N);
            var tmp = b1.Clone();
            tmp.InvertSign ^= b2.InvertSign;

            foreach (var i in b2.UnitBasisIndexes) tmp.Toggle(i);

            return tmp;
        }

        public void Toggle(int i) {
            var presence = this[i, AccessorType.Presence] = !this[i, AccessorType.Presence];

            if (presence) K++;
            else K--;

            if (this[i, AccessorType.Parity])
                InvertSign = !InvertSign;

            for (var j = 1; j < 2 * i; j += 2)
                BitMap[j] = !BitMap[j];
        }

        public Basis Clone() {
            var tmp = new Basis(N, K, InvertSign, new BitArray(BitMap));

            return tmp;
        }

        public static IEnumerable<Basis> EnumerateFromKVectorAccessor(int n, int k) =>
            (n, k).EnumerateCombinations().Select(combination => new Basis(n, combination));

        public override string ToString() =>
            (InvertSign ? "-" : "") + string.Join("", UnitBasisIndexes.Select(x => $"e{x.ToString()}"));

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Basis)obj);
        }

        public override int GetHashCode() => HashCode.Combine(BitMap, N, K, InvertSign);
    #endregion

        public bool Equals(Basis other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return BitMap.Xor(other.BitMap).Cast<bool>().Any()
                   && N == other.N
                   && K == other.K
                   && InvertSign == other.InvertSign;
        }

        public enum AccessorType : byte {
            Presence = 0,
            Parity = 1,
        }
    }
}