using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject cardUI;
    public List<Card> cardList;
    private int cardListLength;
    private int cardIndex = -1;

    public int hp = Config.playerInitHp;

    // Start is called before the first frame update
    void Start()
    {
        cardList = new List<Card>();
        cardListLength = Config.cardListLength;
        print(hp);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckOverEdge();
        SelectCard();
    }
       
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
    }

    void CheckOverEdge() {
        if (transform.position.x < -Config.screenWidthX) {
            transform.position = new Vector2(6f, transform.position.y);
        }
        if (transform.position.x > Config.screenWidthX) {
            transform.position = new Vector2(-6f, transform.position.y);
        }
    }

    void SelectCard() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (cardIndex == -1) {
                print("not select card");
                return;
            }
            cardIndex = -1;
            print("use card");
        }

 

        if (Input.GetKeyDown(KeyCode.Q)) {
            print("select card 1");
            cardIndex = 0;
            for (int i = 0; i < cardListLength; i ++) {
                cardUI.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
            }
            cardUI.transform.GetChild(cardIndex).gameObject.GetComponent<Image>().color = Color.red;
        } else if (Input.GetKeyDown(KeyCode.W)) {
            print("select card 2");
            cardIndex = 1;
            for (int i = 0; i < cardListLength; i ++) {
                cardUI.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
            }
             cardUI.transform.GetChild(cardIndex).gameObject.GetComponent<Image>().color = Color.red;
        } else if (Input.GetKeyDown(KeyCode.E)) {
            print("select card 3");
            for (int i = 0; i < cardListLength; i ++) {
                cardUI.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
            }
            cardIndex = 2;
            cardUI.transform.GetChild(cardIndex).gameObject.GetComponent<Image>().color = Color.red;
        }

        if (cardIndex != -1) {
            if (cardList.Count <= cardIndex) {
                return;
            }
            Card targetCard = cardList[cardIndex];
            if (targetCard == null) {
                print("not targetCard");
                cardIndex = -1;
                return;
            }
        }

    }

}

public class Card {

    public int cardType;
    public int cardEffectType;

    public Card() {

    }

    void Init() {

    }

    void InitTest() {
        cardType = (int)Config.CardType.CardType1;
        cardEffectType = (int)Config.CardEffectType.effectType1;
    }
}