using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRCircle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectToClone;
    public string targetTag;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);

        if (targetObject != null)
        {
            GameObject clone = Instantiate(PRCircle);
            clone.transform.position = targetObject.transform.position;
        }
        else
        {
            Debug.LogError("No object found with tag: " + targetTag);
        }
    }
}
