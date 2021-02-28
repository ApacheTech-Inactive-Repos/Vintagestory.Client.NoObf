using System.Runtime.InteropServices;

namespace Vintagestory.Client.NoObf.CinematicCam.Primitives
{
    /// <summary>
    ///     Represents an immutable, ordered pair of polar coordinates, pitch, and yaw, that defines the rotation around the X and Y axes of a vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct PolarCoordinates
    {
        /// <summary>
        /// Gets the pitch, or vertical rotation around the X axis of a vector.
        /// </summary>
        /// <value>The pitch, or vertical rotation around the X axis of a vector.</value>
        public float Pitch { get; }

        /// <summary>
        ///     Gets the yaw, or horizontal rotation around the Y axis of a vector.
        /// </summary>
        /// <value>The yaw, or horizontal rotation around the Y axis of a vector.</value>
        public float Yaw { get; }

        /// <summary>
        ///     Initialises a new instance of the <see cref="PolarCoordinates"/> struct.
        /// </summary>
        /// <param name="pitch">The pitch, or vertical rotation.</param>
        /// <param name="yaw">The yaw, or horizontal rotation.</param>
        public PolarCoordinates(float pitch, float yaw) => (Pitch, Yaw) = (pitch, yaw);

        /// <summary>
        ///     Converts this <see cref="PolarCoordinates" /> struct to a human-readable string.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this <see cref="PolarCoordinates" /> struct.</returns>
        public override string ToString() => $"Pitch: {Pitch}, Yaw: {Yaw}";
    }
}