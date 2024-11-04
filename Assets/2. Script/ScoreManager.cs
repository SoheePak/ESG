using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using OpenCover.Framework.Model;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<ScoreManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }
    private static ScoreManager m_instance;


    public string gamekey = "Music"; //�⺻�� ���ǰ������� ����

    public struct ScoreData
    {
        public string playerName;
        public int score;

        public ScoreData(string name, int score)
        {
            playerName = name;
            this.score = score;
        }
    }


    public GameObject[] starcount;
    private List<ScoreData> scoreDatalist = new List<ScoreData>();

    public GameObject recordUI;
    public GameObject recordAsk;

    public InputField askname;
    public Transform content; //��ũ�Ѻ��� content
    public GameObject scoretextPrefab;
    public int score;


    private void Start()
    {
        LoadScores(); // ����� ���� �ε�
        UpdateScoreUI(); // UI ������Ʈ
    }
    public void RankUI()
    {
        recordUI.SetActive(true);
        LoadScores();
    }
    private void LoadScores()
    {
        Debug.Log("��ŷ �ý��� ȣ��");
        int scoreCount = PlayerPrefs.GetInt("{gamekey}_ScoreCount", 0);
        scoreDatalist.Clear(); // ���� ������ �ʱ�ȭ

        for (int i = 0; i < scoreCount; i++)
        {
            string playerName = PlayerPrefs.GetString("{gamekey}_PlayerName_{i}", "Unknown");
            int playerScore = PlayerPrefs.GetInt("{gamekey}_PlayerScore_{i}", 0);
            scoreDatalist.Add(new ScoreData(playerName, playerScore));
        }
        SortScore(); // �ҷ��� �� ����
    }
    public void AddScore(string playerName, int score)
    {
        scoreDatalist.Add(new ScoreData(playerName, score));
        SortScore();
        UpdateScoreUI();
        SaveScores();
    }
    private void SaveScores()
    {
        Debug.Log("������");
        for (int i = 0; i < scoreDatalist.Count; i++)
        {
            PlayerPrefs.SetString("{gamekey}_PlayerName_{i}", scoreDatalist[i].playerName);
            PlayerPrefs.SetInt("{gamekey}_PlayerScore_{i}", scoreDatalist[i].score);
        }
        PlayerPrefs.SetInt("{gamekey}_ScoreCount", scoreDatalist.Count);
        PlayerPrefs.Save(); // ������ ����
    }

        private void SortScore()
    {
        scoreDatalist.Sort((x, y) => y.score.CompareTo(x.score)); // �������� ����
        if (scoreDatalist.Count > 5)
        {
            scoreDatalist.RemoveAt(scoreDatalist.Count - 1); // ���� 5���� ����
        }
    }
    IEnumerator CheakRank()
    {
        CurrentScene();
        yield return new WaitForSeconds(2f);
        
            if (scoreDatalist.Count < 5 || scoreDatalist.Count - 1 < score)
            {// ����Ʈ�� �������� �ִ°ͺ��� ������ ũ�ٸ�
                recordAsk.SetActive(true);
            }
    }
    public void CurrentScene()
    {//���� ���� ���� ������ ������ �뵵
        if(SceneManager.GetActiveScene().name == "Sea")
        {
            score = NotesSpawner.instance.score;
        }
        else if(SceneManager.GetActiveScene().name == "Trash")
        {
            score = TrashManager.instance.score;
        }
    } 
    public void AskName()
    {
        recordAsk.SetActive(false);
    }
    public void UploadName() {
        string playername = askname.text;
        AddScore(playername, score);
    }
    public void UpdateScoreUI()
    {
        foreach(Transform child in content)
        {
            Destroy(child.gameObject);
        }
        for(int i= 0; i<scoreDatalist.Count;i++)
        {
            GameObject scoreObj = Instantiate(scoretextPrefab, content);
            TMP_Text rankText = scoreObj.transform.Find("rank").GetComponent<TMP_Text>();
            TMP_Text nameText = scoreObj.transform.Find("name").GetComponent<TMP_Text>();
            TMP_Text ScoreText = scoreObj.transform.Find("score").GetComponent<TMP_Text>();

            rankText.text = (i+1).ToString();
            nameText.text = scoreDatalist[i].playerName;
            ScoreText.text = scoreDatalist[i].score.ToString();
            Debug.Log("���");
        }
    }
    public void StarCount(int count)
    {//Ʋ�� ������ ���� ��
        if (count <= 5)
        {
            //�� 3��
            starcount[2].SetActive(true);

        }
        else if (count <= 8)
        {
            //�� 2��
            starcount[1].SetActive(true);
        }
        else
        {
            //�� 1��
            starcount[0].SetActive(true);
        }
        StartCoroutine("CheakRank");
    }
}
