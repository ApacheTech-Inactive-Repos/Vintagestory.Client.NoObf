using System;
using Vintagestory.API.MathTools;
using Vintagestory.Client.NoObf.CinematicCam.Primitives;

namespace Vintagestory.Client.NoObf.CinematicCam.Extensions
{
    public static class Vec3dEx
    {
        /// <summary>
        ///     Generates the rotational polar coordinates between the agent, and a target location.
        /// </summary>
        /// <param name="agentPos">The agent position.</param>
        /// <param name="targetPos">The target position.</param>
        public static PolarCoordinates InterpolateTargetCoordinates(this Vec3d agentPos, Vec3d targetPos)
        {
            var cartesianCoordinates = targetPos.SubCopy(agentPos).Normalize();
            var pitch = (float) Math.Asin(cartesianCoordinates.Y);
            var yaw = GameMath.TWOPI - (float) Math.Atan2(cartesianCoordinates.Z, cartesianCoordinates.X);
            return new PolarCoordinates(GameMath.PI - pitch, yaw % GameMath.TWOPI);
        }
    }
}