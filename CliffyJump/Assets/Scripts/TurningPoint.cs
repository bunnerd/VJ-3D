using UnityEngine;

public class TurnPoint : MonoBehaviour
{
    public Turn[] turns;
    public int currentTurn = 0;

    public Vector3 centerPos;

    public enum Turn 
    {
        Left,
        Right,
        None
    }

    public void Restart() 
    {
        currentTurn = 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTurn = 0;
        centerPos = transform.position + new Vector3(0, transform.lossyScale.y / 2, 0);
    }
}
