using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public float radius = 5f;  // ���� ������
    public float speed = 1f;    // ȸ�� �ӵ�

    private float angle = 0f;   // ���� ����

    void Start()
    {
        // �ʱ� ���� ����: ���� ��ġ�� ���� �ʱ�ȭ
        angle = Mathf.Atan2(transform.position.z, transform.position.x);
    }

    void Update()
    {
        // ������ �ð��� ���� ������Ŵ
        angle += speed * Time.deltaTime;

        // ���� ��θ� ���� ������Ʈ ��ġ ���
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // ���ο� ��ġ ���� (Y���� �����ϰ� ����)
        transform.position = new Vector3(x, transform.position.y, z);

        // �̵� ���� ���� ���
        Vector3 direction = new Vector3(Mathf.Sin(angle), 0, -Mathf.Cos(angle));

        // ������ �ٶ󺸵��� ȸ��
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation;
        }
    }
}
