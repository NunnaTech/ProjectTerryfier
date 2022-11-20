using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetSound : MonoBehaviour
{
    public AudioSource Feet;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Terrain")
        {
            Feet.Play();
        }
    }
}
