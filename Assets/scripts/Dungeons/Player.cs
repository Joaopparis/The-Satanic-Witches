using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator _anim;
    Rigidbody rb;

    [SerializeField] float speed;
    public static float life = 100f;

    bool run = false;
    float attackCounter = 0f;
    bool die = false;

    void Start()
    {
        Cursor.visible = false;
        _anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(life <= 0 && !die){
            _anim.SetBool("Die", true);
            die = true;
        }else if(life > 0){
            if(Input.GetKey(KeyCode.LeftShift) && !run){
                speed *= 1.5f;
                run = true;
            }else if(Input.GetKeyUp(KeyCode.LeftShift) && run){
                speed /= 1.5f;
                run = false;
            }

            move();
            attack();
        }
    }


    void move()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 Inputs = new Vector3(-Vertical, 0f, Horizontal);
        transform.LookAt(transform.position + new Vector3(Inputs.x, 0, Inputs.z));
        rb.velocity = Vector3.Normalize(Inputs) * speed;
        _anim.SetFloat("walking", Mathf.Floor(rb.velocity.magnitude));
    }

    void attack(){
        attackCounter += Time.deltaTime;

        RaycastHit hit;
        bool rayHit = Physics.Raycast(transform.position + new Vector3(0, 2.5f ,0), transform.TransformDirection(Vector3.forward), out hit, 4.75f);
        Debug.DrawRay(transform.position + new Vector3(0, 2.5f ,0), transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

        if(attackCounter > 1.5f){
            if(Input.GetKeyDown(KeyCode.Mouse0)){
                _anim.SetTrigger("Attack");

                if(rayHit){
                    if(hit.transform.gameObject.tag == "Enemy"){
                        hit.transform.gameObject.GetComponent<Enemy>().removeLife(10);
                    }
                }

                attackCounter = 0f;
            }
        }
    }

    public void removeLife(int lifeRemoved){
        if(life != 0){
            life -= lifeRemoved;
        }
    }
}
