using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainManager: MonoBehaviour
{
    public Button[] gamebuttons;
    public GameObject[] images;
    public GameObject noSelectUI;
    public GameObject updateUI;
    private AudioSource AudioSource;
    public AudioClip selectsound;
    private int gamenum=-1; //선택한 게임 번호 저장

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        for (int i = 0; i < gamebuttons.Length; i++)
        {
            int index = i;
            gamebuttons[i].onClick.AddListener(() => OnClickbtn(index));
        }
    }

    public void OnClickbtn(int index)
    {
        foreach (var img in images)
        {
            img.SetActive(false);
        }
        if(index >= 0 && index < images.Length)
        {
            images[index].SetActive(true);
            gamenum = index;
        }
    }
    public void SelectGame()
    {
        if (gamenum == -1)
        {
            StartCoroutine(ErrorUI(noSelectUI));
        }
        else if (gamenum == 0)
        {
            AudioSource.PlayOneShot(selectsound);
            SceneLoad.instance.LoadScene("Trash");
        }
        else if (gamenum == 1)
        {
            AudioSource.PlayOneShot(selectsound);
            SceneLoad.instance.LoadScene("Sea");
        }
        else if (gamenum == 2 || gamenum == 3)
        {
            StartCoroutine(ErrorUI(updateUI));
        }
    }
    IEnumerator ErrorUI(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(2f);
        obj.SetActive(false);
    }
}
