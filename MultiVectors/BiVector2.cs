// ReSharper disable InconsistentNaming

namespace MultiVectors {
    public class BiVector2 : KVector {
    #region Fields
        public decimal XY {
            get => this[0];
            set => this[0] = value;
        }
    #endregion

    #region Ctor
        public BiVector2() : base(2, 2) { }

        private BiVector2(decimal xy) : base(2, 2, xy) { }
    #endregion

    #region Methods
        public static BiVector2 operator +(BiVector2 a, BiVector2 b) => new(a.XY + b.XY);
        public static BiVector2 operator -(BiVector2 a, BiVector2 b) => new(a.XY - b.XY);
    #endregion
    }
}