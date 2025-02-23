using Climber3D.Events;
using UnityEngine;

namespace Climber3D
{
    public class Grabber : MonoBehaviour
    {
        public delegate void GrabberEnteredEvent(object sender, GrabberEnteredEventArgs eventArgs);
        public event GrabberEnteredEvent? GrabberEntered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Climbable>())
            {
                GrabberEntered?.Invoke(this, new(other.gameObject));
            }
        }
    }
}
