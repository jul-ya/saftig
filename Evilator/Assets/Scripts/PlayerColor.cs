using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour {

	public Transform[] materialHolders;

	public string suitMatName = "Suite_mat";
	public string tieMatName = "Tie_mat";
	public string shirtMatName = "Shirt_mat";
	public string skinMatName = "Skin_mat";

	public List<Material> suitMats = new List<Material> ();
	public List<Material> tieMats = new List<Material> ();
	public List<Material> shirtMats = new List<Material> ();
	public List<Material> skinMats = new List<Material> ();

	public Vector3 suitHsv {
		set {
			SetMaterialsColor(tieMats, value);
		}
	}

	public Vector3 tieHsv {
		set {
			SetMaterialsColor(tieMats, value);
		}
	}

	public Vector3 shirtHsv {
		set {
			SetMaterialsColor(shirtMats, value);
		}
	}

	public Vector3 skinHsv {
		set {
			SetMaterialsColor(skinMats, value);
		}
	}

	private void SetMaterialsColor(List<Material> mats, Vector3 hsv) {
		if(mats.Count == 0) {
			FindMaterials();
		}

		Color rgb = toRgb(hsv);
		Debug.Log(rgb);
		foreach(var mat in mats) {
			mat.SetColor("_Color", rgb);
			mat.SetColor("_EmissionColor", rgb);
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
}
