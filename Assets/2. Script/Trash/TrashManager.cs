using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TrashManager : MonoBehaviour
{
    public static TrashManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<TrashManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }
    private static TrashManager m_instance;

    public int score = 0;
    public int wrong = 0; //틀린 갯수

    public GameObject trashspawner;
    public AudioSource audiosource;
    public AudioClip trash_bgm;

    public GameObject canvers; //처음 시작되고 켜질 UI;
    public GameObject[] tts;
    public GameObject tts_btn;

    public GameObject scoretxt_obj; //게임이 진행되는 동안 보여질 점수
    public TMP_Text score_text; // 게임 진행 중일때 점수UI
    public GameObject record_obj; //등수UI

    public GameObject score_obj; //게임이 끝나고 나올 UI
    public TMP_Text recordscore; // 기록용 점수

    private void Awake()
    {
        ScoreManager.instance.gamekey = "Trash";
    }

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
        StartCoroutine("GameStart");
    }
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3f);
        canvers.SetActive(true);
        yield return new WaitForSeconds(14f);
        tts[0].SetActive(false);    
        tts[1].SetActive(true);
        yield return new WaitForSeconds(14f);
        tts_btn.SetActive(true);
}


    public void UPScore()
    {
        score += 10;
        score_text.text = "점수\n" + score;
    }
    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnClickHome()
    {
        //메인화면으로 돌아가기
    }
    public void StartGame()
    {
        trashspawner.SetActive(true);
        scoretxt_obj.SetActive(true);
        record_obj.SetActive(false);
    }
    public void EndGame()
    {
        trashspawner.SetActive(false);
        StartCoroutine("EndUI");
    }
    IEnumerator EndUI()
    {
        yield return new WaitForSeconds(3f);
        score_obj.SetActive(true);
        recordscore.text = "점수  : " + score;
        ScoreManager.instance.StarCount(wrong);
        Debug.Log("게임끝");
    }
}
