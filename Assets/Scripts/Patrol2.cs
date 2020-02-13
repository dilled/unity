using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol2 : MonoBehaviour
{
    public float patrolRadius = 10f;
    public Transform ukko;
    public Transform mouth;
    Animator animator;
    
    
    private NavMeshAgent agent;
    public float idleDelay = 6f;
    public float idle;

    public AudioSource audioSource;
    public AudioClip stepAudio;
    public AudioClip attackAudio;
    public AudioClip attack2Audio;

    Vector3 startPoint;
    public bool validPath;
    NavMeshPath path;
    public GameObject[] babyDinos;
    public GameObject babies;
    public bool isAttacking = false;
    GameObject cam;
    Vector3 someRandomPoint;

    public void Step()
    {

        audioSource.PlayOneShot(stepAudio, 1f);

    }
    public void AttackSound()
    {

        audioSource.PlayOneShot(attackAudio, 1f);

    }
    public void Attack2()
    {
        if (attack2Audio != null)
        audioSource.PlayOneShot(attack2Audio, 1f);

    }

    void Start()
    {
       // Debug.Log("patrol2 start");
        path = new NavMeshPath();
        startPoint = gameObject.transform.position;
        
        ukko = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = true;
        idle = idleDelay;
        GotoNextPoint();
        PutBabys();
        cam = GameObject.Find("OVRCameraRig");
        

    }
    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("eat " + collision.gameObject.name);
        if (collision.transform.tag == "Player")
        {

            //Debug.Log("eat " + collision.gameObject.name);
            //collision.transform.position = mouth.transform.position;
            // animator.SetBool("eating", false);
            Debug.Log("eat " + collision.gameObject.name);
            ukko.transform.position = mouth.transform.position;
            Debug.Log("fire " + ukko.position);
        }
       

    }
    void PutBabys(){
        int limiter = 0, loopCap = 10000;
        foreach (var baby in babyDinos)
        {
            someRandomPoint = startPoint + Random.insideUnitSphere * patrolRadius;
            validPath = agent.CalculatePath(someRandomPoint, path);
            if (!validPath) //Debug.Log("Found an invalid Path");
    
            while (!validPath && limiter < loopCap)
            {
                    limiter++;
                    someRandomPoint = startPoint + Random.insideUnitSphere * patrolRadius;
                validPath = agent.CalculatePath(someRandomPoint, path);
            }
            GameObject Babys = Instantiate(baby, someRandomPoint, Quaternion.identity);
            Babys.transform.SetParent(babies.transform);
            if (limiter >= loopCap) Debug.Log("Infinite Loop avoided");
        }
        
    }

    public void GotoNextPoint()
    {
        if (!isAttacking){
        someRandomPoint = startPoint + Random.insideUnitSphere * patrolRadius;
        validPath = agent.CalculatePath(someRandomPoint, path);

            int limiter = 0, loopCap = 10000;
            while (!validPath && limiter < loopCap)
            {
                limiter++;
                someRandomPoint = startPoint + Random.insideUnitSphere * patrolRadius;
            validPath = agent.CalculatePath(someRandomPoint, path);
        }

            if (limiter >= loopCap) Debug.Log("Infinite Loop avoided");
            // Set the agent to go to the currently selected destination.
            agent.destination = someRandomPoint;
        }
        // Choose the next point in the array as the destination,
        
    }
    public void AttackNextPoint(Vector3 startPoint)
    {
        
            someRandomPoint = startPoint + Random.insideUnitSphere * patrolRadius/2;
            validPath = agent.CalculatePath(someRandomPoint, path);

        /*
            while (!validPath)
            {

                someRandomPoint = startPoint + Random.insideUnitSphere * patrolRadius/2;
                validPath = agent.CalculatePath(someRandomPoint, path);
            }
            */

            // Set the agent to go to the currently selected destination.
            agent.destination = someRandomPoint;
        
        // Choose the next point in the array as the destination,

    }
    public void SetAttack(){
        isAttacking = true;
        cam.GetComponent<ThirdPersonCamera>().SetUnderAttack();
    }
    public void NotAttack()
    {
        isAttacking = false;
        cam.GetComponent<ThirdPersonCamera>().SetNotUnderAttack();
    }

    void Update()
    {
        float distance = Vector3.Distance(startPoint, transform.position);
        //Debug.Log(distance);
        if (distance > patrolRadius * 4f)
        {
            agent.destination = startPoint;
            //isAttacking = false;
        }

        // Choose the next destination point when the agent gets
        // close to the current one.
        //Debug.Log(idle);
        //Debug.Log(agent.remainingDistance);
        if (!agent.pathPending && agent.remainingDistance < 3.2f)
        {
            idle -= 0.2f;
            //animator.SetBool("eating", true);
            if (idle <= 0)
            {
                //animator.SetBool("eating", false);
                isAttacking = false;
                GotoNextPoint();
                idle = idleDelay;
            }

        }
        if (!agent.hasPath)
        {
            GotoNextPoint();
        }

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }
}