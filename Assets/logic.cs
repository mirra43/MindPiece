using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logic : MonoBehaviour
{
    // Start is called before the first frame update

    public void OnEntered()
    {
        Debug.Log("pegou");
    }

    public void OnReleased()
    {
        Debug.Log("largou");
    }
}
