using UnityEngine;
using UnityEngine.SceneManagement;


// Literally a script used to reset the scene when the key "R" is pressed

public class ResetScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
