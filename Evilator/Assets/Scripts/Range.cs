﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour {

	public HashSet<Transform> playersInRange = new HashSet<Transform>();
	public Transform phoneInRange;

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			playersInRange.Add(other.transform);
		} else if(other.tag == "Phone") {
			phoneInRange = other.transform;
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.tag == "Player") {
			playersInRange.Remove(other.transform);
		} else if(other.tag == "Phone") {
			phoneInRange = null;
		}
	}
}
