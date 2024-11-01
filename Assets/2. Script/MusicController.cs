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
    public GameObject[] musicImage; // 노래 이미지
    public GameObject sampleImage; //노래 선택 전 샘플이미지
    private GameObject bfmusicImage; //이전 이미지 저장을 위한 변수
    public AudioSource audioSource;
    private Button bfbutton; // 이전 버튼 저장을 위한 변수

    public GameObject Error_obj; //노래 or 난이도를 선택하지 않았을 때
    public TMP_Text Error_text;

    public int Choicemusicnum; //선택한 노래 번호
    public int Choicelevelnum; //선택한 레벨 난이도

    public GameObject choice; //선택창
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
        if (audioSource.isPlaying)  //음악이 재생중이면
        {   
            audioSource.Stop();     //음악을 멈추고    
        }
        audioSource.clip = musicClip[index]; 
        audioSource.Play();         //누른 버튼의 노래를 재생시킴
        ChangeMusic(index);
    }
    void ChangeMusicImage(int index)
    { //선택 음악 이미지 변경
        bfmusicImage.SetActive(false);
        musicImage[index].SetActive(true);
        bfmusicImage = musicImage[index];
    }
    void ChangeMusic(int index)
    { //노래 선택
        if(Choicemusicnum != -1)
        {
            Privious(musicButtons[Choicemusicnum]);
        }
        Selectbtn(musicButtons[index]);
        ChangeMusicImage(index);
        Choicemusicnum = index; // 선택한 노래
    }
    void ChangeLevel(int index)
    { // 난이도 선택
        if (Choicelevelnum != -1)
        {
            Privious(levelButtons[Choicelevelnum]);
        }
        Selectbtn(levelButtons[index]);
        Choicelevelnum = index; // 선택한 난이도
    }
    void Selectbtn(Button button)
    {//선택한 이미지 불투명하게 설정
        Image buttonImage = button.GetComponent<Image>();
        Color newColor = buttonImage.color;
        newColor.a = 1; // 투명도 설정
        buttonImage.color = newColor; // 변경된 색상 적용
    }
    void Privious(Button button)
    {
        if (button != null) //선택하지 않았다면
        {
            Image bfImage= button.GetComponent<Image>();
            Color bfColor = bfImage.color;
            bfColor.a = 0.2f; // 투명도 설정
            bfImage.color = bfColor; // 변경된 색상 적용
        }
    }

    public void GameStart_btn()
    {
        if(Choicemusicnum != -1 && Choicelevelnum != -1)
        { //둘 다 선택했다면 게임 시작
            audioSource.Stop();
            choice.SetActive(false);
            MusicGameManager.instance.notesSpawner.SetActive(true); // 게임시작
            MusicGameManager.instance.level = Choicelevelnum;
        }
        else if(Choicemusicnum == -1) 
        {//음악을 선택하지 않았다면
            Error_text.text = "노래를";
            StartCoroutine("Error");
        }
        else if(Choicelevelnum == -1)
        {
            Error_text.text = "난이도를";
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
