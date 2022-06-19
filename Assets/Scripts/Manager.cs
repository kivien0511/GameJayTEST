using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manager : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject specialPlatformPrefab1;
    public GameObject specialPlatformPrefab2;
    public int numberOfPlatforms;
    public GameObject winPointPrefab;
    public float levelWidth = 3f;
    public float minY = .2f;
    public float maxY = 1.8f;
    private float lastx;

    public int itemSpliceCount = 5;
    public int itemSpliceCountOffset = 2;
    public GameObject changeItem;

    public int cardSpliceCount = 4;
    public int cardSpliceCountOffset = 4;
    public List<GameObject> cardItemList;

    public GameObject cardUI;

    public List<GameObject> specialPlatformPrefabList;

    private int flag = 1; // 初始为正

    private void Start()
    {
        Vector3 spawnPosition = new Vector3();
        specialPlatformPrefabList = new List<GameObject>();
        int itemCountIndex = 0;
        int cardCountIndex = 0;
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            itemCountIndex += 1;
            int itemOffsetNumber = UnityEngine.Random.Range(1,10); 
            if (itemOffsetNumber <= itemSpliceCountOffset && itemCountIndex == itemSpliceCount) {
                // 生成道具
                InitChangeItem(spawnPosition);
            }
            if (itemCountIndex >= itemSpliceCount) {
                itemCountIndex = 0;
            }

            cardCountIndex += 1;
            int cardOffsetNumber = UnityEngine.Random.Range(1,10); 
            if (cardOffsetNumber <= cardSpliceCountOffset && cardCountIndex == cardSpliceCount) {
                // 生成卡牌
                InitCardItem(spawnPosition);
            }
            if (cardCountIndex >= cardSpliceCount) {
                cardCountIndex = 0;
            }

            spawnPosition.y += UnityEngine.Random.Range(minY, maxY);
            float tmpx = UnityEngine.Random.Range(-levelWidth, levelWidth);
            while (Math.Abs(tmpx - lastx) > 4f)
            {
                tmpx = UnityEngine.Random.Range(-levelWidth, levelWidth);
            }

            lastx = tmpx;
            spawnPosition.x = tmpx;

            if (i == numberOfPlatforms - 1)
            {
                //生成win point
                Instantiate(winPointPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                int rand = UnityEngine.Random.Range(1, 11);
                if (rand < 6) {
                    Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                } else if (rand > 6 && rand <= 8) {
                    GameObject obj = Instantiate(specialPlatformPrefab1, spawnPosition, Quaternion.identity);
                    specialPlatformPrefabList.Add(obj);
                } else if (rand > 8 && rand <= 10) {
                    GameObject obj = Instantiate(specialPlatformPrefab2, spawnPosition, Quaternion.identity);
                    specialPlatformPrefabList.Add(obj);
                }
            }
        }
        for (int i = 0; i < cardUI.transform.childCount; i++)
        {
            specialPlatformPrefabList.Add(cardUI.transform.GetChild(i).gameObject);
        }
    }

    private void Update() {
        if (flag != Player.flag) {
            // 刷新特殊地板正反

            if (Player.flag == 1) {
                foreach (var x in specialPlatformPrefabList) {
                    if (x == null) continue;
                    x.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
                }
            }
            if (Player.flag == -1) {
                foreach (var x in specialPlatformPrefabList) {
                    if (x == null) continue;
                    x.transform.rotation = Quaternion.Euler(new Vector3(0,0,-180));
                }
            }

            flag = Player.flag;
        }
    }

    void InitChangeItem(Vector3 spawnPosition)
    {
        float posX = 2f;
        float posY = 3f;
        float randPosX = UnityEngine.Random.Range(
            spawnPosition.x - posX,
            spawnPosition.x + posX
        );
        float randPosY = UnityEngine.Random.Range(
            spawnPosition.y + posY,
            spawnPosition.y + posY * 2
        );
        Vector3 changeItemInitPosotion = new Vector3(randPosX, randPosY);
        Instantiate(this.changeItem, changeItemInitPosotion, Quaternion.identity);
    }

    void InitCardItem(Vector3 spawnPosition)
    {
        float posX = 2f;
        float posY = 3f;
        float randPosX = UnityEngine.Random.Range(
            spawnPosition.x - posX,
            spawnPosition.x + posX
        );
        float randPosY = UnityEngine.Random.Range(
            spawnPosition.y + posY,
            spawnPosition.y + posY * 2
        );
        int cardItemIndex = UnityEngine.Random.Range(0, cardItemList.Count);
        Vector3 cardItemInitPosotion = new Vector3(randPosX, randPosY);
        GameObject initObject = new GameObject();
        initObject = this.cardItemList[cardItemIndex];
        int cardFlag = UnityEngine.Random.Range(0, 10);
        if (cardFlag > 5)
        {
            GameObject obj = Instantiate(initObject, cardItemInitPosotion, Quaternion.identity);
            this.specialPlatformPrefabList.Add(obj);
        }
        else
        {
            GameObject obj = Instantiate(
                initObject,
                cardItemInitPosotion,
                Quaternion.Euler(new Vector3(0, 0, -180))
            );
            this.specialPlatformPrefabList.Add(obj);
        }
    }
}
