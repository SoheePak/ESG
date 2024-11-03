using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Video;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] cars; //차들을 넣어놓을 프리팹
    public Transform[] pos; // 차들를 생성할 위치 배열
    private float timer;
    private float spawntime = 2f;
    private float rotationvalue = 90f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawntime)
        {
            timer = 0;
            spawntime = Random.Range(0.5f, 1.5f);
            CarSpawn();
        }
    }

    public void CarSpawn()
    {
        int i =Random.Range(0, pos.Length); //랜덤 위치
        if (i % 2 == 1)
        {
            rotationvalue *= -1;
        }
        Quaternion rotation = Quaternion.Euler(0, rotationvalue, 0);
        GameObject car = Instantiate(cars[Random.Range(0, cars.Length)], pos[i].position,rotation);
    }

}
