// ReSharper disable InconsistentNaming

namespace MultiVectors {
    public class TriVector3 : KVector {
    #region Fields
        public decimal XYZ {
            get => this[0];
            set => this[0] = value;
        }
    #endregion

    #region Ctor
        public TriVector3() : base(3, 3) { }

        private TriVector3(decimal xyz) : base(3, 3, xyz) { }
    #endregion

    #region Methods
        public static TriVector3 operator +(TriVector3 a, TriVector3 b) =>
            new(a.XYZ + b.XYZ);

        public static TriVector3 operator -(TriVector3 a, TriVector3 b) =>
            new(a.XYZ - b.XYZ);
    #endregion
    }
}