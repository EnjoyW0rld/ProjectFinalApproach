using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;
using System.IO;

public class MyGame : Game
{
	List<Scene> scenes = new List<Scene> ();
	EasyDraw transition;
	bool isLoading;
	int nextLevel;
	int previousLevel;

	Tween screenCover;
    Tween screenShake;

    public MyGame() : base(1920, 1080, false,false)		// Create a window that's 800x600 and NOT fullscreen
	{
		transition = new EasyDraw(1920, 1080,false);
		transition.Clear(0);
		transition.x = -1920;

		//AddChild();
		targetFps = 60;
        foreach (string item in Directory.GetFiles("Assets/Levels/"))
        {
			scenes.Add(new Scene(item));
        }
		scenes.Add(new DeathScreen(10));
		previousLevel = 0;
        AddChild(scenes[0]);
		EventsHandler.LevelChange += StartLoading;
		EventsHandler.ShakeScreen += ApplyScreenShake;
		//EventsHandler.LevelChange += ChangeTo;
		AddChild(transition);
	}

	// For every game object, Update is called every frame, by the engine:
	void Update()
	{
		if(Input.GetKeyDown(Key.B)) SoundManager.Instance().PlaySound(0);
		if(Input.GetKeyDown(Key.N)) SoundManager.Instance().StopAllSounds();
		//Gizmos.DrawLine(,null,255);
		// Empty
		if (isLoading && !transition.HasChild(screenCover))
		{
			ChangeTo(nextLevel); 
			isLoading = false;
		}

        if (Input.GetKeyDown(Key.R))
        {
            for (int i = 0; i < scenes.Count; i++)
            {
				if(nextLevel == 9 && scenes[i].sceneNumber == 10)
                {
                }
                
            }
					scenes[previousLevel] = scenes[previousLevel].RestartScene();
					EventsHandler.LevelChange?.Invoke(previousLevel);
        }
	}
	void ApplyScreenShake()
    {
        if (!HasChild(screenShake))
        {
			AddChild(screenShake = new Tween(Tween.Parameter.x, 2, 20, Tween.Function.Sin, 0, true));
        }
    }
	void ChangeTo(int n)
    {
		foreach (var item in GetChildren())
		{
			if(!(item is EasyDraw))
			RemoveChild(item);
		}
		foreach(Scene scene in scenes)
		{
			if(scene.sceneNumber == n + 1)
            {
				AddChildAt(scene, 0);

            } 
			if(scene.sceneNumber == 10)
            {
				((DeathScreen)scene).UpdateNextSceneNumber(n);
            }
		}

		transition.AddChild(new Tween(Tween.Parameter.x, 1, -1920, Tween.Function.easeInQuad));
    }

	void StartLoading(int i)
    {
		if(nextLevel != 9)
		previousLevel = nextLevel;
		nextLevel = i;
		if(!transition.HasChild(screenCover))
		transition.AddChild(screenCover = new Tween(Tween.Parameter.x, 1, 0, Tween.Function.easeInQuad));
		isLoading = true;
    }	

	static void Main()							// Main() is the first method that's called when the program is run
	{
		PlayerInfo.LoadPlayerInfo();
		new MyGame().Start();					// Create a "MyGame" and start it
	}
    /*public Player GetPlayer()
    {
        return player;
    }*/
}