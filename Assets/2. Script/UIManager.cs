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
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<UIManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
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
