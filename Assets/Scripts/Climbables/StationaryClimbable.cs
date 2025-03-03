using UnityEngine;

namespace Climber3D.Climbables
{
    public class StationaryClimbable : Climbable
    {
        [SerializeField] private int _capacity = 1;

        public override int Capacity => _capacity;

        protected override void ProcessGrab(Grabber grabber)
        {
            Debug.Log($"Grabbed by {grabber.gameObject.name}");
        }

        protected override void ProcessRelease(Grabber grabber)
        {
            Debug.Log($"Released by {grabber.gameObject.name}");
        }
    }
}