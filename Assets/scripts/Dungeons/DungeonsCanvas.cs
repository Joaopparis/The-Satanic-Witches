using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonsCanvas : MonoBehaviour
{
    [SerializeField] Slider lifeBar;

    void Start()
    {
        lifeBar.value = Player.life / 100;
    }

    void Update()
    {
        lifeBar.value = Player.life / 100;
    }
}
