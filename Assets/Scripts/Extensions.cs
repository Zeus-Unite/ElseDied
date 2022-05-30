using UnityEngine;
/// ** Check https://www.stussegames.com for more Information about Extensions **
public static class Extensions
{
    public static void SetActive(this Component target, bool active)
    {
            target.gameObject.SetActive(active);
    }
}
