using UnityEngine;

public class StopTime : MonoBehaviour
{
    public GameObject player;
    public void ToggleStoppedTime()
    {
        player.GetComponent<Jump>().fullStopped = !player.GetComponent<Jump>().fullStopped;
        Time.timeScale = 1.0f - Time.timeScale;
    }
}
