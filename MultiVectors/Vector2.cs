// ReSharper disable InconsistentNaming

namespace MultiVectors {
    public class Vector2 : KVector {
    #region Fields
        public decimal X {
            get => this[0];
            set => this[0] = value;
        }

        public decimal Y {
            get => this[1];
            set => this[1] = value;
        }
    #endregion

    #region Ctor
        public Vector2() : base(1, 2) { }

        private Vector2(decimal x, decimal y) : base(1, 2, x, y) { }
    #endregion

    #region Methods
        public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
    #endregion
    }
}