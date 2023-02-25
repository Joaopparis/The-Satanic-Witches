using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Witch : MonoBehaviour
{
    Animator _anim;
    Rigidbody rb;
    GameObject _keyE;

    float TimeAllowed = 0, TimeRange;
    bool allowIndicator = true;

    [SerializeField] float speed;

    [SerializeField] GameObject pit;
    [SerializeField] GameObject well;
    [SerializeField] GameObject Altar, Altar_Absorv;
    [SerializeField] GameObject MagicCircle;
    [SerializeField] GameObject Indicator_KeyE;
    [SerializeField] Animator camAnim;

    dataPersistence data;
    GameManager gameManager;
    UIManager uIManager;

    bool isColliding = false;

    void Start()
    {
        _anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        TimeRange = Random.Range(10, 20);
        data = GameObject.FindGameObjectWithTag("dataPersistence").GetComponent<dataPersistence>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        transform.position = data.PlayerLastPos;
    }

    void Update()
    {
        if(TimeAllowed <= Time.time && data.Stopped)
        {
            _anim.SetTrigger("kneeling");
            TimeRange = Random.Range(10, 20);
            TimeAllowed = Time.time + TimeRange;
        }

        if (data.canLeavePit)
        {
            IndicatorShow(pit, well, Altar, MagicCircle);
        }

        if(!isColliding){
            move();
        }
    }

    void move()
    {
        if (!data.Stopped)
        {
            float Horizontal = Input.GetAxis("Horizontal");
            float Vertical = Input.GetAxis("Vertical");

            Vector3 Inputs = new Vector3(-Vertical, 0f, Horizontal);
            transform.LookAt(transform.position + new Vector3(Inputs.x, 0, Inputs.z));
            rb.velocity = Inputs * speed;
            _anim.SetFloat("walking", Mathf.Floor(rb.velocity.magnitude));
        }
    }

    void IndicatorShow(GameObject _object1, GameObject _object2, GameObject _object3, GameObject _object4)
    {
        if (Vector3.Distance(transform.position, _object1.transform.position) <= 5.3f)
        {
            enableIndicator(_object1, 3.69f);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (data.Stopped)
                {
                    _anim.ResetTrigger("kneeling");
                    _anim.SetTrigger("stopKneeling");
                    data.Stopped = false;
                }
                else
                {
                    _anim.SetFloat("walking", 0);
                    transform.LookAt(pit.transform.position);
                    data.Stopped = true;
                }
            }
        }
        else if (Vector3.Distance(transform.position, _object2.transform.position) <= 9.3f)
        {
            enableIndicator(_object2, 7.46f);

            if (Input.GetKeyDown(KeyCode.E))
            {
                data.PlayerLastPos = transform.position;
                SceneManager.LoadScene("Dungeons");
            }
        }
        else if (Vector3.Distance(transform.position, _object3.transform.position) <= 7.3f && data.Altar)
        {
            if (!data.Stopped)
            {
                enableIndicator(_object3, 5.46f);
                
                if(Input.GetKeyDown(KeyCode.E)){
                    gameManager.onAltar = true;
                    uIManager.btnBuild.SetActive(false);
                    Altar_Absorv.SetActive(true);

                    gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    gameObject.transform.GetChild(2).gameObject.SetActive(false);

                    data.Stopped = true;
                    camAnim.SetBool("Altar", true);
                }
            }
            else{
                if(Input.GetKeyDown(KeyCode.E)){
                    gameManager.onAltar = false;
                    uIManager.btnBuild.SetActive(true);
                    Altar_Absorv.SetActive(false);

                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(2).gameObject.SetActive(true);

                    _anim.ResetTrigger("kneeling");
                    _anim.SetTrigger("stopKneeling");

                    data.Stopped = false;
                    camAnim.SetBool("Altar", false);
                }
            }
        }
        else if (Vector3.Distance(transform.position, _object4.transform.position) <= 12.3f && data.MagicCircle)
        {
            enableIndicator(_object4, 2.46f);
        }
        else
        {
            Destroy(_keyE);
            allowIndicator = true;
        }
    }

    void enableIndicator(GameObject _object, float height)
    {
        if (allowIndicator)
        {
            _keyE = Instantiate(Indicator_KeyE, new Vector3(_object.transform.position.x, _object.transform.position.y + height, _object.transform.position.z), Indicator_KeyE.transform.rotation);
            allowIndicator = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(IndicatorRange());
        }
    }

    IEnumerator IndicatorRange()
    {
        Destroy(_keyE);
        yield return new WaitForSeconds(.7f);
        allowIndicator = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "pit" || collision.gameObject.tag == "well")
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "pit" || collision.gameObject.tag == "well")
        {
            rb.isKinematic = false;
            isColliding = false;
        }
    }
}
