using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.MathTools;
using Vintagestory.Client.NoObf.CinematicCam.Interfaces;
using Vintagestory.Client.NoObf.CinematicCam.Primitives;

namespace Vintagestory.Client.NoObf.CinematicCam.Interpolation
{
    /// <summary>
    ///     Interpolates dynamic camera positions as it traverses a path.
    /// </summary>
    public class CameraPathInterpolator
    {
        private readonly List<CameraPoint> _cameraPath;
        private readonly ICameraPointInterpolator _interpolator;
        private readonly int _nodesToTravel;
        private readonly CameraPoint _point;

        /// <summary>
        ///     Initialises a new instance of the <see cref="CameraPathInterpolator" /> class.
        /// </summary>
        /// <param name="points">The list of nodes along a complete camera path.</param>
        /// <param name="target">The target, if any, that the camera focusses on as it traverses the path.</param>
        public CameraPathInterpolator(IEnumerable<CameraPoint> points, Vec3d target = null)
        {
            _cameraPath = points.ToList();

            // NOTE: We don't count the origin position in the count here.
            _nodesToTravel = _cameraPath.Count - 1;

            _point = new CameraPoint();

            var linearPath = _cameraPath.Count == 2;

            var tmpInterpolator = linearPath
                ? LinearInterpolator.Create(_point)
                : CubicInterpolator.Create(_point);

            _interpolator = target != null
                ? TargetInterpolator.Create(tmpInterpolator, _point, target)
                : tmpInterpolator;
        }

        /// <summary>
        ///     Calculates the camera point at the current position along the path.
        /// </summary>
        /// <param name="pathPosition">The path position.</param>
        public CameraPoint GetPoint(int pathPosition)
        {
            var section = pathPosition * _nodesToTravel;

            var section1 = section;
            if (section1 == _nodesToTravel)
                // pathPosition unavoidably reaches the last node at the very end ->
                // So decrement the previous node to stop the concertina effect.
                section--;
            var section2 = section1 + 1;

            double step = section - section1;

            var section0 = section1 - 1;
            var section3 = section2 + 1;

            // Bounding the outer nodes inside the array if necessary
            // I could extrapolate these points, but this is a quick and acceptable
            // Solution: It even creates some kind of fade in and out
            if (section0 < 0) section0 = 0;
            if (section3 > _nodesToTravel) section3 = _nodesToTravel;

            var nodeList = new InterpolationNodeArray(
                _cameraPath[section0],
                _cameraPath[section1],
                _cameraPath[section2],
                _cameraPath[section3]);

            _interpolator.InterpolatePosition(nodeList, step);
            _interpolator.InterpolatePolarCoordinates(nodeList, step);
            _interpolator.InterpolateAdditionAngles(nodeList, step);
            return _point;
        }
    }
}