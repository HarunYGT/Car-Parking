using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Car Settings")]
    public GameObject[] Cars;
    int activeCarIndex;
    public int howManyCars;

    [Header("Canvas Settings")]
    public GameObject[] CarCanvasImages;
    public Texture CarGreen;
    public TextMeshProUGUI[] texts;
    public GameObject[] panels;
    public GameObject[] TaptoButtons;

    [Header("Platform Settings")]
    public GameObject Platform_1;
    public GameObject Platform_Circle;
    public float[] rotateSpeed;
    bool isRotate;

    [Header("Level Settings")]
    public int diamondNum;
    public ParticleSystem Crash;
    public AudioSource[] Sounds;

    int remainCarNum;

    void Start()
    {
        isRotate = true;
        FirstSetup();
        remainCarNum = howManyCars;
        for (int i = 0; i < howManyCars; i++)
        {
            CarCanvasImages[i].SetActive(true);
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            Cars[activeCarIndex].GetComponent<Car>().moveForward = true;
            activeCarIndex++;
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            panels[0].SetActive(false); 
            panels[3].SetActive(true);
        }
        if(isRotate)
        {
            Platform_1.transform.Rotate(new Vector3(0,0,rotateSpeed[0]),Space.Self);
            Platform_Circle.transform.Rotate(new Vector3(0,0,rotateSpeed[1]),Space.Self);
        }
           

    }

    public void GetNewCar()
    {
        remainCarNum--;
        if(activeCarIndex < howManyCars)
        {
            Cars[activeCarIndex].SetActive(true);  
        }else
        {
            Complete();
        }
        CarCanvasImages[activeCarIndex-1].GetComponent<RawImage>().texture = CarGreen;
    }
    public void Failed()
    {
        TextSet(6);
        panels[2].SetActive(true);
        panels[3].SetActive(false);
        isRotate = false;
        Sounds[1].Play();
        Sounds[3].Play();
        Invoke("LoseTapButton",3f);

    }
    void Complete()
    {
        PlayerPrefs.SetInt("Diamond",PlayerPrefs.GetInt("Diamond")+diamondNum);
        TextSet(2);
        panels[1].SetActive(true);
        panels[3].SetActive(false);
        Sounds[2].Play();
        Invoke("WinTapButton",3f);
    }
    public void WatchandReturn()
    {
        //Ad
    }
    public void WatchAndWin()
    {
        //Ad 
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level",SceneManager.GetActiveScene().buildIndex+1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    void WinTapButton()
    {
        TaptoButtons[0].SetActive(true);
    }
    void LoseTapButton()
    {
        TaptoButtons[1].SetActive(true);
    }
    void TextSet(int i)
    {
        texts[i].text = PlayerPrefs.GetInt("Diamond").ToString();
        texts[i+1].text = "LEVEL: " + SceneManager.GetActiveScene().name;
        texts[i+2].text = (howManyCars-remainCarNum).ToString();
        texts[i+3].text = diamondNum.ToString();
    }
      //Data Management
    public void FirstSetup()
    {
        if(!PlayerPrefs.HasKey("Diamond"))
        {
            PlayerPrefs.SetInt("Diamond",0);
            PlayerPrefs.SetInt("Level",1);
        }
        texts[0].text = PlayerPrefs.GetInt("Diamond").ToString();
        texts[1].text = "LEVEL: " + SceneManager.GetActiveScene().name;
    }
}
