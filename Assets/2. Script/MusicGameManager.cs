using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicGameManager : MonoBehaviour
{
    public static MusicGameManager instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<MusicGameManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }
    private static MusicGameManager m_instance;

    public GameObject notesSpawner;
    public GameObject endGameUI; //������ ��Ÿ���� UI
    public int level;
    public GameObject restart_btn;
    public GameObject home_btn;
    public GameObject record_btn;

    private void Awake()
    {
        ScoreManager.instance.gamekey = "Music";
    }
    private void Start()
    {
       endGameUI.SetActive(false);
    }

    public void GameStart()
    {
        notesSpawner.SetActive(true);
    }
    public void GameEnd()
    {
        //notesSpawner.SetActive(false);
        StartCoroutine("End");
    }
    IEnumerator End()
    {
        yield return new WaitForSeconds(3f);
        endGameUI.SetActive(true); 


        //������ ����� �� �ִ��� ����
    }
    
    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnClickHome()
    {
        //����ȭ������ ���ư���
    }
}
