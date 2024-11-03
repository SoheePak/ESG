using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    public GameObject video_btn;
    public GameObject canvers;
    public GameObject video;
    public GameObject nextscene;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("StartStudy");
    }
    IEnumerator StartStudy()
    {
        yield return new WaitForSeconds(2f);
        canvers.SetActive(true);
        audioSource.PlayOneShot(audioClips[0]); //tts재생
        yield return new WaitForSeconds(14f);
        video_btn.SetActive(true);         //동영상 재생을 위한 버튼
    }
    public void VideoStart_btn()
    {
        video.SetActive(true);
        StartCoroutine("VideoEnd");
    }
    IEnumerator VideoEnd()
    {
        yield return new WaitForSeconds(120f);
        nextscene.SetActive(true);
    }
}
