using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public bool moveForward;
    bool startPointCheck = false;
    public GameObject[] trails;
    public GameObject particlePoint;

    public Transform parent;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame

    void Update()
    {
         if(!startPointCheck)
            transform.Translate(3f*Time.deltaTime*transform.forward);  
        if(moveForward)
            transform.Translate(15f*Time.deltaTime*transform.forward);        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Parking"))
        {
            CarTechnic();
            transform.SetParent(parent);
            gameManager.GetNewCar();

        }
        else if (collision.gameObject.CompareTag("Car"))
        {
            gameManager.Crash.transform.position = particlePoint.transform.position;
            gameManager.Crash.Play();
            CarTechnic();   
            gameManager.Failed();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("StartPoint"))
        {
            startPointCheck = true;
        }
        else if (other.CompareTag("Diamond"))
        {
            gameManager.diamondNum++;
            other.gameObject.SetActive(false);
            gameManager.Sounds[0].Play();
        }
        else if(other.CompareTag("Mid"))
        {
            gameManager.Crash.transform.position = particlePoint.transform.position;
            gameManager.Crash.Play();
            CarTechnic();
            gameManager.Failed();
        }
        else if(other.CompareTag("Park"))
        {
            other.gameObject.GetComponent<Stop>().Parking.SetActive(true);
        }
    }
    void CarTechnic()
    {
        moveForward=false;
        trails[0].SetActive(false);
        trails[1].SetActive(false);
    }
}
