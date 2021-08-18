// ReSharper disable InconsistentNaming

namespace MultiVectors {
    public class Vector3 : KVector {
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
    #endregion

    #region Ctor
        public Vector3() : base(1, 3) { }

        private Vector3(decimal x, decimal y, decimal z) : base(1, 3, x, y, z) { }
    #endregion

    #region Methods
        public static Vector3 operator +(Vector3 a, Vector3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Vector3 operator -(Vector3 a, Vector3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    #endregion
    }
}