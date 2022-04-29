using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Scene : GameObject
    {

        public Scene(string mapName)
        {
            MyMapInterpritor.GetLevel(mapName);
        }
        virtual internal void SpawnObjects(TiledMapParser.ObjectGroup[])
        {

        }
        virtual public Player GetPlayer() => null;
        virtual public Satelite[] GetSatelites() => null;
        virtual public BallCollider[] GetBallColliders() => null;
        virtual public SpaceBody[] GetSpaceBodies() => null;
    }
}
