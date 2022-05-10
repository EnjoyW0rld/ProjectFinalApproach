using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class EventsHandler
    {
        public static Action<int> LevelChange;
        public static Action<int> TransitionEnded;
        public static Action OpenTransition;
        public static Action<BallCollider> EnteredEndPlanet;
    }
}
