using System;
using UnityEngine;

namespace Climber3D.Events
{
    public class GrabberEnteredEventArgs : EventArgs
    {
        public GameObject Target { get; }

        public GrabberEnteredEventArgs(GameObject target)
        {
            Target = target;
        }
    }
}