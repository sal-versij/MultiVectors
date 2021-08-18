// ReSharper disable InconsistentNaming

namespace MultiVectors {
    public class QuatriVector4 : KVector {
    #region Fields
        public decimal XYZW {
            get => this[0];
            set => this[0] = value;
        }
    #endregion

    #region Ctor
        public QuatriVector4() : base(4, 4) { }

        private QuatriVector4(decimal xyzw) : base(4, 4, xyzw) { }
    #endregion

    #region Methods
        public static QuatriVector4 operator +(QuatriVector4 a, QuatriVector4 b) =>
            new(a.XYZW + b.XYZW);

        public static QuatriVector4 operator -(QuatriVector4 a, QuatriVector4 b) =>
            new(a.XYZW - b.XYZW);
    #endregion
    }
}