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
    public GameObject hpUI;
    public GameObject hpUIimage;
    public List<Card> cardList;
    private int cardListLength;
    private int cardIndex = -1;

    private bool boardSpeedFlag = false;
    private int boardSpeed = 0;

    private float timer = 0f;

    public static int flag = 1; // 正

    public int hp = Config.playerInitHp;
    public int hpMax = Config.playerInitHp;

    // Start is called before the first frame update
    void Start()
    {
        DrawHpUI();
        cardList = new List<Card>();
        cardListLength = Config.cardListLength;
        print(hp);
    }

    // Update is called once per frame
    void Update()
    {
        if (boardSpeedFlag) {
            timer += Time.deltaTime;
            if (timer >= 3f) {
                timer = 0f;
                boardSpeedFlag = false;
            }
        }

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

                if (col.gameObject.name.Contains("1")) {
                    // 特殊地板1
                    // print("特殊地板1");
                    // 正 - 1 ， 负 + 1
                    if (flag > 0) {
                        if (hp - 1 > 0) {
                            hp -= 1;
                        } else if (hp - 1 == 0) {
                            print("dead");
                        }
                    } else if (flag < 0) {
                        if (hp + 1 >= hpMax) {
                            hp = hpMax;
                        } else if (hp + 1 < hpMax) {
                            hp += 1;
                        }
                    }
                    DrawHpUI();
                    // print("hp:"+hp);
                } else if (col.gameObject.name.Contains("2")) {
                    // 特殊地板2
                    // print("特殊地板2");
                    // 正 + 移速 ， 负 - 移速
                    boardSpeed = 0;
                    boardSpeedFlag = true;
                } 

                Destroy(col.gameObject);
            }
        }

        if (col.CompareTag("win"))
        {
            SceneManager.LoadScene("Win");
        }
    }

    void Move() {
        // print(flag);
        if (Input.GetKeyDown(KeyCode.R)) { 
            flag = 1;
        } 
        if (Input.GetKeyDown(KeyCode.F)) { 
            flag = -1;
        }

        float horizontalAxis = 0;
        horizontalAxis = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.rotation = Quaternion.Euler(new Vector3(0,-180,0));
        }

        if (flag == 1) { // 正
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }

        if (flag == -1) { // 反
            transform.rotation = Quaternion.Euler(new Vector3(0,0,-180));
        }

        if (boardSpeed == 0) {
            boardSpeed = 3; // 根据卡牌计算

            boardSpeed *= flag;
        }

        if (boardSpeedFlag) {
            print("boardSpeed:"+boardSpeed);
            rb.velocity = new Vector2(horizontalAxis * (Config.playerInitMoveSpeed + boardSpeed), rb.velocity.y);
            return;
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

    void DrawHpUI() {
        for (int i = 0; i < hpUI.transform.childCount; i ++) {
            Destroy(hpUI.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < hp; i ++) {
            GameObject obj = Instantiate(hpUIimage, Vector3.zero, Quaternion.identity);
            obj.transform.position = hpUI.transform.position + new Vector3(i * 100, 0 ,0);
            obj.transform.SetParent(hpUI.transform);
        }
    }

    void FallSpeedUp() {
        if (rb.velocity.y < 0) {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                
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