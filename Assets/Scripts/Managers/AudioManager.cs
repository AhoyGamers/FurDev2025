using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static FMOD.Studio.EventInstance eventInstance { get; set; }

    /// <summary>
    /// Starts playing a background music (BGM) track based on the specified song name.
    /// </summary>
    /// <param name="songName">The name of the song to be played.</param>
    public static void StartBGM(string songName)
    {
        eventInstance = LoadBGM(songName);
        eventInstance.start();
    }

    /// <summary>
    /// Loads a background music (BGM) event instance for the specified song name.
    /// </summary>
    /// <param name="songName">The name of the song to load.</param>
    /// <returns>A new instance of FMOD.Studio.EventInstance for the specified BGM.</returns>
    public static FMOD.Studio.EventInstance LoadBGM(string songName)
    {
        string eventPath = $"event:/BGM/{songName}";
        return FMODUnity.RuntimeManager.CreateInstance(eventPath);
    }

    /// <summary>
    /// Stops the currently playing background music (BGM).
    /// </summary>
    public static void StopBGM()
    {
        if (eventInstance.isValid())
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.release();
        }
    }

    /// <summary>
    /// Plays a sound effect (SFX) based on the specified sound name.
    /// </summary>
    /// <param name="soundName">The name of the sound effect to be played.</param>
    public static void PlaySFX(string soundName)
    {
        string eventPath = $"event:/SFX/{soundName}";
        FMODUnity.RuntimeManager.PlayOneShot(eventPath);
    }
}
