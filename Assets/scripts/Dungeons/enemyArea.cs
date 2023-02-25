using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyArea : MonoBehaviour
{
    public bool onArea = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            onArea = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            onArea = false;
        }
    }
}
