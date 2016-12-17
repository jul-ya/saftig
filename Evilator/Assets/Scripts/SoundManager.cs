using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

/// <summary>
/// This manager script handles the generating and pooling of AudioSources.
/// This script uses Singleton.
/// </summary>
public class SoundManager : MonoBehaviour
{
    //Reference to the sound manager instance
    private static SoundManager soundManagerInstance;
    private static AudioMixer audioMixer;

    private List<AudioSource> audioSources;

    public AudioMixer SoundManagerMainMixer
    {
        get { return SoundManager.audioMixer; }
    }

    /// <summary>
    /// Gets the sound manager instance
    /// </summary>
    public static SoundManager SoundManagerInstance
    {
        get
        {
            soundManagerInstance = GameObject.FindObjectOfType<SoundManager>();

            //If the instance isn't set yet, it will be set (Happens only the first time!)
            if (soundManagerInstance == null)
            {
                soundManagerInstance = new GameObject("_SoundManager").AddComponent<SoundManager>();
                audioMixer = Resources.Load<AudioMixer>("MixerGroups/MainMixer");
                DontDestroyOnLoad(soundManagerInstance);
            }

            return soundManagerInstance;
        }
    }

    void Awake()
    {
        if(audioMixer == null)
            audioMixer = Resources.Load<AudioMixer>("MixerGroups/MainMixer");
        audioSources = new List<AudioSource>();
    }

    /// <summary>
    /// Adds a new Audiosource the SoundManager and sets it as a child object of the emitter. 
    /// When the sound isn't looping, the Audiosource will be destroyd after it is played.
    /// </summary>
    /// <param name="clip">Audioclip which should be played.</param>
    /// <param name="emitter">Parent object of the AudioSource.</param>
    /// <param name="volume">Volume of the clip.</param>
    /// <param name="pitch">Pitch of the clip.</param>
    /// <param name="loop">Specifies if the clip should be looped or not.</param>
    public AudioSource Play(AudioClip clip, Transform emitter, float volume, float pitch, bool loop)
    {
        //Instance a new GameObject
        GameObject g = new GameObject("AudioSource: " + clip.name);
        g.transform.position = emitter.transform.position;
        g.transform.parent = emitter;

        //AudioSource
        AudioSource source;

        source = g.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.Play();

        audioSources.Add(source);

        if (!loop)
        {
            //Destroy(g, clip.length);
            StartCoroutine(WaitForDestroy(g, clip.length));
            audioSources.Remove(source);
        }

        return source;
    }

    /// <summary>
    /// Adds a new Audiosource the SoundManager and sets it as a child object of the emitter.
    /// This soundclip does not loop.
    /// </summary>
    /// <param name="clip">Audioclip which should be played.</param>
    /// <param name="emitter">Parent object of the AudioSource.</param>
    /// <param name="volume">Volume of the clip.</param>
    /// <param name="pitch">Pitch of the clip.</param>
    public AudioSource Play(AudioClip clip, Transform emitter, float volume, float pitch)
    {
        return Play(clip, emitter, volume, pitch, false);
    }

    /// <summary>
    /// Adds a new Audiosource the SoundManager and sets it as a child object of the emitter.
    /// This soundclip does not loop.
    /// </summary>
    /// <param name="clip">Audioclip which should be played.</param>
    /// <param name="emitter">Parent object of the AudioSource.</param>
    public AudioSource Play(AudioClip clip, Transform emitter)
    {
        return Play(clip, emitter, 1f, 1f, false);
    }

    /// <summary>
    /// Adds a new Audiosource the SoundManager and sets it to the specified position.
    /// When the sound isn't looping, the Audiosource will be destroyd after it is played.
    /// </summary>
    /// <param name="clip">Audioclip which should be played.</param>
    /// <param name="position">Position of the AudioSource</param>
    /// <param name="volume">Volume of the clip.</param>
    /// <param name="pitch">Pitch of the clip.</param>
    /// <param name="loop">Specifies if the clip should be looped or not.</param>
    public AudioSource Play(AudioClip clip, Vector3 position, float volume, float pitch, bool loop)
    {
        //Instance a new GameObject
        GameObject g = new GameObject("AudioSource: " + clip.name);
        g.transform.position = position;

        //AudioSource
        AudioSource source;

        source = g.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.Play();

        audioSources.Add(source);

        if (!loop)
        {
            //Destroy(g, clip.length);
            StartCoroutine(WaitForDestroy(g, clip.length));
            audioSources.Remove(source);
        }

        return source;
    }

    /// <summary>
    /// Adds a new Audiosource the SoundManager and sets it to the specified position.
    /// This sound does not loop.
    /// </summary>
    /// <param name="clip">Audioclip which should be played.</param>
    /// <param name="position">Position of the AudioSource</param>
    /// <param name="volume">Volume of the clip.</param>
    /// <param name="pitch">Pitch of the clip.</param>
    public AudioSource Play(AudioClip clip, Vector3 position, float volume, float pitch)
    {
        return Play(clip, position, volume, pitch, false);
    }

    /// <summary>
    /// Adds a new Audiosource the SoundManager and sets it to the specified position.
    /// This sound does not loop.
    /// </summary>
    /// <param name="clip">Audioclip which should be played.</param>
    /// <param name="position">Position of the AudioSource</param>
    public AudioSource Play(AudioClip clip, Vector3 position)
    {
        return Play(clip, position, 1f, 1f, false);
    }


    /// <summary>
    /// Removes the given AudioSource if the source was found.
    /// </summary>
    /// <param name="source">Audio Source to remove</param>
    /// <returns>True if the Audiosource was found and removed.</returns>
    public bool RemoveAudioSource(AudioSource source)
    {
        bool contains = audioSources.Remove(source);

        if (contains)
        {
            source.Stop();
            Destroy(source);
        }

        return contains;
    }

    /// <summary>
    /// Removes all running audio sources.
    /// </summary>
    public void RemoveAllAudioSources()
    {
        foreach (AudioSource a in audioSources)
        {
            audioSources.Remove(a);
            a.Stop();
            Destroy(a);
        }
    }

    /// <summary>
    /// Waits before the given object will be destroyed.
    /// </summary>
    /// <param name="g"></param>
    /// <param name="clip"></param>
    /// <returns></returns>
    protected IEnumerator WaitForDestroy(GameObject g, float waitTime)
    {
        yield return new WaitForSeconds(waitTime * 2f);
        Destroy(g);
    }
}