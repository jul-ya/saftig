using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAmbience : MonoBehaviour {

    public AudioClip elevatorStart;
    public AudioClip[] elevatorLoops;

	// Use this for initialization
	void Start () {
        SoundManager.SoundManagerInstance.Play(elevatorStart, Vector3.zero);
        StartCoroutine(StartLoopOne());
        StartCoroutine(StartLoopTwo());
        StartCoroutine(StartLoopThree());
        StartCoroutine(StartElevatorMusic());
    }

    IEnumerator StartLoopOne()
    {
        yield return new WaitForSeconds(39);
        SoundManager.SoundManagerInstance.Play(elevatorLoops[0], Vector3.zero, 3f, 1, true);
    }

    IEnumerator StartLoopTwo()
    {
        yield return new WaitForSeconds(40);
        SoundManager.SoundManagerInstance.Play(elevatorLoops[1], Vector3.zero, 1f, 1, true);
    }

    IEnumerator StartLoopThree()
    {
        yield return new WaitForSeconds(41);
        SoundManager.SoundManagerInstance.Play(elevatorLoops[2], Vector3.zero, 1f, 1, true);
    }

    IEnumerator StartElevatorMusic()
    {
        yield return new WaitForSeconds(10);
        SoundManager.SoundManagerInstance.Play(elevatorLoops[3], Vector3.zero, 0.25f, 1, true);
    }
}
