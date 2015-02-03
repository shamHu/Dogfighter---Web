using UnityEngine;
using System.Collections;

public class DogfighterMain : MonoBehaviour {
	
	private FContainer currentPage;
	public static DogfighterMain instance;
	public bool musicOn = true;
	public bool titleMusic = true;
	public bool soundOn = true;
	
	// Use this for initialization
	private void Start() {
		Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//Initialize Futile
		instance = this;
				
		FutileParams fparams = new FutileParams(true, true, false, false);
		
		fparams.AddResolutionLevel(480.0f, 1.0f, 1.0f, "");
		
		fparams.origin = new Vector2(0.5f, 0.5f);
		
		Futile.instance.Init(fparams);
		
		Futile.atlasManager.LoadAtlas("Atlases/BananaGameAtlas_Scale1");
		Futile.atlasManager.LoadAtlas("Atlases/Ship_Select_Atlas_2");
		Futile.atlasManager.LoadAtlas("Atlases/19th_Sheet");
		Futile.atlasManager.LoadAtlas("Atlases/Menu_Sheet_1");
		Futile.atlasManager.LoadAtlas("Atlases/BG_1");
		Futile.atlasManager.LoadAtlas("Atlases/SS_and_Options_Extras_1");
		Futile.atlasManager.LoadFont("FranchiseFont_Scale1", 
			"FranchiseFont_Scale1.png", "Atlases/FranchiseFont_Scale1");
		
		SwitchToStartMenu();
	}

	public void SwitchToStartMenu() {
		
		if(currentPage != null)
			currentPage.RemoveFromContainer();
		
		currentPage = new StartMenu();
		Futile.stage.AddChild(currentPage);
	}
	
	public void SwitchToGame() {
		
		if(currentPage != null)
			currentPage.RemoveFromContainer();
		
		currentPage = new InGamePage();
		Futile.stage.AddChild(currentPage);
	}
	
	public void SwitchToShipSelect() {
	
		if(currentPage != null)
			currentPage.RemoveFromContainer();
		
		currentPage = new ShipSelect();
		Futile.stage.AddChild(currentPage);
	}
	
	public void SwitchToOptions() {
		
		if(currentPage != null)
			currentPage.RemoveFromContainer();
		
		currentPage = new OptionsMenu();
		Futile.stage.AddChild(currentPage);
	}

	public void ExitConfirm() {
		
		if(currentPage != null)
			currentPage.RemoveFromContainer();
		
		currentPage = new ExitConfirm();
		Futile.stage.AddChild(currentPage);
	}
	
	public void setMusic(bool music) {
		musicOn = music;	
	}
	// Update is called once per frame
	void Update() {
	}
}
