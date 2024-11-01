using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Switch;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class Saber : MonoBehaviour
{
    public LayerMask layer;
    Vector3 oldPos;
    public GameObject effectprefab;
    public NotesSpawner notesSpawner;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //게임 오브젝트 위치에서 1유닛 길이의 레이캐스트를 쏴서 hit변수에 저장한다.
        if(Physics.Raycast(transform.position,transform.forward, out hit, 1, layer))
        {
            if(Vector3.Angle(transform.position-oldPos, hit.transform.up) > 140)
            {//현재 위치와 이전 위치(oldPos)의 백터 차이를 계산해서 140도 보다 크면
                Destroy(hit.transform.gameObject);
                Instantiate(effectprefab, hit.point,Quaternion.LookRotation(hit.normal));
                notesSpawner.AddScore();
            }
        }
        oldPos = transform.position;
    }
}
