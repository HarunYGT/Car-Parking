using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Car Settings")]
    public GameObject[] Cars;
    int activeCarIndex;
    public int howManyCars;
    public GameObject startPoint;

    [Header("Canvas Settings")]
    public GameObject[] CarCanvasImages;
    public Texture CarGreen;

    [Header("Platform Settings")]
    public GameObject Platform_1;
    public float[] rotateSpeed;

    [Header("Level Settings")]
    public int diamondNum;
     // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < howManyCars; i++)
        {
            CarCanvasImages[i].SetActive(true);
        }
    }
    public void GetNewCar()
    {
        startPoint.SetActive(true);
        if(activeCarIndex < howManyCars)
        {
            Cars[activeCarIndex].SetActive(true);  
        }
        CarCanvasImages[activeCarIndex-1].GetComponent<RawImage>().texture = CarGreen;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            Cars[activeCarIndex].GetComponent<Car>().moveForward = true;
            activeCarIndex++;
        }
        Platform_1.transform.Rotate(new Vector3(0,0,rotateSpeed[0]),Space.Self);

    }
    
}
