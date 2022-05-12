using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class DeathScreen : Scene
    {
        AnimationSprite background;
        int changeToLevel;
        public DeathScreen(int scN) : base(scN)
        {
            background = new AnimationSprite("Assets/DeathScreen/animation.png", 5, 2);
            //background.scale ;
            AddChild(new Sprite("Assets/DeathScreen/Illustration.png"));
            //AddChild(background);
            AddChild(background);
            AddChild(new Sprite("Assets/DeathScreen/Teext.png"));
        }
        void Update()
        {
            background.Animate(0.14f);

        }
        public int GetNextScene()
        {
            return changeToLevel;
        }
        public void UpdateNextSceneNumber(int i) => changeToLevel = i;
    }
}
