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
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<NotesSpawner>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }
    private static NotesSpawner m_instance;

    public GameObject[] notes; // ��Ʈ ������ �迭
    public Transform[] pos; // ��Ʈ�� ������ ��ġ �迭

    public AudioSource audioSource; // ����� �ҽ�
    private float[] spectrumData = new float[256]; // ���ļ� ����Ʈ�� ������
    private float timer; // Ÿ�̸�
    public float beatThreshold = 0.2f; // ��Ʈ ������ ���� �Ӱ谪

    public int score;
    public TMP_Text scoretext;
    public int bestscore;

    public int level;  // ������ ���� ���̵�

    public float beatInterval = 1.0f; //��Ʈ�� �߻��ϴ� �ð� ����
    private float spawnTimer;
    private float noteCooldown = 1.5f; // ��Ʈ ���� �� ��� �ð�
    private float lastNoteTime; // ������ ��Ʈ ���� �ð�
    

    public GameObject score_obj; // ����
    public int wrong = 0; //Ʋ������
    private MusicController musicController;
    public TMP_Text Finalscoretext;
    bool isplay = false;//�÷��� ���θ� Ȯ���ϱ� ���� ����

    void Start()
    {
        Debug.Log("Ȱ��ȭ");
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
            // ���� ���ļ� �����͸� ��������
            audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);
            spawnTimer += Time.deltaTime;
            isplay = true;
            // ��Ʈ ����
            if (DetectBeat() && spawnTimer >= beatInterval && (Time.time - lastNoteTime) >= noteCooldown/level)
            {
                SpawnNote();
                lastNoteTime = Time.time;
                spawnTimer = 0f;
            }
        }
        else if(!audioSource.isPlaying && isplay)
        {//������ ������, ������ �ߴٸ� 
            isplay = false;
            MusicGameManager.instance.GameEnd();
            score_obj.SetActive(false);
            Finalscoretext.text = "���� : " + score;
            ScoreManager.instance.StarCount(wrong);
        }
    }

    bool DetectBeat()
    {
        // Ư�� ���ļ� �뿪�� �������� �м��Ͽ� ��Ʈ�� ����
        float totalEnergy = 0f;
        for (int i = 0; i < spectrumData.Length; i++)
        {
            totalEnergy += spectrumData[i];
        }

        // �Ӱ谪�� �ʰ��ϰ�, ������ ��Ʈ �߻� �ð��� beatInterval���� �����Ǿ����� Ȯ��
        return totalEnergy > beatThreshold && (Time.time - lastNoteTime) > beatInterval;

        return false; // ��Ʈ �̰���
    }

    void SpawnNote()
    {
        int i = Random.Range(0, pos.Length); // ������ ��ġ ����
        GameObject note = Instantiate(notes[Random.Range(0, notes.Length)], pos[i].position, Quaternion.identity);
        note.transform.rotation = Quaternion.Euler(0, 180, 0); 
        //note.transform.localPosition = Vector3.zero; // ��ġ �ʱ�ȭ
    }
    public void MinusScore()
    {
        wrong++;
        if(score >= 30)
        {
            score -= 30;
            scoretext.text = "����\n" + score;
        }
    }
    public void AddScore()
    {
        score += 100;
        scoretext.text = "����\n" + score;
    }
}