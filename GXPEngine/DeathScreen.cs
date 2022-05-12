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
        public static int changeToLevel;
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
            if (Input.GetKeyDown(Key.ESCAPE)) EventsHandler.LevelChange?.Invoke(10);
        }
        public int GetNextScene()
        {
            return changeToLevel;
        }
        override public void UpdateNextSceneNumber(int i)
        {
            if(i != 9 && i != 10 && i != 11)
            changeToLevel = i;

        }

    }
}
