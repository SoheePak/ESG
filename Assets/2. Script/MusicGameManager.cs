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
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<MusicGameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }
    private static MusicGameManager m_instance;

    public GameObject notesSpawner;
    public GameObject endGameUI; //점수를 나타내는 UI
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


        //순위를 등록할 수 있는지 묻기
    }
    
    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnClickHome()
    {
        //메인화면으로 돌아가기
    }
}
