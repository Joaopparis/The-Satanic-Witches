using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State{
        patrol,
        attack,
        die
    };

    [SerializeField]State currentState = State.patrol;
    [SerializeField]enemyArea EnemyArea;

    public Transform[] waypoints;
    public Transform player;
    public int life;
    int currentWaypoint = 0;
    [SerializeField]float speed = 2f;
    float waitTime;
    float waitCounter = 0f;
    float hostTime = 4.25f, hostCounter = 0f;
    float attackCounter = 0f;
    bool waiting = false;
    bool firstHostility = false;
    Animator anim;
    bool die = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        waitTime = Random.Range(4f, 7f);
        currentWaypoint = Random.Range(0, waypoints.Length);
    }

    void Update()
    {
        if(life <= 0 && !die){
            currentState = State.die;
            die = true;
        }else if(life > 0){
            if(Vector3.Distance(transform.position, player.position) <= 20f && EnemyArea.onArea){
                currentState = State.attack;
            }
            else{
                anim.SetBool("Run", false);
                hostCounter = 0f;
                firstHostility = false;
                currentState = State.patrol;
            }

        }
        
        switch(currentState){
            case State.patrol:
                Patrol();
                break;
            case State.attack:
                attack();
                break;
            case State.die:
                anim.SetBool("Die", true);
                break;
        }
    }

    void Patrol(){
        if(waiting){
            waitCounter += Time.deltaTime;
            if(waitCounter < waitTime) return;

            waitTime = Random.Range(3f, 5f);
            waiting = false;
        }

        Transform wp = waypoints[currentWaypoint];

        if(Vector3.Distance(gameObject.transform.position, wp.position) < 0.01f){
            anim.SetBool("Run", false);
            transform.position = wp.position;
            waitCounter = 0f;
            waiting = true;
            currentWaypoint = Random.Range(0, waypoints.Length);
        }else{
            Quaternion targetRotation = Quaternion.LookRotation(wp.position - transform.position);

            transform.position = Vector3.MoveTowards(transform.position, wp.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            anim.SetBool("Run", true);
        }
    }

    void attack(){
        if(!firstHostility){
             anim.SetBool("Run", false);
            anim.SetTrigger("Hostility");
            Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (speed + 1f) * Time.deltaTime);

            hostCounter += Time.deltaTime;
            if(hostCounter < hostTime) return;

            firstHostility = true;
        }

        if(firstHostility && Vector3.Distance(transform.position, player.position) > 4.75f)
        {
            anim.SetBool("Run", true);
            Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);

            transform.position = Vector3.MoveTowards(transform.position, player.position, (speed + 1f) * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (speed + 1f) * Time.deltaTime);
        }else if(Vector3.Distance(transform.position, player.position) <= 4.75f){
            anim.SetBool("Run", false);

            attackCounter += Time.deltaTime;

            if(attackCounter > 3.5f){
                anim.SetTrigger("Attack");
                player.GetComponent<Player>().removeLife(20);
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
