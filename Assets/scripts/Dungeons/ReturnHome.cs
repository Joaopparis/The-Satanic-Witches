using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnHome : MonoBehaviour
{
    bool _inTrigger = false;

    void Update()
    {
        if (_inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Home");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _inTrigger = false;
        }
    }
}
