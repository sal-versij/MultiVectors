// ReSharper disable InconsistentNaming

namespace MultiVectors {
    public class TriVector4 : KVector {
    #region Fields
        public decimal XYZ {
            get => this[0];
            set => this[0] = value;
        }

        public decimal YZW {
            get => this[1];
            set => this[1] = value;
        }

        public decimal ZWX {
            get => this[2];
            set => this[2] = value;
        }

        public decimal WXY {
            get => this[3];
            set => this[3] = value;
        }
    #endregion

    #region Ctor
        public TriVector4() : base(3, 4) { }

        private TriVector4(decimal xyz, decimal yzw, decimal zwx, decimal wxy) : base(3, 4, xyz, yzw, zwx, wxy) { }
    #endregion

    #region Methods
        public static TriVector4 operator +(TriVector4 a, TriVector4 b) =>
            new(a.XYZ + b.XYZ, a.YZW + b.YZW, a.ZWX + b.ZWX, a.WXY + b.WXY);

        public static TriVector4 operator -(TriVector4 a, TriVector4 b) =>
            new(a.XYZ - b.XYZ, a.YZW - b.YZW, a.ZWX - b.ZWX, a.WXY - b.WXY);
    #endregion
    }
}