using Vintagestory.Client.NoObf.CinematicCam.Primitives;

namespace Vintagestory.Client.NoObf.CinematicCam.Interfaces
{
    /// <summary>
    ///     Represents an interpolator that aids calculation of camera positions along a path.
    /// </summary>
    public interface ICameraPointInterpolator
    {
        /// <summary>
        ///     Interpolates the XYZ position between the current and next nodes along the path.
        ///     This step must be done before rotational interpolation can occur.
        /// </summary>
        /// <param name="nodeList">A list of nodes needed for interpolation.</param>
        /// <param name="step">
        ///     The fraction of which the player has already reached the next node (0-> still at current node, 1 ->
        ///     already at next node)
        /// </param>
        void InterpolatePosition(InterpolationNodeArray nodeList, double step);

        /// <summary>
        ///     Interpolates the rotational angles between the current and next nodes along the path.
        ///     This step must be done before additional angle interpolation can occur.
        /// </summary>
        /// <param name="nodeList">A list of nodes needed for interpolation.</param>
        /// <param name="step">
        ///     The fraction of which the player has already reached the next node (0-> still at current node, 1 ->
        ///     already at next node)
        /// </param>
        void InterpolatePolarCoordinates(InterpolationNodeArray nodeList, double step);

        /// <summary>
        ///     Interpolates additional angles between the current and next nodes along the path.
        /// </summary>
        /// <remarks>
        ///     Currently, this only interpolates the rotation around the Z axis, however, it would
        ///     be possible to include other features, such as dynamic FOV, and shader gradation.
        /// </remarks>
        /// <param name="nodeList">A list of nodes needed for interpolation.</param>
        /// <param name="step">
        ///     The fraction of which the player has already reached the next node (0-> still at current node, 1 ->
        ///     already at next node)
        /// </param>
        void InterpolateAdditionAngles(InterpolationNodeArray nodeList, double step);
    }
}