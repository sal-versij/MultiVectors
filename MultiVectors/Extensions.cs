using System.Collections.Generic;
using System.Linq;

namespace MultiVectors {
    public static class Extensions {
    #region Methods
        public static MultiVector AsMultiVector(this KVector kVector) {
            var mVector = new MultiVector(kVector.N);
            for (var i = 0; i < kVector.N; i++)
                mVector[kVector.K][i] = kVector[i];

            return mVector;
        }

        public static (int, int) ToKVectorAccessor(this Basis b) {
            //https://math.stackexchange.com/questions/1363239/fast-way-to-get-a-position-of-combination-without-repetitions
            var indexes = b.UnitBasisIndexes.ToArray();
            //  |01234| is|(n k) - sum[(n-1-v k-i)]
            // 0|xxx  |012|9 - [(4 3) + (3 2) + (2 1)] = 9 - (4 + 3 + 2) = 0
            // 1|xx x |013|9 - [(4 3) + (3 2) + (1 1)] = 9 - (4 + 3 + 1) = 1
            // 2|xx  x|014|9 - [(4 3) + (3 2) + (0 1)] = 9 - (4 + 3 + 0) = 2
            // 3|x xx |023|9 - [(4 3) + (2 2) + (1 1)] = 9 - (4 + 1 + 1) = 3
            // 4|x x x|024|9 - [(4 3) + (2 2) + (0 1)] = 9 - (4 + 1 + 0) = 4
            // 5|x  xx|034|9 - [(4 3) + (1 2) + (0 1)] = 9 - (4 + 0 + 0) = 5
            // 6| xxx |123|9 - [(3 3) + (2 2) + (1 1)] = 9 - (1 + 1 + 1) = 6
            // 7| xx x|124|9 - [(3 3) + (2 2) + (0 1)] = 9 - (1 + 1 + 0) = 7
            // 8| x xx|134|9 - [(3 3) + (1 2) + (0 1)] = 9 - (1 + 0 + 0) = 8
            // 9|  xxx|234|9 - [(2 3) + (1 2) + (0 1)] = 9 - (0 + 0 + 0) = 9
            var n = b.N;
            var k = b.K;
            var sum = (n, k).BinomialCoeficient() - 1;
            for (var i = 0; i < b.K; i++) {
                var v = n - 1 - indexes[i];
                if (v >= k - i)
                    sum -= (v, k - i).BinomialCoeficient();
            }

            return (b.K, sum);
        }

        public static IEnumerable<int[]> EnumerateCombinations(this (int n, int k) binomial) {
            var (n, k) = binomial;
            var tmp = new int[k];
            for (var i = 0; i < k; i++) tmp[i] = i;

            while (true) {
                // TODO Test
                yield return tmp.ToArray();

                var i = tmp.Length - 1;
                if (tmp[i] < n - 1) {
                    tmp[i]++;
                    continue;
                }

                i--;
                while (i >= 0 && tmp[i] + 1 >= tmp[i + 1]) i--;

                if (i < 0) yield break;

                var a = tmp[i];
                for (var j = i; j < k; j++)
                    tmp[j] = a + j - i + 1;
            }
        }

        public static int BinomialCoeficient(this (int n, int k) binomial) {
            var (n, k) = binomial;
            var nHalf = n / 2;

            if (k > nHalf)
                k = n - k;

            switch (k) {
                case 0:
                    return 1;
                case 1:
                    return n;
            }

            var res = n;
            for (var i = 1; i < k; i++) {
                res *= n - i;
                res /= i + 1;
            }

            return res;
        }
    #endregion
    }
}