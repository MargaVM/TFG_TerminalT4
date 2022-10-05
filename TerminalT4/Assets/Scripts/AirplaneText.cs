using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirplaneText : MonoBehaviour {

	//Global variables
	int tocado = 0;
	string nombreAnim;
	string nombreObjeto;
	FlightsSchedule id1;
	GameObject panel;
   
	
	//In Update we call the method that detects if we have touched an airplane and shows the associated info
	void Update () {

		infoTocarAvion ();

	}

	//This method will take the point of the screen where we touch, and if it coincides with the collider of an airplane, it will show the information of the flight
	void infoTocarAvion (){

		//The gameObject panel is where we save the Text Mesh with the airplane info and it is the first child (in the unity hierarchy) of each airplane.
		panel = this.transform.GetChild (0).gameObject;
		//We take the name of the airplane through the animation name because it is the same
		nombreObjeto = this.GetComponent<Animation> ().name;

		//We store each touch on the screen
		Touch t = Input.GetTouch (0);
		//We generate a raycast in this position
		Ray raycast = Camera.current.ScreenPointToRay (t.position);
		RaycastHit raycastHit;

		//If there is a touch on the screen
		if (Input.touchCount > 0) {
			//If that touch has ended and the raycast crash with some object
			if (t.phase == TouchPhase.Ended && Physics.Raycast (raycast, out raycastHit) ){

				//If the object with which we have collide is the collider of this airplane and it is the first time that we touch it
				if (raycastHit.collider.name == nombreObjeto && tocado == 0) {
					//It makes visible the information of the flight
					panel.SetActive (true);
					//And we increase the variable "tocado"
					tocado = 1;
				}
				//If we touch this same colider for second time, the panel disappear 						
				else if (raycastHit.collider.name == nombreObjeto && tocado == 1){ 							
					tocado = 0 ;
					panel.SetActive (false);
				}
			}
		}
	}

	//This method is public because we need access from another script
	//When we remove an airplane fron the queue, this method is called within the airplane script
	public void ordenAnimar (){

		//We take the name of the airplane gameobject (it is the same of the animation attached)
		nombreAnim = this.GetComponent<Animation> ().name;
		//We instantiate the script that controls the flight queues
		id1 = new FlightsSchedule();

		//If the variable "pista libre" of the script (which controls the flights queues)is true, then we execute the animation of this plane
		if (id1.pistaLibre = true){
			this.GetComponent<Animation> ().Play (nombreAnim);
		} 
		//If the animation is running we will set the "pistaLibre" variable to false
		if (this.GetComponent<Animation> ().IsPlaying (nombreAnim) == true) {
			id1.pistaLibre = false;
		}

	}
}