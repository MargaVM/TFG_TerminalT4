using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScreen : MonoBehaviour {

	//This global variables are public because it is necessary the manage the gameobjects int the inspector of unity, to make them visibles/unvisibles
	public GameObject contenedorPantallaAnterior;
	public GameObject contenedorPantallaNuevo;
	public GameObject mensaje;
	public Button yourButton;
	bool pantallaColocada = false;

	//In update we wait for a touch on the screen or in the button "fijar" to change the scene showing
	public void Update () 
	{
		//If the button "fijar" is touched we call the function "pulsoBotonFijar"
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(pulsoBotonFijar);

		//If we touch the screen and the variable "pantallaColocada" is false, we get in
		if(Input.touchCount == 1 && pantallaColocada == false)
		{
			Touch t = Input.GetTouch(0);
			//If the touch on the screen has finished, we get in
			if (t.phase == TouchPhase.Ended)
			{
				//We hide the message of place the airport and we activate the display container where we can modify the position of the airport
				mensaje.SetActive(false);
				contenedorPantallaAnterior.SetActive(true);
				pantallaColocada = true;
			}
		}
	}

	//This method makes disappear the part where the buttons are showed to adapt the position and scale of the airport and shows the display container where we can see the departures, arrives and sensor info (humidity an temperature)
	public void pulsoBotonFijar () {
		contenedorPantallaAnterior.SetActive(false);
		contenedorPantallaNuevo.SetActive(true);
	}
}