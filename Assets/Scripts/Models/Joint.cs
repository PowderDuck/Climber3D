using System.Collections.Generic;
using UnityEngine;

namespace Climber3D.Models
{
    public class Joint
    {
        private IList<GameObject> _points { get; }
        private IList<Vector3> _initialDisplacements { get; set; }

        private GameObject _referencePoint { get; }

        public Vector3 Direction
        {
            get
            {
                if (_points.Count <= 0)
                {
                    return Vector3.zero;
                }

                var direction = Vector3.zero;
                for (var i = 0; i < _points.Count - 1; i++)
                {
                    direction += _points[i + 1].transform.position - _points[i].transform.position;
                }

                return direction.normalized;
            }
        }

        public Vector3 Displacement
        {
            get
            {
                var difference = Vector3.zero;
                for (var i = 0; i < _points.Count; i++)
                {
                    var currentDisplacement = _points[i].transform.position - _referencePoint.transform.position;
                    difference += currentDisplacement - _initialDisplacements[i];
                }

                return difference / _points.Count;
            }
        }

        public Joint(IList<GameObject> points, GameObject referencePoint)
        {
            _points = points;
            _referencePoint = referencePoint;

            UpdateDisplacements();
        }

        public void UpdateDisplacements()
        {
            _initialDisplacements = new List<Vector3>();
            foreach (var point in _points)
            {
                _initialDisplacements.Add(point.transform.position - _referencePoint.transform.position);
            }
        }
    }
}
