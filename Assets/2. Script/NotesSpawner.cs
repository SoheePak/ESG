using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class NotesSpawner : MonoBehaviour
{
    public static NotesSpawner instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<NotesSpawner>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }
    private static NotesSpawner m_instance;

    public GameObject[] notes; // 노트 프리팹 배열
    public Transform[] pos; // 노트를 생성할 위치 배열

    public AudioSource audioSource; // 오디오 소스
    private float[] spectrumData = new float[256]; // 주파수 스펙트럼 데이터
    private float timer; // 타이머
    public float beatThreshold = 0.2f; // 비트 감지를 위한 임계값

    public int score;
    public TMP_Text scoretext;
    public int bestscore;

    public int level;  // 선택한 레벨 난이도

    public float beatInterval = 1.0f; //비트가 발생하는 시간 간격
    private float spawnTimer;
    private float noteCooldown = 1.5f; // 노트 생성 후 대기 시간
    private float lastNoteTime; // 마지막 노트 생성 시간
    

    public GameObject score_obj; // 점수
    public int wrong = 0; //틀린갯수
    private MusicController musicController;
    public TMP_Text Finalscoretext;
    bool isplay = false;//플레이 여부를 확인하기 위한 변수

    void Start()
    {
        Debug.Log("활성화");
        if (audioSource != null)
        {
            audioSource.Play();
        }
        score_obj.SetActive(true);
        level = MusicGameManager.instance.level+1; 
        
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            // 현재 주파수 데이터를 가져오기
            audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);
            spawnTimer += Time.deltaTime;
            isplay = true;
            // 비트 감지
            if (DetectBeat() && spawnTimer >= beatInterval && (Time.time - lastNoteTime) >= noteCooldown/level)
            {
                SpawnNote();
                lastNoteTime = Time.time;
                spawnTimer = 0f;
            }
        }
        else if(!audioSource.isPlaying && isplay)
        {//음악이 끝났고, 게임을 했다면 
            isplay = false;
            MusicGameManager.instance.GameEnd();
            score_obj.SetActive(false);
            Finalscoretext.text = "점수 : " + score;
            ScoreManager.instance.StarCount(wrong);
        }
    }

    bool DetectBeat()
    {
        // 특정 주파수 대역의 에너지를 분석하여 비트를 감지
        float totalEnergy = 0f;
        for (int i = 0; i < spectrumData.Length; i++)
        {
            totalEnergy += spectrumData[i];
        }

        // 임계값을 초과하고, 마지막 비트 발생 시간이 beatInterval보다 오래되었는지 확인
        return totalEnergy > beatThreshold && (Time.time - lastNoteTime) > beatInterval;

        return false; // 비트 미감지
    }

    void SpawnNote()
    {
        int i = Random.Range(0, pos.Length); // 랜덤한 위치 선택
        GameObject note = Instantiate(notes[Random.Range(0, notes.Length)], pos[i].position, Quaternion.identity);
        note.transform.rotation = Quaternion.Euler(0, 180, 0); 
        //note.transform.localPosition = Vector3.zero; // 위치 초기화
    }
    public void MinusScore()
    {
        wrong++;
        if(score >= 30)
        {
            score -= 30;
            scoretext.text = "점수\n" + score;
        }
    }
    public void AddScore()
    {
        score += 100;
        scoretext.text = "점수\n" + score;
    }
}