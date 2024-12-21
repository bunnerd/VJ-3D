using UnityEngine;

public class LevelEntrance : MonoBehaviour
{
    public GameObject first;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Vector3 GetStartPosition() 
    {
        return first.transform.position + new Vector3(0.0f, 14.5f, 0.0f);
    }
}
