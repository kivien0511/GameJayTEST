using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera : MonoBehaviour
{

    public Transform player;
    private Vector3 velocity;
    public Rigidbody2D playerRb;

    private void LateUpdate()
    {
        if (player.position.y >= transform.position.y) {
            Vector3 temp = new Vector3(transform.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, temp, ref velocity, .3f * Time.deltaTime);
        }

        if (player.position.y < transform.position.y - 3f) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
