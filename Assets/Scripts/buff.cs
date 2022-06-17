using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buff : MonoBehaviour
{
    Vector3 movement;
    public float speed;
    GameObject topLine;

    void Start()
    {
        movement.y = speed;
        topLine = GameObject.Find("Top");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }
    void MovePlatform()
    {
        transform.position += -movement * Time.deltaTime;

        if (transform.position.y<= topLine.transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
