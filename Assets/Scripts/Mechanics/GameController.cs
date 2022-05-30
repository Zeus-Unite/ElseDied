using UnityEngine;

/// <summary>
/// This class exposes the the game model in the inspector, and ticks the
/// simulation.
/// </summary> 
public class GameController : MonoBehaviour
{
    static public GameController Instance { get; private set; }

    //This model field is public and can be therefore be modified in the inspector.
    //The reference actually comes from the InstanceRegister, and is shared
    //through the simulation and events. Unity will deserialize over this
    //shared reference when the scene loads, allowing the model to be
    //conveniently configured inside the inspector.

    public PlayerModel model = Simulation.GetModel<PlayerModel>();
    public GameModel gamemodel = Simulation.GetModel<GameModel>();
    public LevelModel levelmodel = Simulation.GetModel<LevelModel>();
    public BaseModel basemodel = Simulation.GetModel<BaseModel>();
    public ShopModel shopmodel = Simulation.GetModel<ShopModel>();

    public MainUIController mainUIController;

    public bool GameRunning = false;

    void OnEnable()
    {
        Instance = this;
        mainUIController = FindObjectOfType<MainUIController>();
        model.PlayerObject.SetActive(false);
    }

    void OnDisable()
    {
        if (Instance == this) Instance = null;
    }

    void Update()
    {
        if (Instance == this) Simulation.Tick();
    }
}
