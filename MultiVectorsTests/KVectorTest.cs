using System.Linq;

using MultiVectors;

using Xunit;
using Xunit.Abstractions;

namespace MultiVectorsTests {
    public class KVectorTest {
    #region Fields
        private readonly ITestOutputHelper _testOutputHelper;
    #endregion

    #region Ctor
        public KVectorTest(ITestOutputHelper testOutputHelper) { _testOutputHelper = testOutputHelper; }
    #endregion

    #region Methods
        [Fact]
        public void ConstructorEmpty() {
            const int n = 3;
            const int k = 2;
            var v = new KVector(k, n);

            Assert.Equal(n, v.N);
            Assert.Equal(k, v.K);
            Assert.True(v.All(x => x == 0));
        }

        [Fact]
        public void ConstructorWithDefaults() {
            const int n = 3;
            const int k = 2;
            var v = new KVector(k, n, 1m, 2m, 3m);

            Assert.Equal(n, v.N);
            Assert.Equal(k, v.K);
            Assert.Equal(1m, v[0]);
            Assert.Equal(2m, v[1]);
            Assert.Equal(3m, v[2]);
        }

        [Fact]
        public void CopyTo() {
            const int n = 3;
            const int k = 2;
            var v1 = new KVector(k, n, 1m, 2m, 3m);
            var v2 = new KVector(k, n);
            v1.CopyTo(v2);

            Assert.Equal(v1, v2);
        }

        [Fact]
        public void Clone() {
            const int n = 3;
            const int k = 2;
            var v1 = new KVector(k, n, 1m, 2m, 3m);
            var v2 = v1.Clone();

            Assert.Equal(v1, v2);
            Assert.False(ReferenceEquals(v1, v2));
        }

        [Fact]
        public void SumAndSubtraction() {
            const int n = 3;
            const int k = 2;

            var v1 = new KVector(k, n, 1m, 2m, 3m);
            var v2 = new KVector(k, n, 2m, -2m, 1m);

            var r1 = v1 + v2;
            var r2 = v1 - v2;

            Assert.Equal(n, r1.N);
            Assert.Equal(3m, r1[k][0]);
            Assert.Equal(0m, r1[k][1]);
            Assert.Equal(4m, r1[k][2]);

            Assert.Equal(n, r2.N);
            Assert.Equal(-1m, r2[k][0]);
            Assert.Equal(4m, r2[k][1]);
            Assert.Equal(2m, r2[k][2]);
        }

        [Fact]
        public void Product() {
            const int n = 3;
            const int k = 2;

            var v1 = new KVector(k, n, 1m, 2m, 3m);
            var v2 = new KVector(k, n, 2m, -2m, 1m);

            var r1 = v1 * v2;
            var r2 = v2 * v1;

            _testOutputHelper.WriteLine(r1.ToString());
            _testOutputHelper.WriteLine(r2.ToString());

            Assert.Equal(n, r1.N);
            Assert.Equal(n, r2.N);

            Assert.Equal(-1, r1[0][0]);
            Assert.Equal(0, r1[1][0]);
            Assert.Equal(0, r1[1][1]);
            Assert.Equal(0, r1[1][2]);
            Assert.Equal(-8, r1[2][0]);
            Assert.Equal(-5, r1[2][1]);
            Assert.Equal(6, r1[2][2]);
            Assert.Equal(0, r1[3][0]);

            Assert.Equal(-1, r2[0][0]);
            Assert.Equal(0, r2[1][0]);
            Assert.Equal(0, r2[1][1]);
            Assert.Equal(0, r2[1][2]);
            Assert.Equal(8, r2[2][0]);
            Assert.Equal(5, r2[2][1]);
            Assert.Equal(-6, r2[2][2]);
            Assert.Equal(0, r2[3][0]);
        }
    #endregion
    }
}