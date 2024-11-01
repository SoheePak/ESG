using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Note : MonoBehaviour
{
    Collider Collider;
    NotesSpawner notesspawener;
    public float speed = 5f;
    private bool ismove; //�����ϼ� �ִ� ����(ó���� �¾�� ��)\
    private Rigidbody rb;
    void Start()
    {
        ismove = true;
        Collider = GetComponent<Collider>();
        notesspawener = FindObjectOfType<NotesSpawner>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
    // Update is called once per frame
    void Update()
    {
        if(ismove)
        {
           transform.position += Time.deltaTime * transform.forward * speed;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            notesspawener.MinusScore();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            ismove = false; //������ ����� �� ����.(�տ��� ����߷����� ���߱� ����)
            rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            return;
        }
        if (this.gameObject.tag == other.gameObject.tag)
        { //�и����Ÿ� �����ߴٸ�
            TrashManager.instance.UPScore();
            StartCoroutine("DestroyObj"); //���� �ð� �ڿ� ������Ʈ�� ����
        }
        else
        {//�и����� �����ߴٸ�
            StartCoroutine("DestroyObj");
            TrashManager.instance.wrong++;
        }
        if (other.CompareTag("Fail")|| other.CompareTag("Floor"))
        {//��ħ
            TrashManager.instance.wrong++; 
            ismove = false; //��ġ�� �� �ȿ��� ����
            StartCoroutine("DestroyObj");
        }

    }
    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    
} 
