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
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<ScoreManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }
    private static ScoreManager m_instance;


    public string gamekey = "Music"; //기본값 음악게임으로 설정

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
    public Transform content; //스크롤뷰의 content
    public GameObject scoretextPrefab;
    public int score;


    private void Start()
    {
        LoadScores(); // 저장된 점수 로드
        UpdateScoreUI(); // UI 업데이트
    }
    public void RankUI()
    {
        recordUI.SetActive(true);
        LoadScores();
    }
    private void LoadScores()
    {
        Debug.Log("랭킹 시스템 호출");
        int scoreCount = PlayerPrefs.GetInt("{gamekey}_ScoreCount", 0);
        scoreDatalist.Clear(); // 기존 데이터 초기화

        for (int i = 0; i < scoreCount; i++)
        {
            string playerName = PlayerPrefs.GetString("{gamekey}_PlayerName_{i}", "Unknown");
            int playerScore = PlayerPrefs.GetInt("{gamekey}_PlayerScore_{i}", 0);
            scoreDatalist.Add(new ScoreData(playerName, playerScore));
        }
        SortScore(); // 불러온 후 정렬
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
        Debug.Log("저장함");
        for (int i = 0; i < scoreDatalist.Count; i++)
        {
            PlayerPrefs.SetString("{gamekey}_PlayerName_{i}", scoreDatalist[i].playerName);
            PlayerPrefs.SetInt("{gamekey}_PlayerScore_{i}", scoreDatalist[i].score);
        }
        PlayerPrefs.SetInt("{gamekey}_ScoreCount", scoreDatalist.Count);
        PlayerPrefs.Save(); // 데이터 저장
    }

        private void SortScore()
    {
        scoreDatalist.Sort((x, y) => y.score.CompareTo(x.score)); // 내림차순 정렬
        if (scoreDatalist.Count > 5)
        {
            scoreDatalist.RemoveAt(scoreDatalist.Count - 1); // 상위 5개만 유지
        }
    }
    IEnumerator CheakRank()
    {
        CurrentScene();
        yield return new WaitForSeconds(2f);
        
            if (scoreDatalist.Count < 5 || scoreDatalist.Count - 1 < score)
            {// 리스트의 마지막에 있는것보다 점수가 크다면
                recordAsk.SetActive(true);
            }
    }
    public void CurrentScene()
    {//현재 씬에 맞춰 점수를 저장할 용도
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
            Debug.Log("등록");
        }
    }
    public void StarCount(int count)
    {//틀린 갯수에 따른 별
        if (count <= 5)
        {
            //별 3개
            starcount[2].SetActive(true);

        }
        else if (count <= 8)
        {
            //별 2개
            starcount[1].SetActive(true);
        }
        else
        {
            //별 1개
            starcount[0].SetActive(true);
        }
        StartCoroutine("CheakRank");
    }
}
