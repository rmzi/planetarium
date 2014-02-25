using UnityEngine;
using System.Collections;

public class Revlover : MonoBehaviour {
	
	public int speed;
	public Vector3 direction;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(transform.parent.position, direction, speed * Time.deltaTime);
	}
}
