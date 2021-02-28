using System.Runtime.InteropServices;
using Vintagestory.Client.NoObf.CinematicCam.Camera;

namespace Vintagestory.Client.NoObf.CinematicCam.Primitives
{
    /// <summary>
    ///     Represents an ordered list of <see cref="CameraPoint" />s, which are required to interpolate between nodes along a
    ///     path.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct InterpolationNodeArray
    {
        /// <summary>
        ///     Gets the node before the last left behind node, which is the same as the current node at the beginning of the path.
        /// </summary>
        /// <value>The node before the last left behind node, which is the same as the current node at the beginning of the path.</value>
        internal CameraPoint Previous { get; }

        /// <summary>
        ///     Gets the most recent node the player left behind.
        /// </summary>
        /// <value>The most recent node the player left behind.</value>
        internal CameraPoint Current { get; }

        /// <summary>
        ///     Gets the upcoming node the player has to pass.
        /// </summary>
        /// <value>The upcoming node the player has to pass.</value>
        internal CameraPoint Next { get; }

        /// <summary>
        ///     Gets the node after the upcoming node, which is the same as the next node at the end of the path.
        /// </summary>
        /// <value>The node after the upcoming node, which is the same as the next node at the end of the path.</value>
        internal CameraPoint Subsequent { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InterpolationNodeArray" /> struct.
        /// </summary>
        /// <param name="previousNode">
        ///     The node before the last left behind node, which is the same as the current node at the beginning of the path.
        /// </param>
        /// <param name="currentNode">The most recent node the player left behind.</param>
        /// <param name="nextNode">The upcoming node the player has to pass.</param>
        /// <param name="nodeAfterNext">The node after the upcoming node which is the same as the next node at the end of the path.</param>
        internal InterpolationNodeArray(
            CameraPoint previousNode, CameraPoint currentNode, CameraPoint nextNode, CameraPoint nodeAfterNext)
        {
            (Previous, Current, Next, Subsequent) = (previousNode, currentNode, nextNode, nodeAfterNext);
        }
    }
}