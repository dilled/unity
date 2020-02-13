using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendPatrol : MonoBehaviour
{
    public bool hasPath;
    public float patrolRadius = 10f;
    Animator animator;
    
    NavMeshAgent agent;
    public float idleDelay = 6f;
    public float idle;

    public AudioSource audioSource;
    public AudioClip stepAudio;
    public AudioClip attackAudio;
    public AudioClip attack2Audio;

    Vector3 awayPoint;
    Vector3 someRandomPoint;
    float distance2;

    Vector3 startPoint;
    public bool validPath;
    NavMeshPath path;
    
    public bool isAttacking = false;
    public bool friends = false;
    public bool goHome = false;
    public bool goScuba = false;
    public bool affraid = false;
    public bool pathSetted = false;
    public bool tooFar = false;
    public bool onQuest = false;
    Vector3 dest;
    Quests quests;
    public Transform homeDef;

    public void StepAudioFriend()
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
        quests = GetComponent<Quests>();
        path = new NavMeshPath();
        startPoint = gameObject.transform.position;
       
        
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = true;
        idle = idleDelay;
        GotoNextPoint();
        

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
           
        }
       

    }
    public void GoHome()
    {
        if (!goHome)
        {
            startPoint = homeDef.position;
            agent.SetDestination(homeDef.position);
            goHome = true;
          //  Debug.Log("gohome");
        }
    }
    public void SetFriends()
    {
        friends = !friends;
    }
    public void SetDestination(Transform target)
    {
        //Debug.Log(target.position);
        if (agent != null)
        {
            agent.SetDestination(target.position);
           // pathSetted = false;
        }
       
    }
    public void GotoNextPoint()
    {
      //  Debug.Log("gotonext");
        StartCoroutine("GotoNext");
        StartCoroutine("MoveNext");
       
    }
    public void MoveAround(Vector3 startPoint)
    {

        someRandomPoint = startPoint + Random.insideUnitSphere * patrolRadius / 2;
        validPath = agent.CalculatePath(someRandomPoint, path);


        


        // Set the agent to go to the currently selected destination.
        agent.destination = someRandomPoint;

        // Choose the next point in the array as the destination,

    }
    public void MoveAway(Vector3 enemyPoint)
    {
        dest = transform.position - transform.forward * patrolRadius ;
        someRandomPoint = dest + Random.insideUnitSphere * patrolRadius ;
        validPath = agent.CalculatePath(someRandomPoint, path);
        float distance2enemy = Vector3.Distance(someRandomPoint, enemyPoint);
        float distance2away = Vector3.Distance(transform.position, someRandomPoint);

        //while (!validPath || distance2enemy <= patrolRadius || distance2away >= patrolRadius*2.1f)

        //Debug.Log("moveaway");
        awayPoint = someRandomPoint;
        // Set the agent to go to the currently selected destination.
        agent.destination = someRandomPoint;
        pathSetted = false;
        // Choose the next point in the array as the destination,

    }
    public void Warp(Transform warp)
    {
        agent.Warp(warp.position);
    }

    public void SetOnQuest()
    {
        
        onQuest = !onQuest;
      //  Debug.Log("quest" + onQuest);
    }
    public void SetOnQuestFalse()
    {

        onQuest = false;
        //Debug.Log("quest" + onQuest);
    }
    public void SetAffraid()
    {
        affraid = !affraid;
    }
    public void SetTooFar(bool far)
    {
        tooFar = far;
    }
    public void NotAttack()
    {
        isAttacking = false;
    }
    public void ContinueQuest()
    {
        pathSetted = true;
        agent.SetDestination(quests.CurrentQuest().transform.position);
        //Debug.Log("continue quest");
    }
    void Update()
    {
        hasPath = agent.hasPath;
        if (onQuest)
        {
            if (!affraid)
            {
                if (!pathSetted)
                {
                    if (!tooFar)
                        {
                        if (quests.CurrentQuest() != null)
                        {
                            pathSetted = true;
                            agent.SetDestination(quests.CurrentQuest().transform.position);
                         //   Debug.Log("quest");
                        }
                        }
                        
                    
                }
            }
        }
        else if (!affraid)
        {

            if (!agent.hasPath && !pathSetted && !onQuest)
            {
                GotoNextPoint();
                //Debug.Log("haspath");
            }
            if (agent.remainingDistance <= agent.stoppingDistance && !onQuest)
            {
                bool idler = ((agent.velocity.magnitude == 0) ? true : false);
                animator.SetBool("idle", idler);
                idle -= 0.2f;
                if (idler && goHome)
                {
                    GoHome();
                }
                //animator.SetBool("eating", true);
                if (idle <= 0)
                {
                    //Debug.Log("idle");
                    pathSetted = false;
                    if (!friends && !pathSetted && !onQuest)
                    {
                      //  Debug.Log("idle");
                        GotoNextPoint();
                        idle = idleDelay;
                        //animator.SetBool("idle", false);
                    }
                }
                // GotoNextPoint();

            }
            if (agent.speed > 0f || idle <= 0)
            {
                //animator.SetBool("idle", false);
            }
            // Choose the next destination point when the agent gets
            // close to the current one.
            //Debug.Log(idle);
            //Debug.Log(agent.remainingDistance);
            /*
            if (!agent.pathPending)
            {
                GotoNextPoint();
                if (agent.remainingDistance <= agent.stoppingDistance)
                {

                }
            }*/
        }

    }
    void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(awayPoint, 3f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(someRandomPoint, 2f);
    }

    public IEnumerator GotoNext()
    {
        int limiter = 0, loopCap = 10000;
        someRandomPoint = startPoint + Random.insideUnitSphere * patrolRadius;
        distance2 = Vector3.Distance(someRandomPoint, transform.position);
        someRandomPoint = new Vector3(someRandomPoint.x, transform.position.y, someRandomPoint.z);
        while (distance2 < patrolRadius / 2 && !validPath && limiter < loopCap)
        {
            limiter++;
            //someRandomPoint = startPoint + Random.insideUnitSphere * patrolRadius;
            // someRandomPoint.y = transform.position.y;
            validPath = agent.CalculatePath(someRandomPoint, path);
            distance2 = Vector3.Distance(someRandomPoint, transform.position);
        }

        if (limiter >= loopCap) Debug.Log("Infinite Loop avoided");

        FaceTarget(someRandomPoint);

        
        yield return new WaitForSeconds(3f);
        

    }
    public IEnumerator MoveNext()
    {
        
        agent.destination = someRandomPoint;
        yield return new WaitForSeconds(3f);
        
    }
}