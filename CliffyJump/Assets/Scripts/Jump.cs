using UnityEngine;
using UnityEngine.EventSystems;

public class Jump : MonoBehaviour
{
    Gravity gravity;
    PlayerMove playerMove;
    public float jumpSpeed;
    public bool fullStopped;

    public AudioSource jumpSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fullStopped = false;
        gravity = GetComponent<Gravity>();
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        // mouse button 0 -> left button
        // screen touches in mobile count as mouse clicks
        if ( (Input.GetKeyDown(KeyCode.Space) || 
             (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())) // Don't jump when clicking UI
            && gravity.Grounded() && !fullStopped)
        {
            DoJump(playerMove.godmode);
        }
    }

    public void DoJump(bool godmode = false) 
    {
        if (playerMove.stopped)
        {
			// Move again
			playerMove.stopped = false;
            GetComponent<Animator>().SetTrigger("move");
        }
        else 
        {
            if (godmode)
                return;

            // Jump
			gravity.speed = jumpSpeed;
            jumpSound.Play();
            GetComponent<Animator>().SetTrigger("jump");
            transform.Find("JumpParticles").gameObject.GetComponent<ParticleSystem>().Play();
        }
	}
}
