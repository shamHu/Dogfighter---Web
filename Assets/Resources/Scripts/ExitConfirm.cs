using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ExitConfirm : FContainer{
	private FLabel title;
	private FButton mainMenuButton;
	private FButton exitButton;
	
	public ExitConfirm (){
		//Create and show menu buttons
		title = new FLabel("FranchiseFont_Scale1","QUIT?");
		title.y = 100.0f;
		title.scaleX = 1.75f;
		title.scaleY = 1.75f;
		AddChild(title);
		
		mainMenuButton = new FButton("YellowButton_normal.png","YellowButton_over.png");
		mainMenuButton.AddLabel("FranchiseFont_Scale1","NO",Color.white);
		mainMenuButton.x = -75.0f;
		mainMenuButton.y = -50.0f;
		AddChild(mainMenuButton);

		exitButton = new FButton("YellowButton_normal.png","YellowButton_over.png");
		exitButton.AddLabel("FranchiseFont_Scale1","YES",Color.white);
		exitButton.x = 75.0f;
		exitButton.y = -50.0f;
		AddChild(exitButton);


		
		//Adds listener to menu buttons that calls the associated function
		mainMenuButton.SignalRelease += MainMenuButtonRelease;
		exitButton.SignalRelease += ExitButtonRelease;
		
	}

	private void MainMenuButtonRelease (FButton button){
		
		DogfighterMain.instance.SwitchToStartMenu();
	}
	
	private void ExitButtonRelease (FButton button){
		
		Application.Quit();
	}

	// Adds and removes listeners for things added to stage and calls HandleUpdate
	override public void HandleAddedToStage(){
		Futile.instance.SignalUpdate += HandleUpdate;
		base.HandleAddedToStage();		
	}
	
	override public void HandleRemovedFromStage(){
		Futile.instance.SignalUpdate -= HandleUpdate;
		base.HandleRemovedFromStage();
	}

	// Listens for escape key to return to main menu, or return (enter) key to exit game
	private void HandleUpdate(){
		if(Input.GetKey(KeyCode.Escape)){
			DogfighterMain.instance.SwitchToStartMenu();
		}
		if(Input.GetKey(KeyCode.Return)){
			Application.Quit();
		}
	}
}