using UnityEngine;

namespace StusseGames.Audio
{
    public class AudioOpenButton : MonoBehaviour
    {
        void Awake()
        {
            audioOpenButton = GetComponent<UnityEngine.UI.Button>();
            audioOpenButton.onClick.AddListener(OpenAudioSettingsPanel);
        }

        UnityEngine.UI.Button audioOpenButton;

        void OpenAudioSettingsPanel()
        {
            if (!AudioController.IsActive()) 
                return; 
            
            AudioController.SetActiveAudio();
        }
    }
}
