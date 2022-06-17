using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    public int hp = Config.playerInitHp;

    // Start is called before the first frame update
    void Start()
    {
        print(hp);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        checkOverEdge();
    }

    // private void OnCollisionEnter2D(Collision2D col) {
    //     //下落的时候才计算碰撞。不然角色只要碰到东西就会往上飞
    //     if (rb.velocity.y <= 0)
    //     {
    //         if (col.CompareTag("board"))
    //         {
    //             rb.velocity = new Vector2(0f, 10f);
    //         }
    //     }

    //     if (col.CompareTag("win"))
    //     {
    //         SceneManager.LoadScene("Win");
    //     }
    // }
       
    private void OnTriggerEnter2D(Collider2D col)
    {
        //下落的时候才计算碰撞。不然角色只要碰到东西就会往上飞
        if (rb.velocity.y <= 0)
        {
            if (col.CompareTag("board"))
            {
                rb.velocity = new Vector2(0f, 10f);
            }
        }

        if (col.CompareTag("win"))
        {
            SceneManager.LoadScene("Win");
        }
    }

    void Move() {
        float horizontalAxis = 0;
        horizontalAxis = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.rotation = Quaternion.Euler(new Vector3(0,-180,0));
        }

        rb.velocity = new Vector2(horizontalAxis * Config.playerInitMoveSpeed, rb.velocity.y);
        print(rb.velocity);
    }

    void checkOverEdge() {
        if (transform.position.x < -Config.screenWidthX) {
            transform.position = new Vector2(6f, transform.position.y);
        }
        if (transform.position.x > Config.screenWidthX) {
            transform.position = new Vector2(-6f, transform.position.y);
        }
    }

}
