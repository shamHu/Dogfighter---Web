using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartMenu : FContainer {
	private FSprite title;
	private FButton startButton;
	private FButton optionsButton;
	private FButton exitButton;
	
	public StartMenu() {
		if (DogfighterMain.instance.musicOn &&
			DogfighterMain.instance.titleMusic)
		{
			DogfighterMain.instance.titleMusic = false;
			FSoundManager.PlayMusic("Music/CiderTime", 0.25f);
		}
		
		//Create and show menu buttons
		title = new FSprite("MM_Title_1.png");
		title.scale = 0.25f;
		title.y = 75.0f;
		AddChild(title);
		
		startButton = new FButton("MM_START.png", "MM_START_Selected.png");
		startButton.scale = 0.5f;
		AddChild(startButton);
		
		optionsButton = new FButton("MM_OPTIONS.png", "MM_OPTIONS_Selected.png");
		optionsButton.scale = 0.5f;
		optionsButton.y = -60.0f;
		AddChild(optionsButton);
		
		exitButton = new FButton("MM_EXIT.png", "MM_EXIT_Selected.png");
		exitButton.scale = 0.5f;
		exitButton.y = -110.0f;
		AddChild(exitButton);
		
		//Adds listener to menu buttons that calls the associated function
		startButton.SignalRelease += StartButtonRelease;
		optionsButton.SignalRelease += OptionsButtonRelease;
		exitButton.SignalRelease += ExitButtonRelease;
		
	}
	// code relating to visible start button
	private void StartButtonRelease(FButton button) {
		
		DogfighterMain.instance.SwitchToShipSelect();
	}
	// code relating to visible options button
	private void OptionsButtonRelease(FButton button) {
		
		DogfighterMain.instance.SwitchToOptions();
	}
	// code relating to visible exit button
	private void ExitButtonRelease(FButton button) {
		
		DogfighterMain.instance.ExitConfirm();
	}
}

