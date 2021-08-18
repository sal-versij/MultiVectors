// ReSharper disable InconsistentNaming

namespace MultiVectors {
    public class Vector4 : KVector {
    #region Fields
        public decimal X {
            get => this[0];
            set => this[0] = value;
        }

        public decimal Y {
            get => this[1];
            set => this[1] = value;
        }

        public decimal Z {
            get => this[2];
            set => this[2] = value;
        }

        public decimal W {
            get => this[3];
            set => this[3] = value;
        }
    #endregion

    #region Ctor
        public Vector4() : base(1, 4) { }

        private Vector4(decimal x, decimal y, decimal z, decimal w) : base(1, 4, x, y, z, w) { }
    #endregion

    #region Methods
        public static Vector4 operator +(Vector4 a, Vector4 b) =>
            new(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);

        public static Vector4 operator -(Vector4 a, Vector4 b) =>
            new(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
    #endregion
    }
}