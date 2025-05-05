using Meta.XR.MRUtilityKit.SceneDecorator;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentBox : MonoBehaviour
{
    GameObject objectInside;
    bool isInside = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInside && objectInside != null)
        {
            objectInside.transform.position = this.transform.position;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("item"))
        {
            objectInside = other.gameObject;
            isInside = true;
            if (other != null)
            {
                //other.gameObject.GetComponent<Grabbable>().enabled = false;
                other.gameObject.transform.position = this.transform.position;
                other.transform.parent = this.transform;
            }
        }
        
        
    }
}
