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

    public GameObject earth;                        // ���� ������Ʈ
    private Renderer earthRenderer;
    public Transform movetransform;                 // �̵��� ������
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
        yield return new WaitForSeconds(3f);        //�ֺ��� �ѷ��� �ð� 
        PlayAudioClip(1);
        yield return new WaitForSeconds(15f);
        camerapath.DOPlay();                        //ī�޶� ���� �̵�
        yield return new WaitForSeconds(1f);
        audioSource.clip = audioClips[0];           
        audioSource.Play();                         //���尨 �ִ� ����������� �ٲ�
        yield return new WaitForSeconds(2f);
        canvas.SetActive(true);                     //���� �³�ȭ ������ ���
        yield return new WaitForSeconds(2f);
        StartCoroutine(BlinkEarth());               //���� ������
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
        while (true) // ���� �ݺ�
        {
            earthRenderer.material.color = Color.red; // ���������� ����
            yield return new WaitForSeconds(0.5f); // 0.5�� ���
            earthRenderer.material.color = Color.white; // ���� �������� ����
            yield return new WaitForSeconds(0.5f); // 0.5�� ���
        }
    }
    public void GoEarth()
    {//���� ��ư�� ������ ����
        audioSource.PlayOneShot(audioClips[4]);
        StartCoroutine(GoNextScene());
    }
    IEnumerator GoNextScene()
    {
        yield return new WaitForSeconds(4f);
        cameratrasform.DOMove(lastmove.position, 4f).OnComplete(() =>
        {
            SceneLoad.instance.LoadScene("Study"); // �̵��� �Ϸ�� �� �� �ε�
        });
    }

}
