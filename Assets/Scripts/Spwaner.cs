using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwaner : MonoBehaviour
{
    public List<GameObject> platforms = new List<GameObject>();

    public float spwanTime;//生成时间
    private float countTime;//生成计数
    private Vector3 spwanPosition;//出生范围
    void Start()
    {
       
    }

    
    void Update()
    {
        SpwanPlatform();
    }

    public void SpwanPlatform()
    {
        countTime += Time.deltaTime;
        spwanPosition = transform.position;
        spwanPosition.x = Random.Range(-7, 7);
        if (countTime>= spwanTime)
        {
            CreatePlatform();
            countTime = 0;
        }
    }

    //平台生成方法
    public void CreatePlatform()
    {
        int index = Random.Range(0, platforms.Count);

        Instantiate(platforms[index], spwanPosition, Quaternion.identity);
    }


}
