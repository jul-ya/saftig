using UnityEngine;
using System.Collections;

/// <summary>
/// This script plays randomly different audio clips with the given audiosource.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MultipleAudioclips : MonoBehaviour 
{
    // Category name.
    [Header("Category name: ")]
    [Tooltip("The name of the audio category.")]
    [SerializeField]
    protected string audioCategory;

    // Audioclips
    [Header("Audio clips: ")]
    [Tooltip("The different audioclips.")]
    [SerializeField]
    protected AudioClip[] clips;

    // Reference to the audiosouce.
    protected AudioSource aSource;

    /// <summary>
    /// Gets the audio category.
    /// </summary>
    public string AudioCategory
    {
        get { return this.audioCategory; }
    }

    void Awake()
    {
        // Set a reference to the audiosource.
        aSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays one of the random clips.
    /// </summary>
    /// <returns>True: Playing is possible, False: Playing of the sound was not possible.</returns>
    public bool PlayRandomClip()
    {
        // Return false and play nothing if there are no clips.
        if (clips.Length == 0)
            return false;
        else if (clips.Length == 1)
            return SetNewAudioSource(clips[0]);
        else if (clips.Length > 1)
            return SetNewAudioSource(clips[Random.Range(0, clips.Length)]);

        return false;
    }

    /// <summary>
    /// Initializes the audiosource and plays it if possible.
    /// </summary>
    /// <param name="clip"></param>
    private bool SetNewAudioSource(AudioClip clip)
    {
        //Debug.Log(aSource);
        // Only initialize the AudioSource if it is not null and if it is not playing an other sound.
        if (aSource != null && !aSource.isPlaying)
        {
            // Set new clip and play it.
            aSource.clip = clip;
            aSource.Play();
            return true;
        }
        else 
            return false;
    }
}