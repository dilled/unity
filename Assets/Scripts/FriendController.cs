using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FriendController : MonoBehaviour
{
    public bool isAttacking = false;
    public float lookRadius = 10f;
    Vector3 dest;
    Vector3 dist;

    Vector3 fwd;
    bool targetHit;
    RaycastHit tHit;

    public Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;

    Animator animator;
    EnemyController momDino;
    const float locomotionAnimationSmoothTime = .1f;
    float xSpeed = 0.5f;
    FriendPatrol patrol;
    bool flosh = false;
    bool sleep = false;
    public bool friends = false;
    public bool affraid = false;
    Transform enemy;
    public float affraidCount = 40f;
    float defAffraidCount;
    public bool onQuest = false;
    float distance2Player;
    Quests quests;
    public GameObject speechBubble;
    public Image quest;
    GameObject playerState;
    public bool goHome = false;
    bool idler = false;
    bool tooFar;
    public GameObject homeDef;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Reward")
        {
           //patrol.GoHome();
            
            patrol.SetOnQuestFalse();
            onQuest = false;
        
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.tag == "Player")
        {

            Debug.Log("eat " + collision.gameObject.name);
            //collision.transform.position = mouth.transform.position;
            // animator.SetBool("eating", false);
            
        }

    }

    void Start()
    {
        quests = GetComponent<Quests>();
        defAffraidCount = affraidCount;
        patrol = GetComponent<FriendPatrol>();
        momDino = GetComponentInParent<EnemyController>();
        animator = GetComponent<Animator>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();

       // agent.Warp(PlayerManager.instance.startPos.position);

        playerState = GameObject.Find("PlayerState");
        if (playerState != null)
        {
            quests.currentQuest = playerState.GetComponent<Player>().currentQuest;
            if (quests.currentQuest > 0)
            {
                agent.Warp(playerState.GetComponent<Player>().startPosition);
                patrol.SetOnQuest();
                onQuest = true;
                quest.gameObject.SetActive(false);
                goHome = true;
                patrol.goHome = true;
            }
        }
         
    }

    // Update is called once per frame
    public void StartQuest()
    {
        homeDef.SetActive(false);
        Debug.Log("NEXTQUEST");
        patrol.SetOnQuest();
        onQuest = true;
        quests.NextQuest();
        speechBubble.SetActive(true);
        quest.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (!goHome)
        {
            if(idler && tooFar)
            {
                patrol.SetDestination(target);
            }
            if (onQuest)
            {
                distance2Player = Vector3.Distance(target.position, transform.position);

                if (distance2Player >= lookRadius * 2f && !tooFar)
                {
                    tooFar = true;
                    patrol.SetTooFar(true);
                    patrol.SetDestination(target);
                    //Debug.Log("too far");
                }

                if (distance2Player <= lookRadius && tooFar)
                {
                    tooFar = false;
                    patrol.SetTooFar(false);
                    patrol.ContinueQuest();
                }

            }
            if (affraid)
            {
                if (enemy != null)
                {
                    float distance2enemy = Vector3.Distance(transform.position, enemy.position);
                    agent.speed = 7f;
                    xSpeed = 1f;
                    if (distance2enemy <= lookRadius && agent.remainingDistance <= agent.stoppingDistance)
                    {/*
                    patrol.MoveAway(enemy.position);
                    affraidCount = defAffraidCount;
                */
                    }
                    else
                    {
                        affraidCount -= 0.1f;
                    }
                    if (affraidCount <= 0f)
                    {
                        affraidCount = defAffraidCount;
                        affraid = false;
                        patrol.SetAffraid();
                    }
                }
                else
                {
                    affraidCount = defAffraidCount;
                    affraid = false;
                    patrol.SetAffraid();
                }
            }
            if (!friends && flosh)
            {
                friends = true;
                GetComponent<FriendPatrol>().SetFriends();
            }
            if (agent.remainingDistance >= 30f)
            {
                agent.speed = 7f;
                xSpeed = 1f;
            }
            animator.SetBool("sleep", sleep);
            animator.SetBool("flosh", flosh);

            fwd = target.position - transform.position;
            dest = transform.position + transform.forward * lookRadius / 1.5f;
            dist = transform.position + transform.forward * lookRadius * 1.7f;
            //agent.velocity = 3.5f;

            float distance = Vector3.Distance(target.position, dest);
            distance2Player = Vector3.Distance(target.position, transform.position);

            if (distance2Player <= agent.stoppingDistance * 1.5f)
            {

                flosh = target.GetComponent<PlayerController2>().flosh;
                sleep = target.GetComponent<PlayerController2>().sleep;
                if (flosh || sleep)
                {
                    agent.isStopped = true;
                }
                else
                {
                    agent.isStopped = false;
                }
            }
            targetHit = Physics.SphereCast(transform.position, lookRadius, transform.forward, out tHit, lookRadius);

            if (targetHit)
            {


                if (tHit.transform.tag == "Enemy")
                {
                    if (tHit.transform.GetComponent<BabyController>() != null)
                    {
                        if (!tHit.transform.GetComponent<BabyController>().CheckSleeping())
                        {
                            //agent.SetDestination(target.position);
                            if (!affraid)
                            {
                                patrol.SetAffraid();
                            }
                            affraid = true;
                            //Debug.Log("friend sees " + tHit.transform.name);
                            //FaceAway();
                            enemy = tHit.transform;
                            patrol.MoveAway(enemy.position);
                        }
                    }
                }

            }
            if (distance2Player <= lookRadius)
            {
                if (!onQuest)
                {
                    if (quests.quests.Length-1 > quests.currentQuest)
                    {
                        quest.gameObject.SetActive(true);
                    }
                }
                //FaceTarget();
                targetHit = Physics.Raycast(transform.position, transform.forward, out tHit, lookRadius);

                if (targetHit)
                {


                    if (tHit.transform.tag == "Player" && !onQuest)
                    {/*
                    Debug.Log("NEXTQUEST");
                    patrol.SetOnQuest();
                    onQuest = true;
                    quests.NextQuest();
                    speechBubble.SetActive(true);
                   */

                        if (friends)
                        {
                            //patrol.MoveAround(target.position);
                            patrol.SetDestination(target);
                        }
                    }
                    else
                    {

                    }

                }
                if (friends)
                {
                    //patrol.MoveAround(target.position);
                    patrol.SetDestination(target);
                }
                if (distance2Player <= agent.stoppingDistance)
                {

                    FaceTarget();
                }

            }
            else
            {
                quest.gameObject.SetActive(false);
                if (agent.remainingDistance <= 10f && !affraid)
                {
                    agent.speed = 1.28f;
                    xSpeed = 0.3f;
                }
            }
        }
        else
        {
            homeDef.SetActive(true);
            if (agent.remainingDistance >= 30f)
            {
                agent.speed = 7f;
                xSpeed = 1f;
            }
            if (agent.remainingDistance <= 10f)
            {
                agent.speed = 1.28f;
                xSpeed = 0.3f;
            }
        }
        // Debug.Log("vel " + agent.velocity.magnitude);
        float speedPercent = agent.velocity.magnitude / agent.speed * xSpeed;
        idler = ((agent.velocity.magnitude == 0) ? true : false);
        animator.SetBool("idle", idler);
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
    void FaceAway()
    {
        Vector3 direction = (transform.position - target.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, dist);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, dest);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(dest, lookRadius);
    }
}
