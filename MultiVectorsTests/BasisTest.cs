using System.Linq;

using MultiVectors;

using Xunit;
using Xunit.Abstractions;

namespace MultiVectorsTests {
    public class BasisTest {
    #region Fields
        private readonly ITestOutputHelper _testOutputHelper;
    #endregion

    #region Ctor
        public BasisTest(ITestOutputHelper testOutputHelper) { _testOutputHelper = testOutputHelper; }
    #endregion

    #region Methods
        [Fact]
        public void ConstructorEmpty() {
            const int n = 3;
            var basis = new Basis(n);
            Assert.Equal(n, basis.N);
            Assert.Equal(0, basis.K);
            Assert.False(basis.InvertSign);
            Assert.Empty(basis.UnitBasisIndexes);
        }

        [Fact]
        public void ConstructorWithDefaults() {
            const int n = 3;

            var b1 = new Basis(n, 1, 2);
            Assert.Equal(n, b1.N);
            Assert.Equal(2, b1.K);
            Assert.False(b1.InvertSign);

            var b2 = new Basis(n, 2, 1);
            Assert.Equal(n, b2.N);
            Assert.Equal(2, b2.K);
            Assert.True(b2.InvertSign);

            var b3 = new Basis(n, 0);
            Assert.Equal(n, b3.N);
            Assert.Equal(1, b3.K);
            Assert.False(b3.InvertSign);
        }

        [Fact]
        public void Toggle() {
            const int n = 3;
            var basis = new Basis(n, 1, 2);
            Assert.Equal(2, basis.K);
            Assert.False(basis.InvertSign);

            basis.Toggle(1);
            Assert.Equal(1, basis.K);
            Assert.True(basis.InvertSign);

            basis.Toggle(0);
            Assert.Equal(2, basis.K);
            Assert.False(basis.InvertSign);

            basis.Toggle(2);
            Assert.Equal(1, basis.K);
            Assert.False(basis.InvertSign);

            basis.Toggle(1);
            Assert.Equal(2, basis.K);
            Assert.False(basis.InvertSign);

            basis.Toggle(2);
            Assert.Equal(3, basis.K);
            Assert.False(basis.InvertSign);
        }

        [Fact]
        public void Product() {
            const int n = 5;
            var bs = new Basis[] {
                new(n, 1, 2, 4),
                new(n, 0, 3, 2),
                new(n, 1),
                new(n, 4, 0),
            };

            var rs = new Basis[16];
            for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                rs[i * 4 + j] = bs[i] * bs[j];


            var expected = new[] {
                (0, true),
                (4, false),
                (2, false),
                (3, false),
                (4, false),
                (0, true),
                (4, true),
                (3, false),
                (2, false),
                (4, false),
                (0, false),
                (3, false),
                (3, true),
                (3, true),
                (3, false),
                (0, true),
            };

            for (var i = 0; i < expected.Length; i++) {
                _testOutputHelper.WriteLine($"{i}:{bs[i / 4]} * {bs[i % 4]} = {rs[i]};");
                Assert.Equal(expected[i], (rs[i].K, rs[i].InvertSign));
            }
        }

        [Fact]
        public void Clone() {
            const int n = 5;

            var bs = new Basis(n, 1, 2, 4);
            var clone = bs.Clone();

            Assert.Equal(bs, clone);
            Assert.False(ReferenceEquals(bs, clone));
        }

        [Fact]
        public void EnumerateFromKVectorAccessor() {
            const int n = 5;
            const int k = 3;
            var basises = Basis.EnumerateFromKVectorAccessor(n, k).ToArray();

            _testOutputHelper.WriteLine(string.Join(";\n", basises.Select(x => x.ToString())));

            Assert.Equal((n, k).BinomialCoeficient(), basises.Length);
        }
    #endregion
    }
}