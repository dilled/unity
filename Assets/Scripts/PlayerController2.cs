using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class PlayerController2 : MonoBehaviour
{
    
    
    public Image floshButton;
    bool trueHiding = false;
    bool hiding = true;
    int hidingCount;
    

    public Interactable focus;
    public bool swimming;
    
    float lasty;

    float speed = 6.0f;
    private Vector3 moveDirection = Vector3.zero;
    public bool lockCursor;
    public float mouseSensitivity = 1;
    public bool fly;
    bool running;
    public float mousey;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    float defWalkSpeed;
    float defRunSpeed;

    bool isGrounded;
    float reJump = 0f;
    public float falling = 0f;
    public float walkSpeed = 3;
    public float waterWalkSpeed = 1f;
    public float flyWalkSpeed = 5f;
    public float runSpeed = 8;
    public float waterRunSpeed = 3f;
    public float flyRunSpeed = 10f;
    public float gravity = -12;
    public float jumpHeight = 1;
    [Range(0, 1)]
    public float airControlPercent;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    public float currentSpeed;
    float velocityY;
    Vector2 input;
   
    public Animator animator;
    Transform cameraT;
    CharacterController controller;
    ImpactReceiver imp;
    CharacterStats myStats;
    CharacterCombat combat;

    CharacterSounds sounds;
   
    float idleTime;
    bool jumping;
    bool teleport = false;
    Transform teleportDestination;
    Transform previousPosition;
    float lastHealth;
    public bool sleep = false;
    public bool flosh = false;
    bool drink = false;
    
    public GameObject enemys;
    Patrol2[] enemies;
    public float falling2 = 0f;
    public float falling2max;
    public float lasty2;
    public GameObject friend;
    GameObject playerState;
    bool jumpCont = false;
    float dist;
    
    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log(other.transform.name);
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            
            SetFocus(interactable);
            
        }
        
        if (other.transform.name == "Grass" || other.transform.name == "Bush")
        {
            hidingCount += 1;
            if (hidingCount > 0)
            {
                trueHiding = true;
                hiding = true;
            }
            //Debug.Log(other.transform.name);
        }
        if (other.transform.name == "WaterBasicDaytime")
        {
            if (falling >= 2f)
            {
                sounds.SplashAudio();
            }
            swimming = true;
                //Debug.Log(other.transform.name);
         
        }
        if (other.transform.name == "DoorWay2")
        {
           // PlayerManager.instance.playerPos = transform;
           // previousPosition = transform;
            //teleportDestination = other.GetComponent<Scenes>().destination;
           // Debug.Log(other.GetComponent<Scenes>().destination.position);
            //Debug.Log("prev" + previousPosition.position);
            //Debug.Log("prev ins" + PlayerManager.instance.playerPos.position);
            //teleport = true;
        }
        if (other.transform.name == "DoorWayExit")
        {
           // Debug.Log("prev" + previousPosition.position);
            teleportDestination = PlayerManager.instance.playerPos;
            teleport = true;
        }

    }
    private void OnTriggerExit (Collider other)
    {
        
        if (other.transform.name == "Grass" || other.transform.name == "Bush")
        {
            hidingCount -= 1;
            if (hidingCount == 0)
            {
                trueHiding = false;
                hiding = false;
            }
        }
        if (other.transform.name == "WaterBasicDaytime")
        {
            swimming = false;
            gravity = -19f;
            walkSpeed = defWalkSpeed;
            runSpeed = defRunSpeed;
            //Debug.Log("exit" + other.transform.name);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        if (falling > 1f && hit.transform.tag == "Enemy")
        {
            falling2 = 0f;
            Vector3 relpos = gameObject.transform.position - hit.transform.position;
            //Quaternion rotation = Quaternion.LookRotation(relpos, Vector3.up);
            //gameObject.transform.rotation = rotation;
            imp.AddImpact(Vector3.forward + relpos, 25f);
            Debug.Log("osuu " + hit.transform.name);
            Attack(hit.gameObject);
            

        }
        if (hit.transform.name == "Enemy")
        {
            Debug.Log("osuu " + hit.transform.name);
        }

        if (hit.transform.name == "Mouth")
        {
            Vector3 relpos = gameObject.transform.position - hit.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relpos, Vector3.up);
            gameObject.transform.rotation = rotation;
            imp.AddImpact(relpos, 50f);
        }
    }
    public bool CheckHiding()
    {
        return hiding;
    }
    void Attack(GameObject target)
    {
        CharacterStats targetStats = target.GetComponent<CharacterStats>();
        if (targetStats != null)
        {
            
            combat.Attack(targetStats);
            sounds.AttackAudio();
            //Invoke("Jump2", 0.0f);
        }
    }
    void SetFocus(Interactable newFocus)
    {
        // If our focus has changed
        if (newFocus != focus)
        {
            // Defocus the old one
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;   // Set our new focus
            //motor.FollowTarget(newFocus);   // Follow the new focus
        }

        newFocus.OnFocused(transform);
    }
    public void PlayAgain()
    {
        animator.SetBool("is_drowning", false);
        animator.SetBool("is_in_air", false);
        animator.SetBool("sleep", true);
    }
    public void Teleport(Transform destination)
    {
        
        Debug.Log("Teleport to" + destination.position);
        teleport = false;
        friend.GetComponent<FriendPatrol>().Warp(destination);
        gameObject.transform.position = destination.position;
        // Debug.Log("From" + PlayerManager.instance.playerPos.transform.position);
    }
    public void ResetView()
    {
        UnityEngine.XR.InputTracking.Recenter();
    }
    void Start()
    {
        ResetView();
        UnityEngine.XR.InputTracking.Recenter();
        //Debug.Log("plactrlstart");
        Application.targetFrameRate = 30;
        //Teleport();

        //transform.position = PlayerManager.instance.startPos.position;
        
        //Debug.Log(gameObject.transform.position);
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        defWalkSpeed = walkSpeed;
        defRunSpeed = runSpeed;
        sounds = GetComponent<CharacterSounds>();
        
        //lastHealth = myStats.currentHealth;
        //player = GetComponent<Transform>();
        myStats = GetComponent<CharacterStats>();
        imp = GetComponent<ImpactReceiver>();
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();

        combat = GetComponent<CharacterCombat>();
        
        playerState = GameObject.Find("PlayerState");
        if (playerState != null)
        {
            myStats.maxHealth = playerState.GetComponent<Player>().maxHealth;
            myStats.maxStamina = playerState.GetComponent<Player>().maxStamina;
            myStats.maxDive = playerState.GetComponent<Player>().maxDive;
            myStats.ableSwim = playerState.GetComponent<Player>().ableSwim;
            transform.position = playerState.GetComponent<Player>().startPosition;
        }
        enemies = enemys.GetComponentsInChildren<Patrol2>();
    }
    public void CheckEnemies()
    {
        

                            if (!trueHiding)
                            {
                                hiding = false;
                                sleep = false;
                                
                            }
                        
                   
                
                else
                {
                    hiding = true;
                }

            
        
    }
    
    public bool GetJoyVer()
    {
        return false;
    }
    public bool GetSpeed()
    {

        if (currentSpeed != 0f)
        {
            return true;
        }
        return false;
    }

    public bool GetAir()
    {
        
            return jumpCont;
        
    }
        void Update()
    {
        
        if (lasty2 > transform.position.y && falling > 1.5f)
        {
            falling2 +=.5f;
            if (falling2max < falling2)
                falling2max = falling2;
            
        }
        lasty2 = transform.position.y;
       // CheckEnemies();
        if (hiding)
        {
            myStats.Hiding();
        }else
        {
            myStats.NotHiding();
        }
        if (currentSpeed > runSpeed * .8)
        {
            myStats.Running(currentSpeed);
        }
        else
        {
            myStats.GainStamina();
        }
        animator.SetBool("sleep", sleep);
        animator.SetBool("flosh", flosh);
        animator.SetBool("drink", drink);

        if (sleep || flosh || drink){
            if (lastHealth > myStats.currentHealth)
            {
                sleep = false;
                flosh = false;
                drink = false;
            }
        }
        if (sleep)
        {
            flosh = false;
            drink = false;
            //sun.SetSleeping(true);
        }else
        {
            //sun.SetSleeping(false);
        }
        lastHealth = myStats.currentHealth;
        if (idleTime > 20f)
        {
            animator.SetBool("idle", true);
            
        }
        else
        {
            animator.SetBool("idle", false);
           
        }

        if (!GetSpeed() && !jumping)
        {
            animator.SetFloat("speedPercent", 0f);
            
            idleTime += 0.1f;
        }else
        {
            
            idleTime = 0f;
            
        }
        
        
        if (swimming)
        {
            jumpCont = false;
            falling2 = 0f;
            idleTime = 0f;
            animator.SetFloat("speedPercent", 0f, 0f, Time.deltaTime);
            animator.SetBool("is_drowning", true);
            gravity = 0f;
            if (lasty < transform.position.y)
            {
                
                gravity += -3f;

            }
            if (lasty > transform.position.y)
            {
               
                gravity += 1f;
            }

            

            if (transform.position.y < 2.1f)
            {
                gravity = .5f;
                
            }
            if (transform.position.y < 1.8f)
            {
                gravity = .8f;

            }


            if (!myStats.ableSwim)
            {
                
                
                walkSpeed = waterWalkSpeed;
                runSpeed = waterRunSpeed;
                gravity = transform.position.y < 1.8f ? .8f : -1.5f;

                //falling = 0f;
            }

            lasty = transform.position.y;
        }
        else
        {
            animator.SetBool("is_drowning", false);
        }
        myStats.Diamonds();

        //input = new Vector2(joystick.Horizontal, joystick.Vertical);
        input = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);// .PrimaryThumbstick);
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Jump();
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            Sleeping();
        }
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            Flosh();
        }
        
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        {
            ResetView();
        }
        
        // Debug.Log(joystick.Vertical);
        //Vector2 input = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
        Vector2 inputDir = input; //.normalized;
        running = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        
        if (Input.GetKeyDown(KeyCode.F) || Input.GetButton("Fly"))
        {
            if (myStats.ableFly)
            {
                fly = !fly;
                gravity = fly ? 0f : -19f;
            }
        }
        else
        {
            animator.SetBool("attack", false);
        }
        //isGrounded = controller.isGrounded;
        if (!fly)
        {
            myStats.GainFly();
            Move(inputDir, !myStats.tired);
        }
       
        if (falling == 0f)
        {
            animator.SetBool("landed", true);
            

            //jumping = false;


        }
        if (falling < 1f ) { 
            reJump = Mathf.Clamp(reJump, 0f, 8f);
            reJump += 0.1f;
            
                animator.SetBool("is_falling", false);
                //animator.SetBool("is_in_air", false);
            
            
            
        }
       
        if (controller.isGrounded)
        {
            falling2 = 0f;
            falling = 0f;
            
            animator.SetBool("landed", true);
            gravity = -10f;
            walkSpeed = defWalkSpeed;
            runSpeed = defRunSpeed;

            animator.SetBool("isgrounded", true);

            jumpCont = false;
            float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
            animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
            animator.SetBool("falling", false);
            animator.SetBool("is_in_air", false);
            if (sleep)
            {
                //myStats.TakeDamage(10);
                myStats.GainHealth();
            }
        }
        else if (falling >= 1.5f)
        {
            animator.SetBool("falling", true);
            //float animationSpeedPercent = 0f;
            //animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
        }
        else
        {
            animator.SetBool("isgrounded", false);
            falling += 0.1f;
            animator.SetBool("landed", false);

        }
        
       
    }
    void Fly(Vector2 inputDir, bool running)
    {
        
        mousey += Input.GetAxis("Mouse Y");
        mousey = Mathf.Clamp(mousey, pitchMinMax.x, pitchMinMax.y);

        moveDirection = new Vector3(0f, mousey * mouseSensitivity, inputDir.y);
        
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));

        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * mousey;// velocityY;

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= ((running) ? speed *2f : speed);

        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        //controller.Move(moveDirection * Time.deltaTime);
    }

    void Move(Vector2 inputDir, bool running)
    {
        if (inputDir != Vector2.zero)
        {
            drink = false;
            flosh = false;
            sleep = false;
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }

        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));
        //Debug.Log("mag " + inputDir);
        //Debug.Log(targetSpeed + "targ");
        //Debug.Log(currentSpeed + "curre");
        
        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            velocityY = 0;
        }

    }
    public void Drinking(){
        drink = !drink;
        sleep = false;
        flosh = false;
    }
    public void Sleeping(){
        sleep = !sleep;
        flosh = false;
        drink = false;
    }
    public void Flosh(){
        flosh = !flosh;
        sleep = false;
        drink = false;
    }

   
    public void Jump()
    {
        if (falling < 1f)
        {
            animator.SetBool("is_in_air", true);
            flosh = false;
            drink = false;
            sleep = false;
            jumping = true;

            falling2max = 0;
            jumpCont = true;
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
            //animator.SetBool("is_in_air", true);
            // animator.SetBool("is_in_air", false);
            reJump = 0;
            jumping = false;
        }
    }
    

    float GetModifiedSmoothTime(float smoothTime)
    {
        if (controller.isGrounded)
        {
            return smoothTime;
        }

        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime;/// airControlPercent;
    }
}