using UnityEngine;

public class Jump : MonoBehaviour
{
    Gravity gravity;
    PlayerMove playerMove;
    public float jumpSpeed;

    public AudioSource sound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gravity = GetComponent<Gravity>();
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gravity.Grounded()) 
        {
            DoJump();
        }   
    }

    private void DoJump() 
    {
        if (playerMove.stopped)
        {
            // Move again
            playerMove.stopped = false;
            GetComponent<Animator>().SetTrigger("move");
        }
        else 
        {
            // Jump
			Debug.Log("Jump");
			gravity.speed = jumpSpeed;
            sound.Play();
            GetComponent<Animator>().SetTrigger("jump");
            transform.Find("JumpParticles").gameObject.GetComponent<ParticleSystem>().Play();
        }
	}
}
