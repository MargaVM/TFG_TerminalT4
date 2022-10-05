using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System;
//We import a .dll file that contains the necessary library to the conection ssh with the raspberry pi
using Renci.SshNet;

public class datosSensores : MonoBehaviour {

	//We create several strings to the conection with the raspberry (ip, port, user name and password)
	UnityEngine.UI.Text text = null;
	string _host = "192.168.1.41";
	string _username = "pi";
	string _password = "tfg2018";
	string parseo = "none";
	string [] arrayParseo;

	
	//We call the method "accederSensor" to obtain the data of temperature and humidity and show it on the app
	void Start () {
		accederSensor ();
	}
		
	void accederSensor ()
	{
		//We initialize the text
		text = this.GetComponent<UnityEngine.UI.Text> ();

		try
		{
			//We connect with the previous strings and port 22
			var connectionInfo = new PasswordConnectionInfo(_host, 22, _username, _password);

			using (var client = new SshClient(connectionInfo ))
			{
				//We connect
				client.Connect();
				//We execute the script that reads the information of the sensors
				var command = client.RunCommand("sudo python /home/pi/Adafruit_Python_DHT/examples/AdafruitDHT.py 22 4");
				//We collect this info
				parseo = command.Result.ToString();
				//We parse the information
				arrayParseo = parseo.Split('=', ' ');

				//We copy in the variable the text that we will show on the app (in 1 and 4 positions are temperature and humidity respectively)
				text.text = arrayParseo [1] + "C" + '\n' + arrayParseo [4];
				client.Disconnect();
			}
		}
		catch(System.Exception e)
		{
			//If we cannot connect it shows this error
			text.text = "Error\n" + e;

		}
	} 
}
