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
    public GameObject[] Hand;
    public GameObject[] tts;
    public GameObject learn;
    public GameObject[] earthbtn;

    private AudioSource audioSource;
    public AudioClip selectsound;
    public AudioClip[] ttssound;

    private void Awake()
    {
        ScoreManager.instance.gamekey = "Music";
    }
    private void Start()
    {
       endGameUI.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void GameStart()
    {
        audioSource.PlayOneShot(selectsound);
        notesSpawner.SetActive(true);
        Hand[0].SetActive(true);
        Hand[1].SetActive(true);
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
    
    public void Learn()
    {
        audioSource.PlayOneShot(selectsound);
        StartCoroutine("Learn2");
    }
    IEnumerator Learn2()
    {
        yield return new WaitForSeconds(2f);
        learn.SetActive(true);
        audioSource.PlayOneShot(ttssound[0]);
        yield return new WaitForSeconds(13f);
        tts[0].SetActive(false);
        tts[1].SetActive(true);
        audioSource.PlayOneShot(ttssound[1]);
        yield return new WaitForSeconds(17f);
        tts[1].SetActive(false);
        tts[2].SetActive(true);
        audioSource.PlayOneShot(ttssound[2]);
        yield return new WaitForSeconds(5f);
        earthbtn[0].SetActive(true);
        yield return new WaitForSeconds(25f);
        earthbtn[1].SetActive(true);
    }
}
