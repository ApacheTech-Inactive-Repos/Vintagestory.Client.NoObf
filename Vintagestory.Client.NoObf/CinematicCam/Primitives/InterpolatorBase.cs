using Vintagestory.Client.NoObf.CinematicCam.Camera;
using Vintagestory.Client.NoObf.CinematicCam.Interfaces;

namespace Vintagestory.Client.NoObf.CinematicCam.Primitives
{
    /// <summary>
    ///     Acts as a base for concrete implementations of camera point interpolators.
    /// </summary>
    internal abstract class InterpolatorBase : ICameraPointInterpolator
    {
        /// <summary>
        ///     Initialises a new instance of the <see cref="InterpolatorBase" /> class.
        /// </summary>
        /// <param name="point">The builder to use for intermediary steps.</param>
        protected InterpolatorBase(CameraPoint point)
        {
            Point = point;
        }

        /// <summary>
        ///     Gets the intermediary point builder.
        /// </summary>
        /// <value>The intermediary camera point builder.</value>
        protected CameraPoint Point { get; }

        /// <summary>
        ///     Interpolates the XYZ position between the current and next nodes along the path.
        ///     This step must be done before rotational interpolation can occur.
        /// </summary>
        /// <param name="nodeList">A list of nodes needed for interpolation.</param>
        /// <param name="step">
        ///     The fraction of which the player has already reached the next node (0-> still at current node, 1 ->
        ///     already at next node)
        /// </param>
        public abstract void InterpolatePosition(InterpolationNodeArray nodeList, double step);

        /// <summary>
        ///     Interpolates the rotational angles between the current and next nodes along the path.
        ///     This step must be done before additional angle interpolation can occur.
        /// </summary>
        /// <param name="nodeList">A list of nodes needed for interpolation.</param>
        /// <param name="step">
        ///     The fraction of which the player has already reached the next node (0-» still at current node, 1 -»
        ///     already at next node)
        /// </param>
        public abstract void InterpolatePolarCoordinates(InterpolationNodeArray nodeList, double step);

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
        public abstract void InterpolateAdditionAngles(InterpolationNodeArray nodeList, double step);

        /// <summary>
        ///     Interpolates between the current and next nodes. This is a cubic interpolation which behaves like Catmull-Rom.
        /// </summary>
        /// <param name="previous">
        ///     The node before the last left behind node which is the same as the current node at the beginning
        ///     of the path.
        /// </param>
        /// <param name="current">The most recent node the player left behind.</param>
        /// <param name="next">The upcoming node the player has to pass.</param>
        /// <param name="subsequent">The node after the upcoming node which is the same as the next node at the end of the path.</param>
        /// <param name="step">
        ///     The fraction of which the player has already reached the next node (0-> still at current node, 1 ->
        ///     already at next node)
        /// </param>
        protected static double CubicCatmull(double previous, double current, double next, double subsequent,
            double step)
        {
            var a = -0.5 * previous + 1.5 * current - 1.5 * next + 0.5 * subsequent;
            var b = previous - 2.5 * current + 2 * next - 0.5 * subsequent;
            var c = -0.5 * previous + 0.5 * next;

            return ((a * step + b) * step + c) * step + current;
        }

        /// <summary>
        ///     Interpolates between the current and next nodes. This is a simple cubic interpolation.
        /// </summary>
        /// <param name="previous">
        ///     The node before the last left behind node which is the same as the current node at the beginning
        ///     of the path.
        /// </param>
        /// <param name="current">The most recent node the player left behind.</param>
        /// <param name="next">The upcoming node the player has to pass.</param>
        /// <param name="subsequent">The node after the upcoming node which is the same as the next node at the end of the path.</param>
        /// <param name="step">
        ///     The fraction of which the player has already reached the next node (0-> still at current node, 1 ->
        ///     already at next node)
        /// </param>
        protected static float Cubic(float previous, float current, float next, float subsequent, double step)
        {
            var a = subsequent - next - previous + current;
            var b = previous - current - a;
            var c = next - previous;

            return (float) (((a * step + b) * step + c) * step + current);
        }

        /// <summary>
        ///     Interpolates between the current and next nodes. This is pure linear interpolation.
        /// </summary>
        /// <param name="current">The most recent node the player left behind.</param>
        /// <param name="next">The upcoming node the player has to pass.</param>
        /// <param name="step">
        ///     The fraction of which the player has already reached the next node (0-> still at current node, 1 ->
        ///     already at next node)
        /// </param>
        protected static double Linear(double current, double next, double step)
        {
            return current + (next - current) * step;
        }

        /// <summary>
        ///     Interpolates between the current and next nodes. This is pure linear interpolation.
        /// </summary>
        /// <param name="current">The most recent node the player left behind.</param>
        /// <param name="next">The upcoming node the player has to pass.</param>
        /// <param name="step">
        ///     The fraction of which the player has already reached the next node (0-> still at current node, 1 ->
        ///     already at next node)
        /// </param>
        protected static float Linear(float current, float next, double step)
        {
            return (float) (current + (next - current) * step);
        }
    }
}