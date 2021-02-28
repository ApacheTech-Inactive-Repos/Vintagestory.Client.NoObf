using Vintagestory.Client.NoObf.CinematicCam.Interfaces;
using Vintagestory.Client.NoObf.CinematicCam.Primitives;

namespace Vintagestory.Client.NoObf.CinematicCam.Interpolation
{
    /// <summary>
    ///     Interpolates values between multiple nodes along a path, using cubic 
    ///     interpolation between the current node and the next node.
    ///     Implements the <see cref="InterpolatorBase" /> base class.
    /// </summary>
    /// <seealso cref="InterpolatorBase" />
    internal class CubicInterpolator : InterpolatorBase
    {
        /// <summary>
        ///     Initialises a new instance of the <see cref="CubicInterpolator" /> class.
        /// </summary>
        /// <param name="point">The camera point to transpose values into.</param>
        private CubicInterpolator(CameraPoint point) : base(point)
        {
        }

        /// <summary>
        ///     Interpolates the XYZ position between the current and next nodes along the path.
        ///     This step must be done before rotational interpolation can occur.
        /// </summary>
        /// <param name="nodeList">A list of nodes needed for interpolation.</param>
        /// <param name="step">
        ///     The fraction of which the player has already reached the next node (0-» still at current node, 1 -»
        ///     already at next node)
        /// </param>
        public override void InterpolatePosition(InterpolationNodeArray nodeList, double step)
        {
            Point.X = CubicCatmull(
                nodeList.Previous.X, nodeList.Current.X, nodeList.Next.X, nodeList.Subsequent.X, step);
            Point.Y = CubicCatmull(
                nodeList.Previous.Y, nodeList.Current.Y, nodeList.Next.Y, nodeList.Subsequent.Y, step);
            Point.Z = CubicCatmull(
                nodeList.Previous.Z, nodeList.Current.Z, nodeList.Next.Z, nodeList.Subsequent.Z, step);
        }

        /// <summary>
        ///     Interpolates the rotational angles between the current and next nodes along the path.
        ///     This step must be done before additional angle interpolation can occur.
        /// </summary>
        /// <param name="nodeList">A list of nodes needed for interpolation.</param>
        /// <param name="step">
        ///     The fraction of which the player has already reached the next node (0-» still at current node, 1 -»
        ///     already at next node)
        /// </param>
        public override void InterpolatePolarCoordinates(InterpolationNodeArray nodeList, double step)
        {
            Point.Pitch = Cubic(
                nodeList.Previous.Pitch, nodeList.Current.Pitch, nodeList.Next.Pitch, nodeList.Subsequent.Pitch, step);
            Point.Pitch = Cubic(
                nodeList.Previous.Yaw, nodeList.Current.Yaw, nodeList.Next.Yaw, nodeList.Subsequent.Yaw, step);
        }

        /// <summary>
        ///     Interpolates additional angles between the current and next nodes along the path.
        /// </summary>
        /// <param name="nodeList">A list of nodes needed for interpolation.</param>
        /// <param name="step">
        ///     The fraction of which the player has already reached the next node (0-» still at current node, 1 -»
        ///     already at next node)
        /// </param>
        /// <remarks>
        ///     Currently, this only interpolates the rotation around the Z axis, however, it would
        ///     be possible to include other features, such as dynamic FOV, and shader gradation.
        /// </remarks>
        public override void InterpolateAdditionAngles(InterpolationNodeArray nodeList, double step)
        {
            Point.Roll = Cubic(
                nodeList.Previous.Pitch, nodeList.Current.Pitch, nodeList.Next.Pitch, nodeList.Subsequent.Pitch, step);
        }

        /// <summary>
        ///     Initialises a new instance of the <see cref="CubicInterpolator" /> class.
        /// </summary>
        /// <param name="point">The camera point to transpose values into.</param>
        internal static ICameraPointInterpolator Create(CameraPoint point)
        {
            return new CubicInterpolator(point);
        }
    }
}