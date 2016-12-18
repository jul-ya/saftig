using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour {

	public Transform[] materialHolders;

	public string suitMatName = "Suite_mat";
	public string tieMatName = "Tie_mat";
	public string shirtMatName = "Shirt_mat";
	public string skinMatName = "Skin_mat";

	public float saturation = 0.9f;
	public float value = 1.0f;

	public List<Material> suitMats = new List<Material> ();
	public List<Material> tieMats = new List<Material> ();
	public List<Material> shirtMats = new List<Material> ();
	public List<Material> skinMats = new List<Material> ();

	/**
	 *  Sets the primary color as hue saturation value.
	 */
	public Vector3 hsv {
		set {
			Vector3 rgbVec = new Vector3();
			double r;
			double g;
			double b;
			Colors.HsvToRgb(value.x, value.y, value.z, out r, out g, out b);

			rgb = new Vector3((float)r, (float)g, (float)b);
		}
	}

	private Vector3 rgb {
		set {

		}
	}

	void FindMaterials() {
		foreach(var holder in materialHolders) {
			var mats = holder.GetComponent<Renderer> ().materials;

			foreach(var mat in mats) {
				print(mat.name);
				if(mat.name == (suitMatName + " (Instance)")) {
					suitMats.Add(mat);
				} else if(mat.name == (tieMatName + " (Instance)")) {
					tieMats.Add(mat);
				} else if(mat.name == (shirtMatName + " (Instance)")) {
					shirtMats.Add(mat);
				} else if(mat.name == (skinMatName + " (Instance)")) {
					skinMats.Add(mat);
				}
			}
		}
	}

	void Start() {
		FindMaterials();
	}
}
