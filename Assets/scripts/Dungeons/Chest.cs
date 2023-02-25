using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject item;
    public GameObject itemIcon;
    
    [SerializeField] InventoryManager Inventory;

    Animator _anim;
    bool _inTrigger = false;
    dataPersistence data;

    void Start()
    {
        _anim = GetComponent<Animator>();
        data = GameObject.FindGameObjectWithTag("dataPersistence").GetComponent<dataPersistence>();
    }

    void Update()
    {
        if (_inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            _anim.SetBool("Open", true);
            GameObject _item = Instantiate(item,new Vector3(
                transform.position.x, 
                item.transform.position.y, 
                transform.position.z
            ),Quaternion.identity);
            _item.transform.SetParent(transform);

            for(int i = 0; i < Inventory.slots.Length; i++){
                if(Inventory.isFull[i] == false){
                    Inventory.isFull[i] = true;
                    data.CollectedItens.Add(itemIcon);
                    Instantiate(itemIcon, Inventory.slots[i].transform, false);
                    StartCoroutine(CollectItem(_item));       
                    break;
                }
            }
        }
    }

    IEnumerator CollectItem(GameObject _item)
    {
        yield return new WaitForSeconds(2f);
        Destroy(_item);
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
