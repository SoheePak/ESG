using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashSpawner : MonoBehaviour
{
    public GameObject[] trashs;

    public float time = 1;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        StartCoroutine("Timeout");
    }
    IEnumerator Timeout()
    { //  제한시간이 끝나면 게임 종료
        yield return new WaitForSeconds(10f); 
        TrashManager.instance.EndGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > time)
        {
            timer -= time;
            GameObject note =
                Instantiate(trashs[Random.Range(0, trashs.Length)], transform.position,transform.rotation);
        }
        timer += Time.deltaTime;
    }
}
