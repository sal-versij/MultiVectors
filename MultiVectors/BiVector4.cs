// ReSharper disable InconsistentNaming

namespace MultiVectors {
    public class BiVector4 : KVector {
    #region Fields
        public decimal XY {
            get => this[0];
            set => this[0] = value;
        }

        public decimal YZ {
            get => this[1];
            set => this[1] = value;
        }

        public decimal ZW {
            get => this[2];
            set => this[2] = value;
        }

        public decimal WX {
            get => this[3];
            set => this[3] = value;
        }
    #endregion

    #region Ctor
        public BiVector4() : base(2, 4) { }

        private BiVector4(decimal xy, decimal yz, decimal zw, decimal wx) : base(2, 4, xy, yz, zw, wx) { }
    #endregion

    #region Methods
        public static BiVector4 operator +(BiVector4 a, BiVector4 b) =>
            new(a.XY + b.XY, a.YZ + b.YZ, a.ZW + b.ZW, a.WX + b.WX);

        public static BiVector4 operator -(BiVector4 a, BiVector4 b) =>
            new(a.XY - b.XY, a.YZ - b.YZ, a.ZW - b.ZW, a.WX - b.WX);
    #endregion
    }
}