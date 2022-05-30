using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace StusseGames.Audio
{
    public class AudioSlider : MonoBehaviour
    {
        void Awake()
        {
            //We get the References to the Title and Slider to change them based on Incoming Values
            mixerTitle = transform.GetComponentInChildren<TextMeshProUGUI>();
            mixerSlider = transform.GetComponentInChildren<Slider>();
        }

        TextMeshProUGUI mixerTitle;
        Slider mixerSlider;

        float s_volume;
        string volumeSave;

        void Start()
        {
            GetSliderVolume();
        }

        public void SetMixerTitle(string title)
        {
            mixerTitle.text = title + " Volume";

            //We Change the Volume Key ToUpper for the Reference Key in the Registry
            volumeSave = title;
            volumeSave.ToUpper();

            mixerSlider.onValueChanged.AddListener(_ => SetSliderVolume());

            //Initial Slider and Volume Load
            GetSliderVolume();
        }

        void GetSliderVolume()
        {
            s_volume = PlayerPrefs.GetFloat(volumeSave, 0.5f);

            if (mixerSlider != null)
            {
                mixerSlider.value = s_volume;
                SetSliderVolume();
                return;
            }

            Debug.LogWarning("No Slider Detected to Adjust Volume");
        }

        void SetSliderVolume()
        {
            s_volume = Mathf.Log10(mixerSlider.value) * 20;
            AudioController.SetVolume(volumeSave, s_volume);
            PlayerPrefs.SetFloat(volumeSave, mixerSlider.value);
            PlayerPrefs.Save();
        }
    }
}
