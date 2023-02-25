using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text txtPoints;

    [SerializeField] GameObject UI;
    [SerializeField] GameObject txtPointCount;
    [SerializeField] Text textOfPointCount;

    [SerializeField] GameObject RemovableTrees;
    [SerializeField] GameObject Well;

    [SerializeField] GameObject RemovableTrees2;
    [SerializeField] GameObject Altar;
    [SerializeField] GameObject RemovableTrees3;
    [SerializeField] GameObject MagicCircle;

    UIManager uIManager;
    dataPersistence data;

    public bool onAltar = false;

    void Start()
    {
        data = GameObject.FindGameObjectWithTag("dataPersistence").GetComponent<dataPersistence>();

        Cursor.visible = true;
        uIManager = UI.GetComponent<UIManager>();

        txtPoints.text = Convert.ToString(data.points) + "x";
        purchases();
    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && !onAltar))
        {
            data.points++;
            txtPoints.text = Convert.ToString(data.points) + "x";
            PointCount();

        }

        if(data.points == 20 && data.first20Point)
        {
            uIManager.openSheet();
            uIManager.openGuide();
            uIManager.openIndicator();
            data.first20Point = false;
        }
    }

    void PointCount()
    {
        textOfPointCount.text = "+" + Convert.ToString(1);

        GameObject _Object = Instantiate(txtPointCount, Input.mousePosition, txtPointCount.transform.rotation) as GameObject;
        _Object.transform.SetParent(UI.transform);
        StartCoroutine(removePointUI(_Object));
    }

    public void BuyWell()
    {
        if(data.points >= 20)
        {
            data.points = data.points - 20;
            txtPoints.text = Convert.ToString(data.points) + "x";

            Destroy(RemovableTrees);
            Well.SetActive(true);

            data.well = true;
            data.canLeavePit = true;

            uIManager.openSheet();
            uIManager.openGuide();
        }
    }

    public void buyAltar(){
        if(data.points >= 50){
            data.points = data.points - 50;
            txtPoints.text = Convert.ToString(data.points) + "x";

            Destroy(RemovableTrees2);
            Altar.SetActive(true);

            data.Altar = true;

            uIManager.closeSheet();
        }
    }

    public void buyMagicCircle(){
        if(data.points >= 60){
            data.points = data.points - 60;
            txtPoints.text = Convert.ToString(data.points) + "x";

            Destroy(RemovableTrees3);
            MagicCircle.SetActive(true);

            data.MagicCircle = true;

            uIManager.closeSheet();
        }
    }

    void purchases()
    {
        if(data.TalkIndex >= 4){
            uIManager.btnBuild.SetActive(true);
        }

        if (data.well)
        {
            Destroy(RemovableTrees);
            Well.SetActive(true);
        }

        if(data.Altar){
            Destroy(RemovableTrees2);
            Altar.SetActive(true);
        }

        if(data.MagicCircle){
            Destroy(RemovableTrees3);
            MagicCircle.SetActive(true);
        }
    }

    IEnumerator removePointUI(GameObject _Object)
    {
        yield return new WaitForSeconds(.5f);
        Destroy(_Object);
    }
}
