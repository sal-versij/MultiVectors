using System.Linq;

using MultiVectors;

using Xunit;
using Xunit.Abstractions;

namespace MultiVectorsTests {
    public class ExtensionsTest {
    #region Fields
        private readonly ITestOutputHelper _testOutputHelper;
    #endregion

    #region Ctor
        public ExtensionsTest(ITestOutputHelper testOutputHelper) { _testOutputHelper = testOutputHelper; }
    #endregion

    #region Methods
        [Fact]
        public void AsMultiVector() {
            var vector = new KVector(1, 2, 1m, 2m);
            var multiVector = vector.AsMultiVector();
            var actual = multiVector[vector.K];

            Assert.Equal(vector.N, multiVector.N);
            Assert.Equal(vector.N + 1, multiVector.Length);
            Assert.Equal(vector, actual);
        }

        [Fact]
        public void ToKVectorAccessor() {
            var basis = new Basis(5, 1, 2, 4);
            var (k, i) = basis.ToKVectorAccessor();
            Assert.Equal(3, k);
            Assert.Equal(7, i);
        }

        [Fact]
        public void EnumerateCombinations() {
            const int n = 7;
            const int k = 3;
            var expectedLength = (7, 3).BinomialCoeficient();

            var combinations = (n, k).EnumerateCombinations().ToArray();

            _testOutputHelper.WriteLine(string.Join(";\n", combinations.Select(x => string.Join(" ", x))));
            Assert.Equal(expectedLength, combinations.Length);
        }

        [Fact]
        public void BinomialCoeficient() {
            const int n = 7;
            const int k = 3;
            const int expected = 35;
            var actual = (n, k).BinomialCoeficient();

            Assert.Equal(expected, actual);
        }
    #endregion
    }
}