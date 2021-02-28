using Vintagestory.API.MathTools;
using Vintagestory.Client.NoObf.CinematicCam.Extensions;
using Vintagestory.Client.NoObf.CinematicCam.Interfaces;
using Vintagestory.Client.NoObf.CinematicCam.Primitives;

namespace Vintagestory.Client.NoObf.CinematicCam.Interpolation
{
    /// <summary>
    ///     Interpolates values between multiple nodes along a path, ensuring the camera always focusses on a single target point. 
    ///     Implements the <see cref="InterpolatorBase" /> base class.
    /// </summary>
    /// <seealso cref="InterpolatorBase" />
    internal class TargetInterpolator : InterpolatorBase
    {
        /// <summary>
        ///     Initialises a new instance of the <see cref="TargetInterpolator"/> class.
        /// </summary>
        /// <param name="interpolator">The base interpolator to use for position and additional angles.</param>
        /// <param name="point">The camera point to transpose values into.</param>
        /// <param name="targetPos">The target position for the camera to focus on.</param>
        private TargetInterpolator(ICameraPointInterpolator interpolator, CameraPoint point, Vec3d targetPos) :
            base(point)
        {
            Interpolator = interpolator;
            TargetPos = targetPos;
        }

        /// <summary>
        /// Gets the base interpolator to use for position and additional angles.
        /// </summary>
        /// <value>The base interpolator to use for position and additional angles.</value>
        private ICameraPointInterpolator Interpolator { get; }

        /// <summary>
        ///     Gets the target position for the camera to focus on.
        /// </summary>
        /// <value>The target position for the camera to focus on.</value>
        private Vec3d TargetPos { get; }

        /// <summary>
        ///     Interpolates the XYZ position between the current and next nodes along the path.
        /// This step must be done before rotational interpolation can occur.
        /// </summary>
        /// <param name="nodeList">A list of nodes needed for interpolation.</param>
        /// <param name="step">The fraction of which the player has already reached the next node (0-» still at current node, 1 -» already at next node)</param>
        public override void InterpolatePosition(InterpolationNodeArray nodeList, double step)
        {
            Interpolator.InterpolatePosition(nodeList, step);
        }

        /// <summary>
        ///     Interpolates the rotational angles between the current and next nodes along the path.
        /// This step must be done before additional angle interpolation can occur.
        /// </summary>
        /// <param name="nodeList">A list of nodes needed for interpolation.</param>
        /// <param name="step">The fraction of which the player has already reached the next node (0-» still at current node, 1 -» already at next node)</param>
        public override void InterpolatePolarCoordinates(InterpolationNodeArray nodeList, double step)
        {
            var pos = new Vec3d(Point.X, Point.Y, Point.Z);
            var coords = pos.InterpolateTargetCoordinates(TargetPos);
            Point.Pitch = coords.Pitch;
            Point.Yaw = coords.Yaw;
        }

        /// <summary>
        ///     Interpolates additional angles between the current and next nodes along the path.
        /// </summary>
        /// <param name="nodeList">A list of nodes needed for interpolation.</param>
        /// <param name="step">The fraction of which the player has already reached the next node (0-» still at current node, 1 -» already at next node)</param>
        /// <remarks>
        ///     Currently, this only interpolates the rotation around the Z axis, however, it would
        ///     be possible to include other features, such as dynamic FOV, and shader gradation.
        /// </remarks>
        public override void InterpolateAdditionAngles(InterpolationNodeArray nodeList, double step)
        {
            Interpolator.InterpolateAdditionAngles(nodeList, step);
        }

        /// <summary>
        ///     Initialises a new instance of the <see cref="TargetInterpolator"/> class.
        /// </summary>
        /// <param name="interpolator">The base interpolator to use for position and additional angles.</param>
        /// <param name="point">The camera point to transpose values into.</param>
        /// <param name="targetPos">The target position for the camera to focus on.</param>
        internal static ICameraPointInterpolator Create(
            ICameraPointInterpolator interpolator, CameraPoint point, Vec3d targetPos)
        {
            return new TargetInterpolator(interpolator, point, targetPos);
        }
    }
}