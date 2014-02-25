using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameObject selectedObject;
	//private Vector3 homeLocation;
	private Vector3 offset = Vector3.forward * 10;

	// Use this for initialization
	void Start () {
		//homeLocation = transform.position;
		selectedObject = null;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;

		if (Input.GetTouch(0).phase == TouchPhase.Began) {
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

			if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
				GameObject target = hit.transform.gameObject;
				(target.GetComponent("Halo")as Behaviour).enabled = true;

				selectedObject = target;
			}
		}
		
		if(selectedObject != null){
			transform.position = selectedObject.transform.position - offset;
		}
	}
}