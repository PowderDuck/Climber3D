using Climber3D.Climbables;
using Climber3D.Events;
using Climber3D.Limbs;
using UnityEngine;

namespace Climber3D
{
    public class Grabber : MonoBehaviour
    {
        public delegate void GrabberEnteredEvent(object sender, GrabberEnteredEventArgs eventArgs);
        public event GrabberEnteredEvent? GrabberEntered;

        public Limb Limb { get; private set; } = default!;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Climbable>(out var climbable))
            {
                GrabberEntered?.Invoke(this, new(climbable));
                climbable.Grabbed(this);
            }
        }

        public void SetLimb(Limb limb)
        {
            Limb = limb;
        }
    }
}
