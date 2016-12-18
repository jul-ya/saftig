using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorLightController : MonoBehaviour {

    public GameObject[] neonlights;
    public Light light;

    private bool blinking = true;

    void Start()
    {
        Debug.Log("light");
        
        //light.gameObject.SetActive(false);
    }

    void shortBlink()
    {
        setLight(false);
        LeanTween.delayedCall(0.1f,()=>
        {
            setLight(true);
        });
    }

    void startBlinking()
    {
        blinking = true;
    }

    private void setLight(bool state)
    {
        if (state)
        {
            light.intensity = 1;
            foreach(GameObject g in neonlights)
            {
                g.SetActive(true);
            }
            light.enabled = false;
        }
        else
        {
            light.intensity = 0;
            foreach (GameObject g in neonlights)
            {
                g.SetActive(false);
            }
            light.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (blinking)
        {
            float rand = Random.Range(0, 1);
            if (rand < 0.05)
            {
                setLight(true);
                Debug.Log("a");
            }else{
                setLight(false);
            }
        }
	}
}
