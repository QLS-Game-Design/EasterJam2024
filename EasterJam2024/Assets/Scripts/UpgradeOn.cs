using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOn : MonoBehaviour
{
    public static bool stop;
    public static bool unstop;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (unstop && time >= 0.75f) {
            time = 0;
            unstop = false;
        } else if (unstop) {
            time += Time.deltaTime;
        }
    }

    public void Upgrade() {
        // gameObject.SetActive(true);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        EnemyController.threshold += 1;
        stop = true;
        unstop = false;
        Debug.Log("upgrade");
    }
}
