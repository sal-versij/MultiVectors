// ReSharper disable InconsistentNaming

namespace MultiVectors {
    public class BiVector3 : KVector {
    #region Fields
        public decimal XY {
            get => this[0];
            set => this[0] = value;
        }

        public decimal YZ {
            get => this[1];
            set => this[1] = value;
        }

        public decimal ZX {
            get => this[2];
            set => this[2] = value;
        }
    #endregion

    #region Ctor
        public BiVector3() : base(2, 3) { }

        private BiVector3(decimal xy, decimal yz, decimal zx) : base(2, 3, xy, yz, zx) { }
    #endregion

    #region Methods
        public static BiVector3 operator +(BiVector3 a, BiVector3 b) =>
            new(a.XY + b.XY, a.YZ + b.YZ, a.ZX + b.ZX);

        public static BiVector3 operator -(BiVector3 a, BiVector3 b) =>
            new(a.XY - b.XY, a.YZ - b.YZ, a.ZX - b.ZX);
    #endregion
    }
}