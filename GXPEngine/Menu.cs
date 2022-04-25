using System;
namespace GXPEngine
{
    public class Menu : GameObject
    {
        public Button _button;
        public Sprite tutorial, nextLevelSprite, restartSprite,winSprite;
        EasyDraw canvas;
        bool _hasStarted;
        public static bool _gameover = false;
        bool level1completed = false;
        bool level2completed = false;
        private int _score;
        Level level = new Level();
        Level2 level2 = new Level2();
        Sound music;
        SoundChannel musicChannel;
        public Menu() : base()
        {
            
            //musicChannel = music.Play();
            _hasStarted = false;
            canvas = new EasyDraw(800, 600);
            tutorial = new Sprite("tutorial.png");
            nextLevelSprite = new Sprite("nextLevel.png");
            restartSprite = new Sprite("restartSprite.png");
            winSprite = new Sprite("winSprite.png");
            _button = new Button();
            canvas.collider.isTrigger = true;
            tutorial.collider.isTrigger = true;
            nextLevelSprite.collider.isTrigger = true;
            restartSprite.collider.isTrigger = true;
            winSprite.collider.isTrigger = true;
            canvas.DrawSprite(tutorial);
            AddChild(canvas);
            AddChild(tutorial);
            AddChild(_button);
            _button.x = (game.width - _button.width) / 2;
            _button.y = 310;
        }

        void Update() //gameover vkliuchi ctuby on ctoto dellat tut
        {
            if (Input.GetMouseButtonUp(0) && !_hasStarted)
            {
                if (_button.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    startGame(1);
                    music = new Sound("music.mp3", false, true);
                    musicChannel = music.Play();
                    hideMenu();
                }
            }
            if (Input.GetMouseButtonUp(0) && level1completed)
            {
                if (_button.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    hideMenu();
                    startGame(2);
                    music = new Sound("music.mp3", false, true);
                    musicChannel.Stop();
                    musicChannel = music.Play();
                }
            }
            if (Input.GetMouseButtonUp(0) && level2completed)
            {
                if (_button.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    hideMenu();
                    level1completed = false;
                    level2completed = false;
                    startGame(1);
                    music = new Sound("music.mp3", false, true);
                    musicChannel.Stop();
                    musicChannel = music.Play();

                }
            }
            if (Input.GetMouseButtonUp(0) && _gameover && !level1completed)
            {
                if (_button.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    _gameover = false;
                    hideMenu();
                    startGame(1);
                    music = new Sound("music.mp3", false, true);
                    musicChannel.Stop();
                    musicChannel = music.Play();
                }
            }
            if (Input.GetMouseButtonUp(0) && _gameover && level1completed)
            {
                if (_button.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    _gameover = false;
                    hideMenu();
                    music = new Sound("music.mp3", false, true);
                    musicChannel.Stop();
                    musicChannel = music.Play();
                }
            }
            if (!level1completed && Player._score >= 10)
            {
                RemoveChild(level);
                AddChild(nextLevelSprite);
                AddChild(_button);
                level1completed = true;
                Player._score = 0;
            }
            if (!level2completed && Player._score >= 10)
            {
                RemoveChild(level2);
                AddChild(winSprite);
                AddChild(_button);
                level2completed = true;
                Player._score = 0;
            }
            if (!level1completed && _gameover)
            {
                RemoveChild(level);
                AddChild(restartSprite);
                AddChild(_button);
                //_gameover = false;
                Player._score = 0;
                musicChannel.Stop();
            }
            if (level1completed && _gameover)
            {
                RemoveChild(level2);
                AddChild(restartSprite);
                AddChild(_button);
                //_gameover = false;
                Player._score = 0;
                musicChannel.Stop();
            }
            
            Console.WriteLine(_gameover);
        }

        void hideMenu()
        {

            RemoveChild(canvas);
            RemoveChild(_button);
            RemoveChild(tutorial);
            RemoveChild(nextLevelSprite);
        }

        void startGame(int currentLevel)
        {
            switch (currentLevel)
            {
                case 1:
                AddChild(level);
                RemoveChild(level2);
                _hasStarted = true;
                break;

                case 2:
                RemoveChild(level);
                AddChild(level2);
                break;
            }
        }
        

    }
}
