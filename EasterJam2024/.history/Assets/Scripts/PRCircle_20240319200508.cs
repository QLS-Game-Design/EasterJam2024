using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRCircle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Circle;
    public string targetTag;

    private GameObject player;
    private PlayerController playerController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);

        if (targetObject != null)
        {
            GameObject clone = Instantiate(Circle);
            clone.transform.position = targetObject.transform.position;
        }
        else
        {
            Debug.LogError("No object found with tag: " + targetTag);
        }
    }
}
