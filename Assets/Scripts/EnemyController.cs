using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    Vector3 dest;
    Vector3 dist;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;
    BabyController[] babyDino;
    Patrol2[] patrol2;
    Animator animator;
    Patrol2 patrol;

    public PlayerController2 playerController;

    public bool isHidden = true;
    public bool isAttacking = false;
    public float attackTime = 100f;
    float maxTime = 100f;
    public float notSee = 5f;
    Vector3 fwd;
    bool targetHit;
    RaycastHit tHit;
    public Transform mouth;
    public Image hidingEye;
    //public Image eye;
    CharacterStats targetStats;


    void OnTriggerEnter(Collider collision)
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
        patrol = GetComponent<Patrol2>();
        animator = GetComponent<Animator>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        hidingEye = GameObject.Find("HidingEye").GetComponent<Image>();
        playerController = GameObject.Find("unicorn").GetComponent<PlayerController2>();
        targetStats = target.GetComponent<CharacterStats>();
        patrol2 = GetComponentsInChildren<Patrol2>();

    }
    public void SetAttack()
    {
        
        foreach (Patrol2 baby in patrol2)
        {
            baby.SetAttack();
            //Debug.Log(baby.name);
        }

        
    }
    // Update is called once per frame
    public void NotAttack()
    {

        attackTime = 0f;
        //eye.gameObject.SetActive(false);
        isAttacking = false;
        //patrol2 = GetComponentsInChildren<Patrol2>();
        foreach (Patrol2 baby in patrol2)
        {
            baby.NotAttack();
            //Debug.Log(baby.name);
        }
    }
    public void SetDestination(Transform target)
    {
        MomAttack();
        babyDino = GetComponentsInChildren<BabyController>();
        foreach (BabyController baby in babyDino)
        {
            baby.SetDestination(target);
            //Debug.Log(baby.name);
        }
        SetAttack();
        agent.SetDestination(target.position);
    }
    public void Idle()
    {
        agent.SetDestination(transform.position);
    }
    public float Distance()
    {
        return Vector3.Distance(target.position, transform.position);
    }

    void Update()
    {
        
        if (isAttacking)
        {
            if (!agent.hasPath)
            {
                //patrol.GotoNextPoint();
                patrol.AttackNextPoint(target.position);
            }
            attackTime -= 0.15f;
            if (attackTime <= 0f)
            {
                
                NotAttack();
                
            }
            hidingEye.fillAmount = attackTime / maxTime;
            //Debug.Log(attackTime / maxTime);
        }
        else
        {
           // attackTime = 0f;
        }
        //fwd = transform.TransformDirection(Vector3.forward) * lookRadius * 1.7f;
        fwd = target.position - transform.position;
        dest = transform.position + transform.forward * lookRadius/1.5f;
        dist = transform.position + transform.forward * lookRadius * 1.7f;


        float distance = Vector3.Distance(target.position, dest);
        float distance2 = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius || isAttacking)
        {
            //Idle();
            //FaceTarget();
            //agent.SetDestination(agent.destination);
            bool hiding = playerController.CheckHiding();
            if (!hiding)
            {
                targetHit = Physics.Raycast(transform.position, fwd, out tHit, lookRadius * 1.7f);
                //targetHit = Physics.BoxCast(transform.position, transform.localScale*3, fwd, out tHit, transform.rotation, lookRadius * 1.7f);
                if (targetHit)
                {
                    // Debug.Log(tHit.transform.name);
                    if (tHit.transform == target.transform)
                    {
                        MomAttack();
                        if (notSee >= 5f)
                        {
                            patrol.Attack2();
                            notSee = 0f;
                        }

                    }
                    else
                    {
                        notSee += 0.1f;
                        isHidden = true;
                        //Debug.Log(tHit.transform.name);
                        //isAttacking = false;
                    }
                }
                if (distance2 <= agent.stoppingDistance)
                {
                    
                    if (targetStats != null)
                    {
                        animator.SetBool("eating", true);
                        combat.Attack(targetStats);
                    }

                    FaceTarget();
                }
                else
                {
                    animator.SetBool("eating", false);
                }
                if (isAttacking)
                {
                    if (attackTime <= 0f)
                    {
                        patrol.AttackNextPoint(target.position);
                       // attackTime = 100f;
                    }

                    //agent.SetDestination(target.position);
                }
                if (notSee >= 100f)
                {
                    notSee = 0f;
                    NotAttack();
                    patrol.NotAttack();
                    patrol.GotoNextPoint();
                   
                }
            }
            else if (distance2 <= agent.stoppingDistance)
            {
                MomAttack();
                /*
                notSee = 0f;
                if (isAttacking)
                {
                    if (attackTime <= 30f)
                    {
                        isAttacking = false;
                        NotAttack();
                        patrol.AttackNextPoint(target.position);
                        attackTime = 0f;
                    }
                    else if (attackTime == 0f && notSee == 0f)
                    {
                        patrol.NotAttack();
                    }

                    //agent.SetDestination(target.position);
                }
                */
            }
        }
    }public void MomAttack()
    {
        isHidden = false;
        isAttacking = true;

        agent.SetDestination(target.position);
        attackTime = 100f;
       // eye.gameObject.SetActive(true);
        //SetDestination(target);
    }
    void FaceTarget ()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
    void OnDrawGizmosSelected ()
    {
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, dest);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(dest, lookRadius);
    }
}
