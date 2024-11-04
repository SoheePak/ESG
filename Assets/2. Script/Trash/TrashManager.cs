using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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
    public AudioClip correct;    //정답 사운드
    public AudioClip how_tts;
    public AudioClip wrongsound;
    public AudioClip selectSound;
    public AudioClip result;
    public AudioClip[] ttssounds;

    public GameObject howUI; //처음 시작되고 켜질 UI;
    public GameObject[] tts;
    public GameObject tts_btn;
    public GameObject how_movie;
    public GameObject gamestart_btn;
    public GameObject GameManu;

    public GameObject scoretxt_obj; //게임이 진행되는 동안 보여질 점수
    public TMP_Text score_text; // 게임 진행 중일때 점수UI
    public GameObject record_obj; //등수UI

    public GameObject score_obj; //게임이 끝나고 나올 UI
    public TMP_Text recordscore; // 기록용 점수

    public GameObject[] how_UI; //분리수거 방법 UI
    public GameObject movie_btn;
    public GameObject wrongUI;

    private void Awake()
    {
        ScoreManager.instance.gamekey = "Trash";
    }

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }
    public void Learn()
    {
        StartCoroutine("learn");
    }
    IEnumerator learn()
    {
        yield return new WaitForSeconds(1f);
        howUI.SetActive(true);
        audiosource.PlayOneShot(ttssounds[0]);
        yield return new WaitForSeconds(14f);
        tts[0].SetActive(false);    
        tts[1].SetActive(true);
        audiosource.PlayOneShot(ttssounds[1]);
        yield return new WaitForSeconds(14f);
        tts_btn.SetActive(true);
}
    public void How1()
    {
        how_UI[0].SetActive(true);
        StartCoroutine("How2");
    }
    IEnumerator How2()
    {
        yield return new WaitForSeconds(2f);
        audiosource.PlayOneShot(how_tts);
        how_UI[1].SetActive(true);
        yield return new WaitForSeconds(15f);
        movie_btn.SetActive(true);
    }
    public void How_movie()
    {
        how_movie.SetActive(true);
        StartCoroutine("How_movie2");
    }
    IEnumerator How_movie2()
    {
        yield return new WaitForSeconds(33f);
        gamestart_btn.SetActive(true);
    }
    public void UPScore()
    {
        audiosource.PlayOneShot(correct);
        score += 10;
        score_text.text = "점수\n" + score;
    }
    public void WrongCount()
    {
        audiosource.PlayOneShot(wrongsound);
        wrong++;
        wrongUI.SetActive(true);
        StartCoroutine("WrongUI");
    }
    IEnumerator WrongUI()
    {
        yield return new WaitForSeconds(0.5f);
        wrongUI.SetActive(false);
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
        audiosource.PlayOneShot(selectSound);
        audiosource.clip = trash_bgm;
        audiosource.Play();
    }
    public void EndGame()
    {
        trashspawner.SetActive(false);
        StartCoroutine("EndUI");
    }
    IEnumerator EndUI()
    {
        yield return new WaitForSeconds(5f);
        audiosource.PlayOneShot(result);
        score_obj.SetActive(true);
        recordscore.text = "점수  : " + score;
        ScoreManager.instance.StarCount(wrong);
        Debug.Log("게임끝");
    }
}
