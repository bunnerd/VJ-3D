using System.Collections;
using UnityEngine;

public abstract class Decoration : MonoBehaviour
{
    public abstract void Init();

    public abstract IEnumerator Load();

    public abstract IEnumerator Unload();
}
