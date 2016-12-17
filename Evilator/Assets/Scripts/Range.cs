using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour {

	public HashSet<Transform> playersInRange = new HashSet<Transform>();

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			playersInRange.Add(other.transform);
			print("In range: " + playersInRange.Count);
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.tag == "Player") {
			playersInRange.Remove(other.transform);
		}
	}
}
