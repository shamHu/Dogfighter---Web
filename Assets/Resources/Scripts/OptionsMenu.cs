using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class OptionsMenu : FContainer{
	private FLabel title;
	
	private FButton exitButton;
	private FSprite exitLabel;
	private FButton musicToggle;
	private FSprite musicOnLabel;
	private FSprite musicOffLabel;
	private FButton soundToggle;
	private FSprite soundOnLabel;
	private FSprite soundOffLabel;
	private FSprite soundLabel;
	
	public OptionsMenu (){
		//Create and show menu buttons
		title = new FLabel("FranchiseFont_Scale1","OPTIONS");
		title.y = 100.0f;
		title.scaleX = 1.75f;
		title.scaleY = 1.75f;
		AddChild(title);
		
		exitButton = new FButton("YellowButton_normal.png","YellowButton_over.png");
		exitButton.y = -100.0f;
		AddChild(exitButton);
		exitLabel = new FSprite("Exit_Options.png");
		exitLabel.y = -100f;
		exitLabel.x = 10f;
		exitLabel.scale = 0.25f;
		AddChild(exitLabel);
		
		musicToggle = new FButton("YellowButton_normal.png","YellowButton_over.png");
		musicToggle.y = -40.0f;
		AddChild(musicToggle);
		//musicToggle.AddLabel("FranchiseFont_Scale1","Music On/Off",Color.white);
		musicOnLabel = new FSprite("Music_On.png");
		musicOnLabel.y = -40f;
		musicOnLabel.x = 22f;
		musicOnLabel.scale = 0.25f;
		musicOffLabel = new FSprite("Music_off.png");
		musicOffLabel.y = -40f;
		musicOffLabel.x = 22f;
		musicOffLabel.scale = 0.25f;
		if (DogfighterMain.instance.musicOn)
			AddChild(musicOffLabel);
		else
			AddChild(musicOnLabel);
		
		soundToggle = new FButton("YellowButton_normal.png","YellowButton_over.png");
		soundToggle.y = 20.0f;
		AddChild(soundToggle);
		soundOnLabel = new FSprite("Sound_On.png");
		soundOnLabel.y = 20f;
		soundOnLabel.x = 20f;
		soundOnLabel.scale = 0.25f;
		soundOffLabel = new FSprite("Sound_Off.png");
		soundOffLabel.y = 20f;
		soundOffLabel.x = 20f;
		soundOffLabel.scale = 0.25f;
		if (DogfighterMain.instance.soundOn)
			AddChild(soundOffLabel);
		else
			AddChild(soundOnLabel);
		
		//Adds listener to menu buttons that calls the associated function
		exitButton.SignalRelease += ExitButtonRelease;
		musicToggle.SignalRelease += setMusic;
		soundToggle.SignalRelease += setSound;
		
	}
	
	
	private void ExitButtonRelease (FButton button){
		
		DogfighterMain.instance.SwitchToStartMenu();
	}
	
	private void setMusic(FButton button) {
		if (DogfighterMain.instance.musicOn == true)
		{
			DogfighterMain.instance.musicOn = false;
			FSoundManager.StopMusic();
			RemoveChild(musicOffLabel);
			AddChild(musicOnLabel);
		}
		else if (DogfighterMain.instance.musicOn == false)
		{
			DogfighterMain.instance.musicOn = true;
			DogfighterMain.instance.titleMusic = false;
			FSoundManager.PlayMusic("Music/intro", 0.25f);
			RemoveChild(musicOnLabel);
			AddChild(musicOffLabel);
		}
	}
	
	private void setSound(FButton button) {
		if (DogfighterMain.instance.soundOn == true) {
			DogfighterMain.instance.soundOn = false;
			RemoveChild(soundOffLabel);
			AddChild(soundOnLabel);
		}
		
		else if (DogfighterMain.instance.soundOn == false)
		{
			DogfighterMain.instance.soundOn = true;
			FSoundManager.PlaySound("Sounds/adrien techno", 5.0f);
			RemoveChild(soundOnLabel);
			AddChild(soundOffLabel);
		}
	}
}

