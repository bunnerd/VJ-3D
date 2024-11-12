using UnityEngine;

public class Jump : MonoBehaviour
{
    Gravity gravity;
    public float jumpSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gravity = GetComponent<Gravity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gravity.Grounded()) 
        {
            Debug.Log("Jump");
            gravity.speed = jumpSpeed;
        }   
    }
}
