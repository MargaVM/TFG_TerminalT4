using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {


	//We use this method at app the app start screen, it changes the scene touching the button "Inicio"
	public void NewGameBtn (string newGameLevel) {
    	SceneManager.LoadScene(newGameLevel);
	}

	//We use this method at app the app start screen, getting out the app touching the button "Salir"
	public void ExitGameBtn () {
    	Application.Quit();
	}
}
