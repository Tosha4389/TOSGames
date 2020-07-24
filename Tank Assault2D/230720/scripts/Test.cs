using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed;
    public BorderCheck borderCheck;

    void Start()
    {
        borderCheck = GetComponent<BorderCheck>(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0f, 1f, 0f);

        if(borderCheck.exitBorder == true) {
            Destroy(gameObject);
        }
    }
}
