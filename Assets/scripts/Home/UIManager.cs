using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject sheet;

    public GameObject btnBuild;
    [SerializeField] GameObject Shop;
    [SerializeField] GameObject guide;
    [SerializeField] GameObject Indicator;
    [SerializeField] GameObject NextBtn;

    [SerializeField] Text guideTalk;

    dataPersistence data;

    string[] Talk = { 
        "Hey! It's looks like you want to be a powerful Witch", 
        "good that you invoke me, I can help u",
        "Fist, let get some coins",
        "Now, Let's buy a well to find some items",
        "Let's enter on the sewage and see what we can get",
        ""
    };

    void Start()
    {
        data = GameObject.FindGameObjectWithTag("dataPersistence").GetComponent<dataPersistence>();
    }

    void Update()
    {
        if(Time.time >= 1.5f && data.canGuide)
        {
            openSheet();
            openGuide();

            guideTalk.text = Talk[data.TalkIndex];
            data.TalkIndex++;

            data.canGuide = false;
        }
    }

    public void nextTalk()
    {
        guideTalk.text = Talk[data.TalkIndex];
        data.TalkIndex++;

        if (data.TalkIndex == 4 || data.TalkIndex == 5 || data.TalkIndex == 6)
        {
            closeSheet();
            closeGuide();
        }
    }

    public void openGuide()
    {
        guide.SetActive(true);

        if (data.TalkIndex == 4)
        {
            btnBuild.SetActive(true);
            btnBuild.transform.SetAsLastSibling();
            NextBtn.SetActive(false);
        }
        else
        {
            btnBuild.transform.SetAsFirstSibling();
            NextBtn.SetActive(true);
        }
    }

    public void closeGuide()
    {
        guide.SetActive(false);
    }

    public void openIndicator()
    {
        Indicator.SetActive(true);
    }

    public void openSheet()
    {
        sheet.SetActive(true);
    }

    public void closeSheet()
    {
        sheet.SetActive(false);
    }

    public void openShop()
    {
        if (Indicator != null)
        {
            Destroy(Indicator);
            nextTalk();
            openSheet();
        }

        Shop.SetActive(true);
        btnBuild.SetActive(false);
    }

    public void closeShop()
    {
        Shop.SetActive(false);
        btnBuild.SetActive(true);
    }
}
