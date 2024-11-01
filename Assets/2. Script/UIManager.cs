using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<UIManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }
    private static UIManager m_instance;

    public GameObject[] hittext;

    public void hitText(int hit)
    {
        if(hit == 3)
        {
            hittext[3].SetActive(true);
        }
        else if (hit == 2)
        {
            hittext[2].SetActive(true);
        }
        else if (hit == 1)
        {
            hittext[1].SetActive(true);
        }
        else
        {
            hittext[0].SetActive(true);
        }
        StartCoroutine("offtext");
    }
    IEnumerator offtext()
    {
        yield return new WaitForSeconds(1f);
        foreach(GameObject text in hittext)
        {
            text.SetActive(false);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
