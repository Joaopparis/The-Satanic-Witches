using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;

    dataPersistence data;

    [SerializeField] GameObject Inventory;
    [SerializeField] GameObject Sheet;

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.FindGameObjectWithTag("dataPersistence").GetComponent<dataPersistence>();

        if(data.CollectedItens != null){
            for(int i = 0; i < data.CollectedItens.Count; i++){
                isFull[i] = true;
                Instantiate(data.CollectedItens[i], slots[i].transform, false); 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(data.canLeavePit){
            if(Input.GetKeyDown(KeyCode.I)){
                if(Inventory.activeSelf){
                    closeInventory();
                }else{
                    Inventory.SetActive(true);
                    Sheet.SetActive(true);
                }
            }
        }
    }

    public void closeInventory(){
        Inventory.SetActive(false);
        Sheet.SetActive(false);
    }
}
