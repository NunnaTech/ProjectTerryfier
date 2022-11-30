using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetSound : MonoBehaviour
{
    public Collider FeetLeft;
    public Collider FeetRight;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Terrain")
        {
            //Feet.Play();
            
            Debug.Log("Entra left");

        }
		if(FeetRight.gameObject.tag == "Terrain")
        {
            //Feet.Play();
            Debug.Log("Entra left");

        }
		if(FeetRight.gameObject.tag == "Terrain")
        {
            //Feet.Play();
            Debug.Log("Entra left");

        }

    }
}
