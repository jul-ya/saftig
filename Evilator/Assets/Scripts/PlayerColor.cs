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

	public Vector3 suitHsv {
		set {
			Color rgb = toRgb(value);
			foreach(var mat in suitMats) {
				mat.SetColor("_Color", rgb);
			}
		}
	}

	public Vector3 tieHsv {
		set {
			Color rgb = toRgb(value);
			foreach(var mat in tieMats) {
				mat.SetColor("_Color", rgb);
			}
		}
	}

	public Vector3 shirtHsv {
		set {
			Color rgb = toRgb(value);
			foreach(var mat in shirtMats) {
				mat.SetColor("_Color", rgb);
			}
		}
	}

	public Vector3 skinHsv {
		set {
			Color rgb = toRgb(value);
			foreach(var mat in skinMats) {
				mat.SetColor("_Color", rgb);
			}
		}
	}

	private Color toRgb(Vector3 hsv) {
		double r;
		double g;
		double b;
		Colors.HsvToRgb(hsv.x, hsv.y, hsv.z, out r, out g, out b);

		return new Color((float)r, (float)g, (float)b);
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
