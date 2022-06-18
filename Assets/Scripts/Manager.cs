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

    public List<GameObject> specialPlatformPrefabList;

    private int flag = 1; // 初始为正

    private void Start()
    {
        Vector3 spawnPosition = new Vector3();
        specialPlatformPrefabList = new List<GameObject>();
        for (int i = 0; i < numberOfPlatforms; i++)
        {
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
}
