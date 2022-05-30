using UnityEngine;
/// <summary>
/// We Are using the Mission Audio Manager as Parent Object for our AudioObject Pool
/// The Audio Manager carries the Instantiated Audio Pool Objects through Scenes
/// Initialize the Audio Player with it.
/// The Audio Manager can be Extendet with various more Tasks such as Music and other Sounds.
/// </summary>
public class AudioManager : MonoBehaviour
{
    #region Singleton
    /// <summary>
    /// Regular Singleton Instance to Access the Manager Object
    /// </summary>
    static AudioManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        //We Use DontDestroyOnLoad, also the AudioObjects will get carried
        //Into the Next Scene and dont need to be reinstantiated.
        DontDestroyOnLoad(this);

        //Initialize the AudioPlayer with Min and MaxAmount
        AudioPlayer.InitializeAudioPlayer(AudioObjectAmount, AudioObjectMaxAmount);
    }
    #endregion

    [SerializeField] int AudioObjectAmount = 20;
    [SerializeField] int AudioObjectMaxAmount = 50;

    /// <summary>
    /// Used to Set AudioObjects Parent Transform for Sorting
    /// </summary>
    /// <returns></returns>
    public static Transform ReturnTransform()
    {
        return Instance.transform;
    }
}