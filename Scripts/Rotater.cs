﻿using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

	public int speed;

	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * speed * Time.deltaTime);
	}
}
