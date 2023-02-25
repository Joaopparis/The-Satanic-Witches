using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{

    [SerializeField]TextMesh MagicPoints;

    float Timer;

    public static int Natural;

    void Start()
    {
        MagicPoints.text = "Magic Force: \n" + Natural + "/500";
    }

    void Update()
    {
        Timer += Time.deltaTime;

        if(Timer >= .5f && Natural <= 500){
            Natural += 1;
            MagicPoints.text = "Magic Force: \n " + Natural + "/500";

            Timer = 0;
        }
    }
}
