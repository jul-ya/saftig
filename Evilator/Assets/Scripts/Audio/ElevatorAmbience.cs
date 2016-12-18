using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAmbience : MonoBehaviour {

    public AudioClip elevatorStart;
    public AudioClip[] elevatorLoops;

    private AudioSource loopOne;
    private AudioSource loopTwo;
    private AudioSource loopThree;
    private AudioSource music;

	// Use this for initialization
	public void StartElevatorAmbience () {
        SoundManager.SoundManagerInstance.Play(elevatorStart, Vector3.zero);
        StartCoroutine(StartLoopOne());
        StartCoroutine(StartLoopTwo());
        StartCoroutine(StartLoopThree());
    }

    public void StartMusic()
    {
        StartCoroutine(StartElevatorMusic());
    }

    IEnumerator StartLoopOne()
    {
        yield return new WaitForSeconds(39);
        loopOne =  SoundManager.SoundManagerInstance.Play(elevatorLoops[0], Vector3.zero, 3f, 1, true);
    }

    IEnumerator StartLoopTwo()
    {
        yield return new WaitForSeconds(40);
        loopTwo = SoundManager.SoundManagerInstance.Play(elevatorLoops[1], Vector3.zero, 1f, 1, true);
    }

    IEnumerator StartLoopThree()
    {
        yield return new WaitForSeconds(41);
        loopThree = SoundManager.SoundManagerInstance.Play(elevatorLoops[2], Vector3.zero, 1f, 1, true);
    }

    IEnumerator StartElevatorMusic()
    {
        yield return new WaitForSeconds(1.5f);
        music = SoundManager.SoundManagerInstance.Play(elevatorLoops[3], Vector3.zero, 0.15f, 1, true);
    }

    public void DisableAllSounds()
    {
        StopAllCoroutines();
        if (loopOne != null)
        {
            loopOne.Stop();
        }

        if(loopTwo != null)
        {
            loopTwo.Stop();
        }
        if (loopThree != null)
        {
            loopThree.Stop();
        }
        if (music != null)
        {
            music.Stop();
        }
    }
}
