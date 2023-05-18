using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Top : MonoBehaviour
{
   
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private AudioSource Topsesi;
    
   
    private void OnTriggerEnter(Collider other)
    {
        Topsesi.Play();

        if (other.CompareTag("Basket"))
        {
            _GameManager.Basket(transform.position);

        }
        else if (other.CompareTag("OyunBitti"))
        {
            _GameManager.Kaybettin();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Topsesi.Play();
     
 
    }


    

    
    
}
