using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlatformKucultme : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Sure;
    [SerializeField] private int BaslangicSuresi;
    [SerializeField] private GameManager _GameManager;
    IEnumerator Start()
    {
        Sure.text = BaslangicSuresi.ToString();

        while (true)
        {
            yield return new WaitForSeconds(1f);
            BaslangicSuresi--;
            Sure.text = BaslangicSuresi.ToString();

            if (BaslangicSuresi == 0)
            {
                gameObject.SetActive(false);
                break;
            }
        }
    }  

    private void OnTriggerEnter(Collider other)
    {
         gameObject.SetActive(false);
        _GameManager.PlatformKucult(transform.position);
    }
}
