using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainUIController : MonoBehaviour
{
    [SerializeField] Transform UIPanel = null;
    [SerializeField] Transform EndWavePanel = null;
    [SerializeField] Transform InfoPanel = null;

    [SerializeField] Transform EndGamePanel = null;
    [SerializeField] TextMeshProUGUI EndGameText = null;
    [SerializeField] Transform DeathBeginningButton = null;
    [SerializeField] Transform StartOverButton = null;


    void Start()
    {
        EndWavePanel.SetActive(false);
        EndGamePanel.SetActive(false);
    }

    public void StartGame()
    {
        EventSystem.current.SetSelectedGameObject(null);

        var NewStart = Simulation.Schedule<StartLevel>();
        NewStart.IsNewStart = true;

        UIPanel.SetActive(false);
        InfoPanel.SetActive(false);
        EndWavePanel.SetActive(false);
        EndGamePanel.SetActive(false);
    }


    public void EndGame(bool playerAlive)
    {
        UIPanel.SetActive(false);
        EndWavePanel.SetActive(false);
        EndGamePanel.SetActive(true);

        if (playerAlive)
        {
            EndGameText.text = "You can Continue Playing and the Enemies become Stronger!\nThe Death of them was just the beginning!";
            DeathBeginningButton.SetActive(true);
            return;
        }

        EndGameText.text = "You Lost to the Hordes of glowing Zombies!\nYou can Start Over again!!";
        DeathBeginningButton.SetActive(false);
    }


    public void StartWave()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EndWavePanel.SetActive(false);
        Simulation.Schedule<StartWave>();
    }

    public void EndWave()
    {
        EndWavePanel.SetActive(true);
    }

    public void StartDeathBeginning()
    {
        EventSystem.current.SetSelectedGameObject(null);

        var NewStart = Simulation.Schedule<StartLevel>();
        NewStart.IsNewStart = false;
        
        UIPanel.SetActive(false);
        EndGamePanel.SetActive(false);
    }

    public void StartOverAgain()
    {
        UIPanel.SetActive(true);
        InfoPanel.SetActive(true);
        EndGamePanel.SetActive(false);
    }

}
