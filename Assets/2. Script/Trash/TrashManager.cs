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

    public GameObject canvers; //ó�� ���۵ǰ� ���� UI;
    public GameObject[] tts;
    public GameObject tts_btn;

    public GameObject scoretxt_obj; //������ ����Ǵ� ���� ������ ����
    public TMP_Text score_text; // ���� ���� ���϶� ����UI
    public GameObject record_obj; //���UI

    public GameObject score_obj; //������ ������ ���� UI
    public TMP_Text recordscore; // ��Ͽ� ����

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
        score_text.text = "����\n" + score;
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
        recordscore.text = "����  : " + score;
        ScoreManager.instance.StarCount(wrong);
        Debug.Log("���ӳ�");
    }
}
