using Vintagestory.Client.NoObf.CinematicCam.Interfaces;
using Vintagestory.Client.NoObf.CinematicCam.Primitives;

namespace Vintagestory.Client.NoObf.CinematicCam.Interpolation
{
    /// <summary>
    ///     Interpolates values between multiple nodes along a path, using linear interpolation between the current node and next node. 
    ///     Implements the <see cref="InterpolatorBase" /> base class.
    /// </summary>
    /// <seealso cref="InterpolatorBase" />
    internal class LinearInterpolator : InterpolatorBase
    {
        /// <summary>
        ///     Initialises a new instance of the <see cref="LinearInterpolator"/> class.
        /// </summary>
        /// <param name="point">The camera point to transpose values into.</param>
        private LinearInterpolator(CameraPoint point) : base(point) { }

        /// <summary>
        ///     Initialises a new instance of the <see cref="LinearInterpolator"/> class.
        /// </summary>
        /// <param name="point">The camera point to transpose values into.</param>
        internal static ICameraPointInterpolator Create(CameraPoint point) => new LinearInterpolator(point);

        /// <summary>
        ///     Interpolates the XYZ position between the current and next nodes along the path.
        ///     This step must be done before rotational interpolation can occur.
        /// </summary>
        /// <param name="nodeList">A list of nodes needed for interpolation.</param>
        /// <param name="step">The fraction of which the player has already reached the next node (0-» still at current node, 1 -» already at next node)</param>
        public override void InterpolatePosition(InterpolationNodeArray nodeList, double step)
        {
            Point.X = Linear(nodeList.Current.X, nodeList.Next.X, step);
            Point.Y = Linear(nodeList.Current.Y, nodeList.Next.Y, step);
            Point.Z = Linear(nodeList.Current.Z, nodeList.Next.Z, step);
        }

        /// <summary>
        ///     Interpolates the rotational angles between the current and next nodes along the path.
        ///     This step must be done before additional angle interpolation can occur.
        /// </summary>
        /// <param name="nodeList">A list of nodes needed for interpolation.</param>
        /// <param name="step">The fraction of which the player has already reached the next node (0-» still at current node, 1 -» already at next node)</param>
        public override void InterpolatePolarCoordinates(InterpolationNodeArray nodeList, double step)
        {
            Point.Pitch = Linear(nodeList.Current.Pitch, nodeList.Next.Pitch, step);
            Point.Yaw = Linear(nodeList.Current.Yaw, nodeList.Next.Yaw, step);
        }

        /// <summary>
        ///     Interpolates additional angles between the current and next nodes along the path.
        /// </summary>
        /// <param name="nodeList">A list of nodes needed for interpolation.</param>
        /// <param name="step">The fraction of which the player has already reached the next node (0-» still at current node, 1 -» already at next node)</param>
        /// <remarks>Currently, this only interpolates the rotation around the Z axis, however, it would
        /// be possible to include other features, such as dynamic FOV, and shader gradation.</remarks>
        public override void InterpolateAdditionAngles(InterpolationNodeArray nodeList, double step)
        {
            Point.Roll = Linear(nodeList.Current.Roll, nodeList.Next.Roll, step);
        }
    }
}