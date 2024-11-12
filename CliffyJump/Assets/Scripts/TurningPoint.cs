using UnityEngine;

public class TurnPoint : MonoBehaviour
{
    public PlayerMove player;
    public Turn turn;

    public enum Turn 
    {
        Left,
        Right
    }

    private bool touchedPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        touchedPlayer = false;
    }

	private void OnTriggerEnter(Collider other)
	{
        if (!touchedPlayer && other.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Player hit!");
            touchedPlayer = true;

            if (turn == Turn.Left)
                player.TurnLeft();
            else if (turn == Turn.Right)
                player.TurnRight();
        }
	}
}
