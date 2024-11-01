using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public float radius = 5f;  // 원의 반지름
    public float speed = 1f;    // 회전 속도

    private float angle = 0f;   // 현재 각도

    void Start()
    {
        // 초기 각도 설정: 현재 위치에 맞춰 초기화
        angle = Mathf.Atan2(transform.position.z, transform.position.x);
    }

    void Update()
    {
        // 각도를 시간에 따라 증가시킴
        angle += speed * Time.deltaTime;

        // 원형 경로를 따라 오브젝트 위치 계산
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // 새로운 위치 설정 (Y축은 일정하게 유지)
        transform.position = new Vector3(x, transform.position.y, z);

        // 이동 방향 벡터 계산
        Vector3 direction = new Vector3(Mathf.Sin(angle), 0, -Mathf.Cos(angle));

        // 방향을 바라보도록 회전
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation;
        }
    }
}
