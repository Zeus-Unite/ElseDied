using System;
using UnityEngine;

/// <summary>
/// Use AudioShot inside a Script and Define the Parameters in the Inspector
/// Call the AudioShot by AudioShot.Play(Vector3?) to Play an AudioClip
/// </summary>
[Serializable]
public class AudioShot
{
    //Setup the Values in the Inspector
    public AudioClip audioClip = null; //AudioClip that gets Played
    public float audioVolume = 1; //Volume the AudioClip gets Played
    public float audioPitch = 1; //Pitch the AudioClip gets Played
    [Header("Add Start Delay If Necessary")]
    public float playDelay = .0f; //If > 0 the AudioManager will Activate the AudioClip as 3D Sound

    public void Play(Vector3? position = null) => PlayAudio(position);

    /// <summary>
    /// When PlayAudio is Called the AudioShot play's itself with the defined Settings
    /// </summary>
    /// <param name="position">Pass a Vector3 Position, Sound will be 3D on that Position</param>
    void PlayAudio(Vector3? position = null)
    {
        if (audioClip == null)
        {
            Debug.LogWarning("Audio Clip Missing");
            return;
        }

        //If we have a Delay we using our Timer Class to add the Delay before the Sound gets Executed
        if (playDelay > 0)
        {
            MyTimer.CreateTimer(() => AudioPlayer.CreateAudio(this, position), playDelay);
            return;
        }

        //Execute the Sound inside the AudioPlayer
        AudioPlayer.CreateAudio(this, position);
    }
}