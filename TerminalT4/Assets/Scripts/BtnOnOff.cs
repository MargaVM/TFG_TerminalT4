using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOnOff : MonoBehaviour {

	//As global variables we have the gameobject that we want that appears and desappears and the boolean to confirm the state of the button
	bool toggle =false ;
	public GameObject fondoVuelos;

	//This method makes visible the departures and arrives flights with one touch on the screen, and make it invisible with two
	public void cambioEstado () 
	{

		if(!toggle)
		{
			fondoVuelos.SetActive(true);
			toggle= true;

		}
		else 
		{
			fondoVuelos.SetActive(false);
			toggle= false;
		}
	}
}
