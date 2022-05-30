[System.Serializable]
public class PickUpModel 
{
    public PickUpType TypeToPickup;
    public int Amount;

    public float MaxSceneTime;

    public AudioShot Sound;
    public UnityEngine.Transform ParticleSystem;

}
public enum PickUpType
{
    Money,
    Energy,
    Score,
}