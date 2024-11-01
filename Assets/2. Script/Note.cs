using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Note : MonoBehaviour
{
    Collider Collider;
    NotesSpawner notesspawener;
    public float speed = 5f;
    private bool ismove; //움직일수 있는 상태(처음에 태어났을 때)\
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
            ismove = false; //손으로 잡았을 때 멈춤.(손에서 떨어뜨렸을때 멈추기 위해)
            rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            return;
        }
        if (this.gameObject.tag == other.gameObject.tag)
        { //분리수거를 성공했다면
            TrashManager.instance.UPScore();
            StartCoroutine("DestroyObj"); //일정 시간 뒤에 오브젝트를 삭제
        }
        else
        {//분리수거 실패했다면
            StartCoroutine("DestroyObj");
            TrashManager.instance.wrong++;
        }
        if (other.CompareTag("Fail")|| other.CompareTag("Floor"))
        {//놓침
            TrashManager.instance.wrong++; 
            ismove = false; //놓치면 통 안에서 멈춤
            StartCoroutine("DestroyObj");
        }

    }
    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    
} 
