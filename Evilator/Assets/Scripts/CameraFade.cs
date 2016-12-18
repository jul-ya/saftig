using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFade : MonoBehaviour {


    private int depth = -1000;

    [SerializeField]
    private float fadeDuration = 0.3f;

    [SerializeField]
    private Texture2D fadeTexture;

    private int fadeDirection = -1;
    private float alphaValue = 0.0f;

   
    void OnGUI()
    {
        GUI.color = new Color(0,0,0, alphaValue);
        GUI.depth = depth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
    }


    public void FadeOutIn()
    {
        LeanTween.value(gameObject, 0.0f, 1.0f, fadeDuration)
          .setOnUpdate((float amount) =>
          {
              alphaValue = amount;
          })
          .setEase(LeanTweenType.easeOutSine)
          .setOnComplete(() => { FadeIn(); });
    }

    private void FadeIn()
    {
        LeanTween.value(gameObject, 1.6f, 0.0f, fadeDuration+0.2f)
        .setOnUpdate((float amount) =>
        {
            alphaValue = amount;
        })
        .setEase(LeanTweenType.easeInSine);
    }
}
