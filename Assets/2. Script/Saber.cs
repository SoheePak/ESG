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
        //���� ������Ʈ ��ġ���� 1���� ������ ����ĳ��Ʈ�� ���� hit������ �����Ѵ�.
        if(Physics.Raycast(transform.position,transform.forward, out hit, 1, layer))
        {
            if(Vector3.Angle(transform.position-oldPos, hit.transform.up) > 140)
            {//���� ��ġ�� ���� ��ġ(oldPos)�� ���� ���̸� ����ؼ� 140�� ���� ũ��
                Destroy(hit.transform.gameObject);
                Instantiate(effectprefab, hit.point,Quaternion.LookRotation(hit.normal));
                notesSpawner.AddScore();
            }
        }
        oldPos = transform.position;
    }
}
