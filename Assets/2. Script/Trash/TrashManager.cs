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
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<TrashManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }
    private static TrashManager m_instance;

    public int score = 0;
    public int wrong = 0; //Ʋ�� ����

    public GameObject trashspawner;
    public AudioSource audiosource;
    public AudioClip trash_bgm;
    public AudioClip correct;    //���� ����
    public AudioClip how_tts;
    public AudioClip wrongsound;
    public AudioClip selectSound;
    public AudioClip result;
    public AudioClip[] ttssounds;

    public GameObject howUI; //ó�� ���۵ǰ� ���� UI;
    public GameObject[] tts;
    public GameObject tts_btn;
    public GameObject how_movie;
    public GameObject gamestart_btn;
    public GameObject GameManu;

    public GameObject scoretxt_obj; //������ ����Ǵ� ���� ������ ����
    public TMP_Text score_text; // ���� ���� ���϶� ����UI
    public GameObject record_obj; //���UI

    public GameObject score_obj; //������ ������ ���� UI
    public TMP_Text recordscore; // ��Ͽ� ����

    public GameObject[] how_UI; //�и����� ��� UI
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
        score_text.text = "����\n" + score;
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
        //����ȭ������ ���ư���
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
        recordscore.text = "����  : " + score;
        ScoreManager.instance.StarCount(wrong);
        Debug.Log("���ӳ�");
    }
}
