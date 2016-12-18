using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour {

	public Vector3 hsv {
		set {
			Vector3 rgbVec = new Vector3();
			rgb = Colors.HsvToRgb(value.x, value.y, value.z, rgbVec.x, rgbVec.y, rgbVec.z);
		}
	}

	public Vector3 rgb {
		set {

		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
