using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.Client.NoObf.CinematicCam.Primitives;

namespace Vintagestory.Client.NoObf.CinematicCam.Camera
{
    /// <summary>
    ///     Ripped from game source, and refactored to conform to basic coding standards.
    /// </summary>
    public sealed class CameraPoint
    {
        internal double Distance { get; set; }
        internal float Pitch { get; set; }
        internal float Roll { get; set; }
        internal double X { get; set; }
        internal double Y { get; set; }
        internal float Yaw { get; set; }
        internal double Z { get; set; }

        internal PolarCoordinates PolarCoordinates
        {
            get => new PolarCoordinates(Pitch, Yaw);
            set
            {
                Pitch = value.Pitch;
                Yaw = value.Yaw;
            }
        }

        internal Vec3d Position
        {
            get => new Vec3d(X, Y, Z);
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }

        public static CameraPoint FromEntityPos(EntityPos pos)
        {
            var point = new CameraPoint
            {
                X = pos.X,
                Y = pos.Y,
                Z = pos.Z,
                Pitch = pos.Pitch,
                Yaw = pos.Yaw,
                Roll = pos.Roll
            };

            return point;
        }

        internal CameraPoint Clone()
        {
            return new CameraPoint
            {
                X = X,
                Y = Y,
                Z = Z,
                Pitch = Pitch,
                Yaw = Yaw,
                Roll = Roll
            };
        }

        internal CameraPoint ExtrapolateFrom(CameraPoint p, int direction)
        {
            var dx = p.X - X;
            var dy = p.Y - Y;
            var dz = p.Z - Z;
            var dpitch = p.Pitch - Pitch;
            var dyaw = p.Yaw - Yaw;
            var droll = p.Roll - Roll;

            return new CameraPoint
            {
                X = X - dx * direction,
                Y = Y - dy * direction,
                Z = Z - dz * direction,
                Pitch = Pitch - dpitch * direction,
                Yaw = Yaw - dyaw * direction,
                Roll = Roll - droll * direction
            };
        }

        internal bool PositionEquals(CameraPoint point)
        {
            return point.X.Equals(X) && point.Y.Equals(Y) && point.Z.Equals(Z);
        }
    }
}