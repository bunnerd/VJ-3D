using UnityEngine;

public class TargetFramerate : MonoBehaviour
{
    void Start()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        Application.targetFrameRate = 60;
#else
        Application.targetFrameRate = 30;
#endif
    }
}
