using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StusseGames.Audio
{
    public class AudioController : MonoBehaviour
    {
        #region Singleton
        static AudioController audioController;

        void Awake()
        {
            CreateAudioControllerInstance();
        }

        void CreateAudioControllerInstance()
        {
            if (audioController != null)
            {
                Destroy(this.gameObject);
                return;
            }

            audioController = this;
            DontDestroyOnLoad(this);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        #endregion

        [Header("Audio Mixer Settings")]
        [Tooltip("Drag your Audio Mixer inside here")]
        public AudioMixer AudioMixer = null;

        public static AudioMixer GetAudioMixer => audioController.AudioMixer;
        [Tooltip("Please Enter the Main Group of your Audio Mixer here, Default Master")]
        [SerializeField] string MasterGroup = "Master";

        [Header("Game Handle")]
        [Tooltip("Turn On to change the TimeScale based on Active Setting Panel to 1 for Pause not Active or 0 for Pause Active")]
        [SerializeField] bool CanPauseGame = false;

        [Header("Open Audio Key")]
        [SerializeField] KeyCode openAudioKey = KeyCode.None;

        [Header("Instantiate Prefabs")]
        [SerializeField] Transform settingsPanelPrefab = null;
        [SerializeField] AudioSlider audioSliderPrefab = null;
        [SerializeField] Button closeAudioSettingsButtonPrefab = null;

        //[Header("Debug Mode")]
        //[SerializeField] bool DebugController = false;

        Button closeAudioSettingsButton = null;
        Transform audioSettingsPanel = null;
        bool gamePaused = false;

        #region Global Access
        public static void SetActiveAudio(bool active)
        {
            //Set the Audio Settings Panel to active, Decide if active  is either true or false;
            audioController.SetActiveAudioController(active);
        }

        public static void SetActiveAudio()
        {
            //Set the Audio Settings Panel to the active state he is not in.
            audioController.SetActiveAudioController();
        }

        public static void SetVolume(string name, float volume)
        {
            //Default the Volume gets Set by the Slider value, it can be changed to different Variants such as Number Input.
            audioController.AudioMixer.SetFloat(name, volume);
        }
        public static bool IsActive()
        {
            //For Certain Reasons we wanna Check if the Audio Controller has his Settings Panel active
            return audioController.audioSettingsPanel != null ? true : false;
        }

        #endregion

        #region SceneLoad Setup
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //if(DebugController)
            //    Debug.Log("On Scene Load Audio Controller and Setup the Audio Settings Panel");

            if (AudioMixer == null)
            {
                //Create AudioMixer
                //Future Update
            }

            //if (DebugController)
            //    Debug.Log("Create Settings Panel");

            SetupSettingsPanel();

            //if (DebugController)
            //    Debug.Log("Create Audio Settings Menu from Audio Mixer Entires");

            SetupSlider();

            //if (DebugController)
            //    Debug.Log("Create Audio Panel Close Button");

            SetupCloseButton();

            //if (DebugController)
            //    Debug.Log("Deactivate Audio Panel");

            Invoke("SetActiveAudioController", .01f);
        }

        void SetupSettingsPanel()
        {
            if (audioSettingsPanel == null)
            {
                //The Canvas should contain a GameObject that is named "SettingsPanel" in order to Instantiate it at runtime.
                Transform panel = Instantiate(settingsPanelPrefab);
                audioSettingsPanel = panel.Find("SettingsPanel");
            }
        }

        void SetupSlider()
        {
            //Getting all Volume Information from the Mixer and Build our Slider and Reference Keys for Saving
            var x = AudioMixer.FindMatchingGroups(MasterGroup);

            for (int i = 0; i < x.Length; i++)
            {
                //if (DebugController)
                //    Debug.Log("Create Audio Slider for: " + item.name);

                if (audioSettingsPanel != null)
                {
                    AudioSlider audioSliderLocal = Instantiate(audioSliderPrefab, audioSettingsPanel);
                    audioSliderLocal.SetMixerTitle(x[i].name);
                }
            }
        }

        void SetupCloseButton()
        {
            //Since i dont know if you reload a Scene or something we Try just to Remove All Listeners from the Button if it Exists

            try
            {
                if (closeAudioSettingsButton != null)
                {
                    closeAudioSettingsButton.onClick.RemoveAllListeners();
                    closeAudioSettingsButton = null;
                }
            }
            finally
            {
                //We Instantiate the Close Settings Button and Add a Listener to close the Settings Panel
                closeAudioSettingsButton = Instantiate(closeAudioSettingsButtonPrefab, audioSettingsPanel);
                closeAudioSettingsButton.onClick.AddListener(delegate { SetActiveAudioController(false); });
            }
        }
        #endregion

        void Update()
        {
            //Check for Input to Open the Audio Settings Panel
            if (Input.GetKeyDown(openAudioKey))
            {
                SetActiveAudioController(!audioSettingsPanel.gameObject.activeSelf);
            }
        }

        void SetActiveAudioController(bool active)
        {
            audioSettingsPanel.gameObject.SetActive(active);
        }

        void SetActiveAudioController()
        {
            audioSettingsPanel.gameObject.SetActive(!audioSettingsPanel.gameObject.activeSelf);


        }

    }
}
