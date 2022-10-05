using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class T4UIController : MonoBehaviour
{
	//This varibles are used to create, place and scale the airport
    public GameObject T4Mesh;
    TangoPointCloud m_pointCloud;
	bool emplazado = false;
	GameObject instanciado;
	public Button tmas;
	public Button tmenos;
	public Button rmas;
	public Button rmenos;
	public Button emas;
	public Button emenos;

	//We need to access the Tango components
    public void Start()
    {
        m_pointCloud = FindObjectOfType<TangoPointCloud>();

    }
	//In update we are expecting a screen touch to place the airport
	public void Update ()
    {
		if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
			if (t.phase == TouchPhase.Ended)
            {
                PlaceT4(t.position);

            }
        }
		desplazarT4 ();
    }

	//This is the method to place the airport 
	public void PlaceT4(Vector2 touchPosition)
    {
		//It opens the camera
        Camera cam = Camera.main;
		//Try to find a perpendicular vector
        Vector3 planeCenter; 
        Plane plane;
		//If doesnt find anyone it shows an error
        if (!m_pointCloud.FindPlane(cam, touchPosition, out planeCenter, out plane))
        {
            Debug.Log("cannot find plane.");
            return;
        }
		//If it finds a perpendicular vector, the airport is instantiated
		if (Vector3.Angle (plane.normal, Vector3.up) < 30.0f && emplazado == false) 
		{
			Vector3 up = plane.normal;
			Vector3 right = Vector3.Cross (plane.normal, cam.transform.forward).normalized;
			Vector3 forward = Vector3.Cross (right, plane.normal).normalized;
			instanciado = Instantiate (T4Mesh, planeCenter, Quaternion.LookRotation (forward, up));
			emplazado = true;
		} 

    }
	//This method is called to return the gameobject of the airport
	public GameObject devolverInstancia (){

		return instanciado;
	
	}
	//This method is called once the airport is on camera to place it wherever you want, with buttons to move, scale and  rotate it 
	void desplazarT4 () 
	{
		Button btn1 = tmas.GetComponent<Button>();
		Button btn2 = tmenos.GetComponent<Button>();
		Button btn3 = rmas.GetComponent<Button>();
		Button btn4 = rmenos.GetComponent<Button>();
		Button btn5 = emas.GetComponent<Button>();
		Button btn6 = emenos.GetComponent<Button>();

		btn1.onClick.AddListener (pulsoTrasladarMas);
		btn2.onClick.AddListener (pulsoTrasladarMenos);
		btn3.onClick.AddListener (pulsoRotarMas);
		btn4.onClick.AddListener (pulsoRotarMenos);
		btn5.onClick.AddListener (pulsoEscalarMas);
		btn6.onClick.AddListener (pulsoEscalarMenos);
	}
	//When the button of move right is touched, with this method we move to the right the airport
	public void pulsoTrasladarMas () {
		instanciado.transform.Translate (0.0001F,0.0F,0.0F);
	
	}
	//When the button of move left is touched, with this method we move to the left the airport
	void pulsoTrasladarMenos () {
		instanciado.transform.Translate (-0.0001F,0.0F,0.0F);
	
	}
	//When the button of rotate right is touched, with this method we rotate to the right the airport
	void pulsoRotarMas () {
		instanciado.transform.Rotate(new Vector3(0,0.01F,0));
	
	}
	//When the button of rotate left is touched, with this method we rotate to the left the airport
	void pulsoRotarMenos () {
		instanciado.transform.Rotate(new Vector3(0,-0.01F,0));

	}
	//When the button of scale bigger is touched, with this method we make the airport bigger
	void pulsoEscalarMas () {
		instanciado.transform.localScale += new Vector3(0.0001F, 0.0001F, 0.0001F);
	
	}
	//When the button of scale smaller is touched, with this method we make the airport smaller
	void pulsoEscalarMenos () {
		instanciado.transform.localScale -= new Vector3(0.0001F, 0.0001F, 0.0001F);

	}
		
}
 