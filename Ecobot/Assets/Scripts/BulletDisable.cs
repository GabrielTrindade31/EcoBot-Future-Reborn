using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDisable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall" || other.tag == "Platform" || other.tag == "Ground")
        {
            gameObject.SetActive(false);
        }
    }

    // Desativa a bala se ela sair da visão da câmera
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
