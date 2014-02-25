﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameObject selectedObject;
	private Vector3 homePosition;
	private Vector3 offset = Vector3.forward * 8;

	// Use this for initialization
	void Start () {
		homePosition = transform.position;
		selectedObject = null;
	}

	void selectObject(GameObject obj){
		(obj.GetComponent("Halo")as Behaviour).enabled = true;
		selectedObject = obj;
	}

	void deselectObject(GameObject obj) {
		(obj.GetComponent("Halo")as Behaviour).enabled = false;
		// Reset Camera
		transform.position = homePosition;
		selectedObject = null;
	}

	// Update is called once per frame
	void Update () {
		RaycastHit hit;

		if (Input.GetTouch(0).phase == TouchPhase.Began) {
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

			if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
				GameObject target = hit.transform.gameObject;

				if(selectedObject != null && target.tag == selectedObject.tag){
					deselectObject(target);
				} else {
					selectObject(target);
				}
			}
		}
	}

	void LateUpdate() {
		if(selectedObject != null){
			transform.position = selectedObject.transform.position - offset;
		}
	}
}