using UnityEngine;
using UnityEngine.UI;

public class PlayOneShotAudio : MonoBehaviour
{
    [SerializeField] AudioShot playOneShot;

    Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate { playOneShot.Play(); });
    }
}
