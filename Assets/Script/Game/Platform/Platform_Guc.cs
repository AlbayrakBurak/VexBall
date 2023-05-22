using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Guc : MonoBehaviour
{
    [SerializeField] private float uygulanacakGuc;
    private Vector3 platformunSonPozisyonu;

    private void Start()
    {
        platformunSonPozisyonu = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody topRigidbody = collision.gameObject.GetComponent<Rigidbody>();

        if (topRigidbody != null)
        {
            Vector3 carpismaNoktasi = collision.contacts[0].point;
            Vector3 platformOrtasi = transform.position;
            float yatayFarkOrani = (carpismaNoktasi.x - platformOrtasi.x) / transform.localScale.x;

            Vector3 yukselmeYonu = collision.contacts[0].normal;
            yukselmeYonu.y = Mathf.Abs(yukselmeYonu.y); // Y ekseninde pozitif yön
            yukselmeYonu.Normalize();

            Vector3 uygulanacakKuvvet = uygulanacakGuc * yukselmeYonu;

            topRigidbody.AddForce(uygulanacakKuvvet, ForceMode.Impulse);

            // Topun yatay (x) hareket yönünü güncelle
            Vector3 hareketYonu = topRigidbody.velocity.normalized;

            float platformunHareketYonuX = platformunSonPozisyonu.x - transform.position.x;
            platformunSonPozisyonu = transform.position;

            hareketYonu.x += platformunHareketYonuX;
            hareketYonu.Normalize();

            topRigidbody.velocity = topRigidbody.velocity.magnitude * hareketYonu;
        }
    }
}
