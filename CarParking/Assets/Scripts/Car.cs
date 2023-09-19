using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public bool moveForward;
    bool startPointCheck = false;
    public GameObject[] trails;

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
        if(collision.gameObject.CompareTag("StartPoint"))
        {
            startPointCheck = true;
            gameManager.startPoint.SetActive(false);
        }
        else if(collision.gameObject.CompareTag("Parking"))
        {
            moveForward=false;
            transform.SetParent(parent);
            trails[0].SetActive(false);
            trails[1].SetActive(false);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            gameManager.GetNewCar();

        }
        else if(collision.gameObject.CompareTag("Mid"))
        {
            Destroy(gameObject); // Lose Canvas 
        }
        else if (collision.gameObject.CompareTag("Car"))
        {
            //Lose Canvas 
        }
        else if(collision.gameObject.CompareTag("Diamond"))
        {
            gameManager.diamondNum++;
            Destroy(collision.gameObject);
        }
            

    }
}
