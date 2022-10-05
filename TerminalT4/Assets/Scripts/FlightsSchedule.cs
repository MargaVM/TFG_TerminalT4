using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class FlightsSchedule : MonoBehaviour {

	// Global Variables 

	//To display departure and arrival flights
	public Text TextFieldS;
	public Text TextFieldL;
	//We convert to String the txt with the flights
	string cadenaS;
	string cadenaL;
	//We create the arrays to include inside the info of the Strings of the flights
	string[] arraySalida;
	string[] arrayLlegada;
	//Number of airplanes that are going to be visible at once in departures and arrives(usually 8)
	int avionesAbajo;
	int avionesArriba;
	//Gameobject child of the T4 Building, where are saved all the departures airplanes
	GameObject FlotaAvionesSalida;
	//Gameobject child of the T4 Building, where are saved all the arrives airplanes
	GameObject FlotaAvionesLlegada;
	//Gameobject of the T4 Building
	GameObject T4Prefab;
	//Queues of departures and arrives to manage the flights
	static Queue<GameObject> colaSalida = new Queue<GameObject>();
	static Queue<GameObject> colaPista = new Queue<GameObject>();
	//Boolean indicating free track and a Float countdown to let the airplanes take off once at a time
	public bool pistaLibre;
	float countdown;

	//This method makes visibles the airplanes once we place the airport on the screen
	public void visibilidadAviones()
	{

	 	GeneradorVuelos gv1 = new GeneradorVuelos();
		avionesAbajo = gv1.datosVuelosSalidas.Length;
		avionesArriba = gv1.datosVuelosLlegadas.Length;

		arraySalida = gv1.datosVuelosSalidas;
		arrayLlegada = gv1.datosVuelosLlegadas;

		for (int j = 0; j < avionesAbajo; j++) {
			cadenaS = '\n' + gv1.datosVuelosSalidas[j];
			print (cadenaS);
		}

		for (int z = 0; z < avionesArriba; z++) {
			cadenaL = '\n' + gv1.datosVuelosLlegadas[z];
			print (cadenaL);
		}

		//To avoid changes on the prefab, we call the method "devolverInstancia" of the class "T4Controller" and work with this gameobject
		T4Prefab = GameObject.Find("UIController").GetComponent<T4UIController> ().devolverInstancia ();

		//We access to childs 0 and 1 of the instance of the T4 Building created (departure airplane fleet and arrive airplane fleet)
		FlotaAvionesSalida = T4Prefab.transform.GetChild (0).gameObject;
		FlotaAvionesLlegada = T4Prefab.transform.GetChild (1).gameObject;

		//We go over all the departure airplane fleet and make them visibles, copying the info of each one inside
		for (int contador = 0; contador < avionesAbajo; contador++) {

			FlotaAvionesSalida.transform.GetChild (contador).gameObject.SetActive (true);
			GameObject avion = FlotaAvionesSalida.transform.GetChild (contador).gameObject;
			TextMesh tm1 = avion.transform.GetChild(0).gameObject.GetComponent<TextMesh> ();

		}

		//We make the same action to the departure airplane fleet, but the don´t are visible yet until they get out of queue and the track is clear for landing
		for (int contador2 = 0; contador2 < avionesArriba; contador2++) {

			GameObject avion2 = FlotaAvionesLlegada.transform.GetChild (contador2).gameObject;
			TextMesh tm2 = avion2.transform.GetChild(0).gameObject.GetComponent<TextMesh> ();
		}
	}

	//Method to manage the queues of departures and arrives
	public void actualizarColas (){

		//We store the actual hour and minutes
		int hora = System.DateTime.Now.Hour;
		int minutos = System.DateTime.Now.Minute;
		int minutosArriba = minutos + 10;
		int minutosAbajo = minutos - 10;

		//We go over the departure airplanes and if the flight that contains is near the actual hour (10 minutes up or down) it gets in the departure queue 
		for (int i = 0; i < avionesAbajo; i++){
			//We take the info of the airplane where we are
			GameObject avion = FlotaAvionesSalida.transform.GetChild (i).gameObject;
			TextMesh tm1 = avion.transform.GetChild(0).gameObject.GetComponent<TextMesh> ();

			//If this info contains a flight that is going to departure 10 minutes up or 10 minutes down from the actual hour 
			for(int j= minutosAbajo; j<minutosArriba; j++){
				if (tm1.text.Contains(hora.ToString()) == true) {
					if(tm1.text.Contains(j.ToString()) == true){
						//It puts this flight into the departures queue
						colaSalida.Enqueue(FlotaAvionesSalida.transform.GetChild(i).gameObject);
					}
				}
			}
		}

		//We make the same action to the arrives flights
		for (int z = 0; z < avionesArriba; z++){
			//We take the info of the airplane where we are
			GameObject avion2 = FlotaAvionesLlegada.transform.GetChild (z).gameObject;
			TextMesh tm2 = avion2.transform.GetChild(0).gameObject.GetComponent<TextMesh> ();


			//If this info contains a flight that is going to departure 10 minutes up or 10 minutes down from the actual hour
			for (int a = minutosAbajo; a < minutosArriba; a++) {
				if (tm2.text.Contains (hora.ToString ()) == true) {
					if (tm2.text.Contains (a.ToString ()) == true) {
						//It puts this flight into the arrives queue
						colaPista.Enqueue (FlotaAvionesLlegada.transform.GetChild(z).gameObject);
					}
				}
			}
		}
	}

	//In this method we launch the flights on queues
	public void lanzarColas (){

		//This variables shows the name of the airplane we are getting out of the queue and if the track is free or no
		string nombreAnim;
		pistaLibre = true;

		//We select random between departures and arrives
		int randomCola =  UnityEngine.Random.Range(0,2);

		//If there are elements in the departure queue and the number corresponding to the queue has been selected
		if(colaSalida.Count > 0 && randomCola == 0){
			//If the track is free
			if (pistaLibre == true){
				//We instanciate the script "airplanetext" and the airplane that is in the departure queue
				AirplaneText apt1 = GameObject.Find (colaSalida.Peek ().name).GetComponent<AirplaneText> ();
				//We call the function "ordenAnimar" which launch the animation of that airplane from the script "airplanetext" attached
				apt1.ordenAnimar ();
			}
			//After calling this method, we dequeue that airplane
			colaSalida.Dequeue ();
		}

		//Here we make the same operations than before, if the random number is 1, we make it from queue of landings. The difference between both is here we need to make visible the airplanes before
		else if (colaPista.Count > 0 && randomCola == 1){
			//If the track is free
			if (pistaLibre == true){
				//We check the gameobject from the queue
				GameObject go2 = colaPista.Peek ();
				//We make it visible
				go2.SetActive (true);
				//We instanciate its script and call to "ordenAnimar" to lauch the animation
				AirplaneText apt2 = go2.GetComponent<AirplaneText> ();
				apt2.ordenAnimar ();
			}
			//We dequeue
			colaPista.Dequeue ();
		}

	}

	//Method where call to previous ones just one time
	public void StartSchedule () {

		visibilidadAviones ();
		actualizarColas ();
		lanzarColas ();
	}

	//At initialization we put the counter to 10 seconds
	void Start () {
		countdown = 10.0f;
	}
	
	//In this function, executed in every frame, we decrease the countdown and we call to the method "lanzarColas" every 10 seconds
	void Update () {
		countdown -= Time.deltaTime;

		if (countdown < 0.0f && pistaLibre == true){
			lanzarColas ();
			countdown = 10.0f;
		}
	}
}
