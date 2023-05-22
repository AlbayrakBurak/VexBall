using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private AudioSource Topsesi;
    [SerializeField] private float UygulanacakGuc;

    private Rigidbody topRigidbody;
    private float dususAcisi;

    private void Start()
    {
        topRigidbody = GetComponent<Rigidbody>();
    }

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

        if (collision.gameObject.CompareTag("Platform"))
        {
            Vector3 platformYonu = collision.contacts[0].normal;
            Vector3 dususYonu = -collision.relativeVelocity.normalized;
            dususAcisi = Vector3.SignedAngle(dususYonu, Vector3.down, Vector3.forward);

            Vector3 gucYonu = Quaternion.Euler(0f, dususAcisi * 2f, 0f) * platformYonu;
            topRigidbody.AddForce(gucYonu * UygulanacakGuc, ForceMode.Force);
        }
    }
}
