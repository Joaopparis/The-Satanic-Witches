using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] float Distance;
    float height = 38.1f;
    bool run = false;

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && !run){
            Distance += .25f;
            run = true;
        }else if(Input.GetKeyUp(KeyCode.LeftShift) && run){
            Distance -= .25f;
            run = false;
        }

        transform.position = new Vector3(Player.position.x + Distance, height, Player.position.z);
    }
}
