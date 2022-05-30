using System.Collections.Generic;
using UnityEngine;
using StusseGames.Audio;
/// <summary>
/// A Static Class we Create Once 
/// and is available on the entire Project
/// called somewhere with AudioPlayer.InitializeAudioPlayer(amount, maxamount)
/// </summary>
public static class AudioPlayer
{
    /// <summary>
    /// Create a AudioShot from the AudioObject Pool and pass the Position if necessary.
    /// </summary>
    /// <param name="_audioShot"></param>
    /// <param name="position">Activate the Audio Source on the defined Position.
    /// When Position is used to set the AudioSource to 3D.
    /// If Position is null the Sound gets Played as 2D Sound</param>
    public static void CreateAudio(AudioShot _audioShot, Vector3? position = null)
    {
        AudioShot audioShot = _audioShot;

        for (int i = 0; i < AudioSources.Count; i++)
        {
            if (AudioSources[i].gameObject.activeInHierarchy)
                continue;

            if (position != null)
                AudioSources[i].transform.position = (Vector3)position;

            AudioSources[i].clip = audioShot.audioClip;
            AudioSources[i].volume = audioShot.audioVolume;
            AudioSources[i].pitch = audioShot.audioPitch;
            AudioSources[i].spatialBlend = position != null ? 1 : 0;
            AudioSources[i].minDistance = 12.25f;
            AudioSources[i].SetActive(true);
            AudioSources[i].Play();

            MyTimer.CreateTimer(() => AudioSources[i].gameObject.SetActive(false), AudioSources[i].clip.length);
            return;
        }

        //Check if we still under the Max Amount of Audio Objects
        if (AudioSources.Count >= audioObjectsMaxAmount)
            return; //End the Function and dont Play any Sound


        //Create a New Audio Pool  Object and Play
        AudioSource audio = CreateAudioPoolObject(AudioSources.Count + 1);

        if (position != null)
            audio.transform.position = (Vector3)position;

        audio.clip = audioShot.audioClip;
        audio.volume = audioShot.audioVolume;
        audio.pitch = audioShot.audioPitch;

        //When we send a Position the AudioSource gets set to 3D Sound
        audio.spatialBlend = position != null ? 1 : 0;
        audio.minDistance = 12.25f;

        audio.SetActive(true);
        audio.Play();

        //A Common Use for a Timer, to turn of something after a period of Time.
        MyTimer.CreateTimer(() => audio.SetActive(false), audio.clip.length);
    }

    /// <summary>
    /// Store the  AudioSource Objects for Pool
    /// </summary>
    static List<AudioSource> AudioSources = new List<AudioSource>();

    static int audioObjectsMaxAmount = 0;

    public static void InitializeAudioPlayer(int preInstantiate, int maxAmount)
    {
        //Set Max Amount of AudioObjects
        audioObjectsMaxAmount = maxAmount;

        //Create new Min Amount of AudioObjects
        for (int i = 0; i < preInstantiate; i++)
        {
            CreateAudioPoolObject(i);
        }
    }

    /// <summary>
    /// Create Audio Pool Object and Set Parent.
    /// </summary>
    /// <param name="i">Number in Pool</param>
    /// <returns>AudioSource</returns>
    static AudioSource CreateAudioPoolObject(int i)
    {
        GameObject go = new GameObject();
        go.name = "AudioPoolObject-" + i + 1;
        go.transform.SetParent(AudioManager.ReturnTransform());
        AudioSource audio = go.AddComponent(typeof(AudioSource)) as AudioSource;
        audio.outputAudioMixerGroup = AudioController.GetAudioMixer.FindMatchingGroups("Effects")[0];
        AudioSources.Add(audio);
        go.SetActive(false);
        return audio;
    }
}