using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Climber3D.Climbables
{
    public abstract class Climbable : MonoBehaviour
    {
        public abstract int Capacity { get; }

        protected List<Grabber> _grabbers { get; set; } = new();
        public IReadOnlyList<Grabber> Grabbers => _grabbers;

        public void Grabbed(Grabber grabber)
        {
            if (_grabbers.Count < Capacity
                && !_grabbers.Contains(grabber))
            {
                _grabbers.Add(grabber);
                ProcessGrab(grabber);
            }
        }

        public void Released(Grabber grabber)
        {
            if (_grabbers.Contains(grabber))
            {
                _grabbers.Remove(grabber);
                ProcessRelease(grabber);
            }
        }

        public virtual bool Validate()
        {
            return _grabbers.Count < Capacity;
        }

        protected abstract void ProcessGrab(Grabber grabber);
        protected abstract void ProcessRelease(Grabber grabber);

        public void RefreshGrabbers()
        {
            var presentGrabbers = _grabbers
                .Where(grabber => grabber != null)
                .ToList();

            _grabbers = presentGrabbers;
        }
    }
}
