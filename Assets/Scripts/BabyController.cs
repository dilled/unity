using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BabyController : MonoBehaviour
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
    Patrol2 patrol;
    RedEye redEye;
    public bool sleeping;
    public float sleepTime = 0f;
    public float timeToSleep = 25f;
    EnemyStats myStats;
    CharacterStats targetStats;
    PlayerController2 pcont;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("eat " + collision.gameObject.name);
        if (collision.transform.tag == "Player")
        {

            Debug.Log("eat " + collision.gameObject.name);
            //collision.transform.position = mouth.transform.position;
            // animator.SetBool("eating", false);

        }

    }

    void Start()
    {
        pcont = GameObject.Find("unicorn").GetComponent<PlayerController2>();
        myStats = GetComponent<EnemyStats>();
        patrol = GetComponent<Patrol2>();
        momDino = GetComponentInParent<EnemyController>();
        animator = GetComponent<Animator>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        redEye = GetComponent<RedEye>();
        //babyDino = GetComponent<EnemyController>();
        targetStats = target.GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    
    public void SetDestination(Transform target)
    {
        //Debug.Log(target.position);
        if (agent != null)
        {
            agent.SetDestination(target.position);
        }
    }
    public void SetSleeping()
    {
        agent.isStopped = true;
        sleeping = true;
        patrol.NotAttack();
    }
    void Update()
    {
        fwd = target.position - transform.position;
        dest = transform.position + transform.forward * lookRadius / 1.5f;
        dist = transform.position + transform.forward * lookRadius * 1.7f;
        //agent.velocity = 3.5f;

        float distance = Vector3.Distance(target.position, dest);
        float distance2 = Vector3.Distance(target.position, transform.position);
        if (!sleeping)
        {
            if (distance2 < 30f)
            {
                if (!sleeping)
                {
                    pcont.CheckEnemies();
                }
            }
            if (distance <= lookRadius)
            {
                //FaceTarget();
                targetHit = Physics.Raycast(transform.position, fwd, out tHit, lookRadius * 1.7f);

                if (targetHit)
                {

                    if (tHit.transform == target.transform)
                    {
                        //agent.SetDestination(target.position);

                        redEye.AttackEye();
                        isAttacking = true;
                        agent.speed = 7f;
                        xSpeed = 1f;
                        if (momDino != null)
                        {
                            momDino.SetDestination(target);
                        }
                        else
                        {
                            patrol.SetAttack();
                            patrol.AttackNextPoint(target.position);
                            SetDestination(target);
                        }
                    }

                }

                if (distance2 <= agent.stoppingDistance)
                {
                    
                    if (targetStats != null)
                    {
                        animator.SetBool("attack", true);
                        combat.Attack(targetStats);
                    }

                    FaceTarget();
                }
                else
                {
                    animator.SetBool("attack", false);
                }
            }
            else
            {
                if (isAttacking)
                {
                    patrol.NotAttack();
                    redEye.NotAttackEye();
                    isAttacking = false;
                }
                agent.speed = 1.28f;
                xSpeed = 0.5f;
            }
            // Debug.Log("vel " + agent.velocity.magnitude);
            float speedPercent = agent.velocity.magnitude / agent.speed * xSpeed;
            animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
        }
        else
        {
            sleepTime += .01f;
            isAttacking = false;
            animator.SetBool("attack", false);
            animator.SetBool("sleeping", true);

            if (sleepTime > timeToSleep)
            {
                animator.SetBool("sleeping", false);
                sleeping = false;
                sleepTime = 0;
                myStats.Born();
                agent.isStopped = false;
            }
        }
        
    }
    public bool CheckSleeping()
    {
        return sleeping;
    }
    void FaceTarget ()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, dist);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, dest);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(dest, lookRadius);
    }
}
