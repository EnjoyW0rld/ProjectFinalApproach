using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class WinScreen : Scene
    {
        AnimationSprite photo;
        public WinScreen() : base(4)
        {
            AddChild(new Sprite("Assets/EndScreen/End_win_bg.png"));
            photo = new AnimationSprite("Assets/EndScreen/End_win.png", 4, 2);
            AddChild(photo);
            AddChild(new Sprite("Assets/EndScreen/win_text.png"));
        }
        void Update()
        {
            photo.Animate(0.15f);
            if (Input.GetKeyDown(Key.ESCAPE))
            {
                EventsHandler.LevelChange?.Invoke(10);
                DeathScreen.changeToLevel = 0;
            }
        }
    }
    
}
