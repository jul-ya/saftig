﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeShakeShake : MonoBehaviour {

    [SerializeField]
    private bool shake = false;

    [SerializeField]
    private float intensity = 10.0f;

    [SerializeField]
    private float shakeTime = 1.0f;

    private float currentShake = 10.0f;

    [SerializeField]
    private bool loop = false;

 
    private Vector3 originalPosition;
    private Quaternion originalRotation;

	// Use this for initialization
	void Start () {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
	}

    void Update()
    {
        if (shake)
        {
            shake = !shake;
            Shake();
        }
    }

    public void Shake()
    {
        currentShake = shakeTime;
        LeanTween.value(gameObject, intensity, 0.0f, shakeTime)
          .setOnUpdate((float amount) =>
          {
              currentShake = amount;
              transform.position = originalPosition + Random.onUnitSphere * intensity;
              transform.rotation.Set(
                   originalRotation.x + Random.Range(-currentShake, currentShake) * 0.2f,
                   originalRotation.y + Random.Range(-currentShake, currentShake) * 0.2f,
                   originalRotation.z + Random.Range(-currentShake, currentShake) * 0.2f,
                   originalRotation.w + Random.Range(-currentShake, currentShake) * 0.2f);
          })
          .setEase(LeanTweenType.easeOutSine)
          .setOnComplete(()=> {
              if (loop)
              {
                  intensity += Random.Range(-intensity / 2.0f, intensity / 2.0f);
                  shakeTime += Random.Range(0.0f, 8.0f);
                  Shake();
              }
          });
    }
}
