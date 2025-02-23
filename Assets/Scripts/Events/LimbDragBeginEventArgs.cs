using System;

namespace Climber3D.Events
{
    public class LimbDragBeginEventArgs : EventArgs
    {
        public LimbType LimbType { get; }

        public LimbDragBeginEventArgs(LimbType type)
        {
            LimbType = type;
        }
    }
}
