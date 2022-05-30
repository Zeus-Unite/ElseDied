using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayOneShotAudio : MonoBehaviour
{
    [SerializeField]AudioClip clip = null;

    Button btn;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate { PlayAudio(); });
    }

    private void PlayAudio()
    {
        audioSource.PlayOneShot(clip);
    }

}
