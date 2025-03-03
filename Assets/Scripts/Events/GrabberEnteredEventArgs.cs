using System;
using Climber3D.Climbables;

namespace Climber3D.Events
{
    public class GrabberEnteredEventArgs : EventArgs
    {
        public Climbable Target { get; }

        public GrabberEnteredEventArgs(Climbable target)
        {
            Target = target;
        }
    }
}