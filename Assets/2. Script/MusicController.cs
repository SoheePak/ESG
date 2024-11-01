using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicController : MonoBehaviour
{
    public Button[] musicButtons;
    public Button[] levelButtons;
    public AudioClip[] musicClip;
    public GameObject[] musicImage; // �뷡 �̹���
    public GameObject sampleImage; //�뷡 ���� �� �����̹���
    private GameObject bfmusicImage; //���� �̹��� ������ ���� ����
    public AudioSource audioSource;
    private Button bfbutton; // ���� ��ư ������ ���� ����

    public GameObject Error_obj; //�뷡 or ���̵��� �������� �ʾ��� ��
    public TMP_Text Error_text;

    public int Choicemusicnum; //������ �뷡 ��ȣ
    public int Choicelevelnum; //������ ���� ���̵�

    public GameObject choice; //����â
    void Start()
    {
        for (int i = 0; i < musicButtons.Length; i++)
        {
            int index = i;
            musicButtons[i].onClick.AddListener(() => PlayMusic(index));
        }
        for (int i = 0; i < levelButtons.Length; i++)
        {

            int index = i;
            levelButtons[i].onClick.AddListener(() => ChangeLevel(index));
        }
        bfmusicImage = sampleImage;
        Choicelevelnum = -1;
        Choicemusicnum = -1;
    }

    void PlayMusic(int index)
    {
        if (audioSource.isPlaying)  //������ ������̸�
        {   
            audioSource.Stop();     //������ ���߰�    
        }
        audioSource.clip = musicClip[index]; 
        audioSource.Play();         //���� ��ư�� �뷡�� �����Ŵ
        ChangeMusic(index);
    }
    void ChangeMusicImage(int index)
    { //���� ���� �̹��� ����
        bfmusicImage.SetActive(false);
        musicImage[index].SetActive(true);
        bfmusicImage = musicImage[index];
    }
    void ChangeMusic(int index)
    { //�뷡 ����
        if(Choicemusicnum != -1)
        {
            Privious(musicButtons[Choicemusicnum]);
        }
        Selectbtn(musicButtons[index]);
        ChangeMusicImage(index);
        Choicemusicnum = index; // ������ �뷡
    }
    void ChangeLevel(int index)
    { // ���̵� ����
        if (Choicelevelnum != -1)
        {
            Privious(levelButtons[Choicelevelnum]);
        }
        Selectbtn(levelButtons[index]);
        Choicelevelnum = index; // ������ ���̵�
    }
    void Selectbtn(Button button)
    {//������ �̹��� �������ϰ� ����
        Image buttonImage = button.GetComponent<Image>();
        Color newColor = buttonImage.color;
        newColor.a = 1; // ���� ����
        buttonImage.color = newColor; // ����� ���� ����
    }
    void Privious(Button button)
    {
        if (button != null) //�������� �ʾҴٸ�
        {
            Image bfImage= button.GetComponent<Image>();
            Color bfColor = bfImage.color;
            bfColor.a = 0.2f; // ���� ����
            bfImage.color = bfColor; // ����� ���� ����
        }
    }

    public void GameStart_btn()
    {
        if(Choicemusicnum != -1 && Choicelevelnum != -1)
        { //�� �� �����ߴٸ� ���� ����
            audioSource.Stop();
            choice.SetActive(false);
            MusicGameManager.instance.notesSpawner.SetActive(true); // ���ӽ���
            MusicGameManager.instance.level = Choicelevelnum;
        }
        else if(Choicemusicnum == -1) 
        {//������ �������� �ʾҴٸ�
            Error_text.text = "�뷡��";
            StartCoroutine("Error");
        }
        else if(Choicelevelnum == -1)
        {
            Error_text.text = "���̵���";
            StartCoroutine("Error");
        }
    }
    IEnumerator Error()
    {
        Error_obj.SetActive(true);
        yield return new WaitForSeconds(2f);
        Error_obj.SetActive(false);
    }
}
