using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataPersistence : MonoBehaviour
{
    public int points = 0;
    public int TalkIndex = 0;

    public bool canGuide = true;
    public bool first20Point = true;
    public bool canLeavePit = false;
    public bool Stopped = true;

    public Vector3 PlayerLastPos = new Vector3(0, 0, -6.05f);

    public bool well = false;
    public bool Altar = false;
    public bool MagicCircle = false;

    public List<GameObject> CollectedItens;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
