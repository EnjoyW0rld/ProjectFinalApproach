using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;
using System.IO;

public class MyGame : Game
{
	List<Scene> scenes = new List<Scene> ();
	public MyGame() : base(1920, 1080, false,false)		// Create a window that's 800x600 and NOT fullscreen
	{
		//Scene sc = new Scene("level1.tmx");
		targetFps = 60;
		scenes.Add(new Scene("level1.tmx"));
        //AddChild(scenes[0]);
        foreach (var item in scenes)
        {
			AddChild(item);
        }
		EventsHandler.LevelChange += ChangeTo;
	}

	// For every game object, Update is called every frame, by the engine:
	void Update()
	{
		//Gizmos.DrawLine(,null,255);
		// Empty
	}
	void ChangeTo()
    {
        Console.WriteLine("levelChanged");
    }
	static void Main()							// Main() is the first method that's called when the program is run
	{

		new MyGame().Start();					// Create a "MyGame" and start it
	}
    /*public Player GetPlayer()
    {
        return player;
    }*/
}