using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceManager : MonoBehaviour
{
    public DOTweenPath camerapath;
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    public GameObject canvas;

    [SerializeField]
    public Transform cameratrasform;

    public GameObject earth;                        // 지구 오브젝트
    private Renderer earthRenderer;
    public Transform movetransform;                 // 이동할 포지션
    public GameObject ecoco;
    public GameObject ecocoUI;

    public Transform lastmove;


    void Start()
    {
        camerapath = FindObjectOfType<DOTweenPath>();
        audioSource = GetComponent<AudioSource>();
        earthRenderer = earth.GetComponent<Renderer>();
    }
    public void OnClickGameStart_btn()
    { 
        audioSource.PlayOneShot(audioClips[2]);
        StartCoroutine("CameraMove");
    }
    IEnumerator CameraMove()
    {
        yield return new WaitForSeconds(3f);        //주변을 둘러볼 시간 
        PlayAudioClip(1);
        yield return new WaitForSeconds(15f);
        camerapath.DOPlay();                        //카메라 시점 이동
        yield return new WaitForSeconds(1f);
        audioSource.clip = audioClips[0];           
        audioSource.Play();                         //긴장감 있는 배경음악으로 바뀜
        yield return new WaitForSeconds(2f);
        canvas.SetActive(true);                     //지구 온난화 동영상 재생
        yield return new WaitForSeconds(2f);
        StartCoroutine(BlinkEarth());               //지구 깜빡임
        yield return new WaitForSeconds(40f);
        StartCoroutine("Ecoco");
    }
    IEnumerator Ecoco()
    {
        Debug.Log("Ecoco");
        cameratrasform.DOMove(movetransform.position, 2f);
        yield return new WaitForSeconds(3f);
        ecoco.SetActive(true);
        ecocoUI.SetActive(true);
        audioSource.PlayOneShot(audioClips[3]);
    }

    private void PlayAudioClip(int index)
    {
        if (audioClips != null && index < audioClips.Length)
        {
            audioSource.PlayOneShot(audioClips[index]);
        }
    }
    IEnumerator BlinkEarth()
    {
        while (true) // 무한 반복
        {
            earthRenderer.material.color = Color.red; // 빨간색으로 변경
            yield return new WaitForSeconds(0.5f); // 0.5초 대기
            earthRenderer.material.color = Color.white; // 원래 색상으로 변경
            yield return new WaitForSeconds(0.5f); // 0.5초 대기
        }
    }
    public void GoEarth()
    {//모험 버튼을 누르면 실행
        audioSource.PlayOneShot(audioClips[4]);
        StartCoroutine(GoNextScene());
    }
    IEnumerator GoNextScene()
    {
        yield return new WaitForSeconds(4f);
        cameratrasform.DOMove(lastmove.position, 4f).OnComplete(() =>
        {
            SceneLoad.instance.LoadScene("Study"); // 이동이 완료된 후 씬 로드
        });
    }

}
