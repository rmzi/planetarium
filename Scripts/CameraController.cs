using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameObject selectedObject;
	private Vector3 homePosition;
	private Vector3 homeOrientation;
	private Vector3 offset = Vector3.forward * 10;
	private bool sunSelected = false;
	private GameObject[] planets;

	// Use this for initialization
	void Start () {
		homePosition = transform.position;
		homeOrientation = transform.forward;
		selectedObject = null;
		planets = GameObject.FindGameObjectsWithTag ("Planet");
	}

	void startPlanets(){
		foreach(GameObject planet in planets){
			// Used empty object to add more tags to a single object
			GameObject actualObject = planet.transform.parent.gameObject;

			actualObject.GetComponent<Revolver>().speed = Random.Range(5,55);
			actualObject.GetComponent<Rotater>().speed = Random.Range(5,55);
		}
	}

	void stopPlanets(){
		foreach(GameObject planet in planets){
			// Used empty object to add more tags to a single object
			GameObject actualObject = planet.transform.parent.gameObject;

			actualObject.GetComponent<Revolver>().speed = 0;
			actualObject.GetComponent<Rotater>().speed = 0;
			
			//(actualObject.GetComponent("Revolver")as Behaviour).enabled = false;
			//(actualObject.GetComponent("Rotater")as Behaviour).enabled = false;
		}
	}

	void selectObject(GameObject obj){
		(obj.GetComponent("Halo")as Behaviour).enabled = true;
		selectedObject = obj;

		if (selectedObject.tag == "Sun") {
			sunSelected = true;
			stopPlanets();
		}
			
	}

	void deselectSun(){
		if (sunSelected) {
			sunSelected = false;
			startPlanets();
		}
	}

	void deselectObject(GameObject obj) {
		(obj.GetComponent("Halo")as Behaviour).enabled = false;
		// Reset Camera
		transform.position = homePosition;
		transform.forward = homeOrientation;

		deselectSun();

		selectedObject = null;
	}

	// Update is called once per frame
	void Update () {
		RaycastHit hit;

		// Desktop Version
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				GameObject target = hit.transform.gameObject;

				if (selectedObject != null) {
					if (target.tag == selectedObject.tag) {
						deselectObject (target);
					} else {
						(selectedObject.GetComponent ("Halo")as Behaviour).enabled = false;
						deselectSun();
						selectObject (target);
					}
				} else {
					selectObject (target);
				}
			}
		}
		// End Desktop Version

		// Mobile Version
		/**
		if (Input.GetTouch(0).phase == TouchPhase.Began) {
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

			if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
				GameObject target = hit.transform.gameObject;

				if(selectedObject != null){
					if(target.tag == selectedObject.tag){
						deselectObject(target);
					} else {
						(selectedObject.GetComponent("Halo")as Behaviour).enabled = false;
						selectObject(target);
					}
				} else {
					selectObject(target);
				}
			}
		}
		*/
		// End Mobile Version
	}

	void LateUpdate() {
		if(selectedObject != null){
			transform.LookAt(selectedObject.transform);
			transform.position = selectedObject.transform.position - offset;
		}
	}

	void OnGUI () {
		if (GUI.Button (new Rect (10,10,150,100), "I am a button")) {
			print ("You clicked the button!");
		}
	}
}