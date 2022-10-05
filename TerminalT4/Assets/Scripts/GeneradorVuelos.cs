using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class GeneradorVuelos : MonoBehaviour {

	//This global variables store the info of all the departure and arrive flights
	public string [] datosVuelosSalidas;
	public string [] datosVuelosLlegadas;

	//We create this method to manage the flights
	public void crearVuelos(){

		//We create several flights with differents origins and destinations and we store it inside a array
		int hora = System.DateTime.Now.Hour;
		int minutos = System.DateTime.Now.Minute;
		int diferencia = 60 - minutos;
		string pathS = "Assets/Resources/VuelosSalida.txt";
		string pathL = "Assets/Resources/VuelosLlegada.txt";
		StreamWriter writer = new StreamWriter(pathS, true);
		StreamWriter writer2 = new StreamWriter(pathL, true);

		string [] destinos =  new string[] {"LONDRES", "PARIS", "NUEVA YORK", "ROMA", "BERLIN", "DUBLIN", "MANCHESTER", "ESCOCIA", "BARCELONA", "SEVILLA",
			"VALENCIA", "SANTIAGO DE COMPOSTELA", "VARSOVIA", "MILAN", "CAGLIARI", "BOSTON", "CARACAS", "MEXICO DF", "SAO PAULO", "ATENAS", "FLORENCIA", "FRANKFURT",
			"NANTES", "MARSELLA", "TOKYO", "LUXEMBURGO", "OPORTO", "BRUSELAS", "OSLO", "COPENHAGUE", "AMSTERDAM", "LUQA", "BRATISLAVA", "GINEBRA",
			"BUDAPEST", "VIENA", "HELSINKI", "MIAMI", "FILADELFIA", "LOS ANGELES", "BANGKOK", "NAIROBI", "SHANGHAY", "TORONTO", "MONTREAL", "PUNTA CANA", 
			"CANCÚN", "SAN JOSÉ", "LA HABANA", "MALÉ", "SIDNEY"};
		string[] identificadores = new string[] {"IBE7364", "IBE9112", "IBE7444", "IBE1130", "IBE2345", "IBE9834", "ESJ0982", "ESJ9264", "ESJ1242", 
			"ESJ9937", "ESJ8354", "ESJ8755", "RYA9010", "RYA9766", "RYA1123", "RYA9843", "RYA6785", "RYA5847", "RYA6678", "RYA0876", "RYA1412", "RYA4564",
			"VUE39236", "VUE39936", "VUE7732", "VUE1019", "VUE7034", "VUE3132", "VUE6233", "FEM6739", "FEM7443", "FEM2345", "FEM1124", "FEM4431", 
			"FEM3654", "FEM6665", "FEM7542", "FEM9731", "FEM0012", "FEM3551", "FEM0915", "FEM7721"};

		for (int i = 0; i < 8; i++) {
			if (diferencia < 20 && diferencia > 10) {
				datosVuelosSalidas [i] = " " + hora.ToString () + ":" + UnityEngine.Random.Range (minutos, diferencia).ToString () + "    " + RandomItem (destinos) + "       " + RandomItem (identificadores); 

			}
			else if (diferencia < 10) {
				datosVuelosSalidas [i] = " " + (hora+1).ToString () + ":" + UnityEngine.Random.Range (minutos, diferencia+20).ToString () + "    " + RandomItem (destinos) + "       " + RandomItem (identificadores); 
			} 
			else {
				datosVuelosSalidas [i] = " " + hora.ToString () + ":" + UnityEngine.Random.Range (minutos, diferencia).ToString () + "    " + RandomItem (destinos) + "       " + RandomItem (identificadores); 
				System.IO.File.WriteAllText (pathS, vuelo);
			}
		}
			//We store all the departures flights created on the text file "VuelosSalida"
			Resources.GetBuiltinResource<File> ();
			Resources.Load ("VuelosSalida") as TextAsset;
			writer.WriteLine(vuelo);
		writer.Close();

		for (int i = 0; i < 8; i++) {
			datosVuelosLlegadas [i] = " " + hora.ToString () + ":" + UnityEngine.Random.Range (minutos, diferencia).ToString () + "    " + RandomItem (destinos) + "       " + RandomItem (identificadores); 
			System.IO.File.WriteAllText (pathL, vuelo);
			writer2.WriteLine(vuelo);
		}
		writer2.Close();
	}
	//This method select a random info to create the flights
	static string RandomItem (string [] array)
	{
		return array [UnityEngine.Random.Range (0, array.Length)];
	}

	

}
