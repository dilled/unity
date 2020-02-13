using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool attackZoom = false;
    public float aZoom =4f;
    

    public float camPosY = 0.6f;
    public bool lockCursor;
    public float mouseSensitivity = 3;
    public Transform target;
    public GameObject targetObj;
    public float dstFromTarget = 2;
    public float currentDstFromTarget;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float currentPitch;
    //Vector3 nextPos;
    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    bool eka;// = true;
    public float yaw;
    public float yaw2;
    public float pitch;
    public bool moving;
    public bool jumped;
    public float defPitch = 3.2f;
    PlayerController2 pCont;
    bool backing;
    
    public bool underAttack;
    GameObject playerState;

    void Start()
    {
        playerState = GameObject.Find("PlayerState");
        pCont = targetObj.GetComponent<PlayerController2>();
        if (playerState != null)
        {
            //mouseSensitivity = playerState.GetComponent<Player>().sensitivity;
        }
        else
        {
            mouseSensitivity = 4f;
        }
            currentDstFromTarget = dstFromTarget;
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        //currentRotation = target.eulerAngles;
    }
    
    public void OnValueChanged(float newValue)
    {
        mouseSensitivity = newValue;
        //Debug.Log(newValue);
    }
    public void SetUnderAttack()
    {
        underAttack = true;
    }
    public void SetNotUnderAttack()
    {
        underAttack = false;
    }
    private void Update()
    {
        moving = pCont.GetSpeed();
        //backing = pCont.GetJoyVer();
        jumped = pCont.GetAir();


        if (underAttack && attackZoom)
        {
            if (dstFromTarget < aZoom)
            {
                currentDstFromTarget += .1f;
                dstFromTarget += .1f;
            }
            if (pitch < 40f)
            {
                pitch += .7f;
            }
        }
        else
        {
            if (dstFromTarget > 3.3f)
            {
                currentDstFromTarget -= .1f;
                dstFromTarget -= .1f;
            }
            if (pitch > 3f)
            {
                pitch -= .7f;
            }
            //currentDstFromTarget = 3.26f;
            //dstFromTarget = 3.26f;
        }

        if (moving && !underAttack)
        {
            // Debug.Log(pitch);
            if (pitch < defPitch )
            {
                if (pitch + 3f < defPitch)
                {
                    pitch += 3f;
                }else
                {
                    pitch = defPitch;
                }
            }
            if (pitch > defPitch )
            {
                if (pitch - 3f > defPitch)
                {
                    pitch -= 3f;
                }else
                {
                    pitch = defPitch;
                }
            }
        }


        //currentDstFromTarget = Mathf.Clamp(currentDstFromTarget, 1.4f, dstFromTarget);
       // RaycastHit hit;
        RaycastHit hitDown;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitDown, 1.5f))
        {
           // Debug.Log("hitDown " + hitDown.transform.name);
                     
               // Debug.Log("++");
                pitch += 0.7f;
            

        }
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitDown, 1.8f) && currentPitch >=4 && currentPitch <= 30 && !underAttack)
        {
            //Debug.Log("hitDown " + hitDown.transform.name);
            //Debug.Log("--");
                pitch -= 0.7f;
            
            
        }
        /*
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, currentDstFromTarget))
        {
            //Debug.Log(hit.transform.name);
           
            if (hit.transform.name != "unicorn" && hit.transform.name != "WaterBasicDaytime")
            {
                //currentDstFromTarget -= 0.1f;
               // Debug.Log("hit " + hit.transform.name);
            }
            else
            {
                if (Physics.Raycast(nextPos, transform.TransformDirection(Vector3.forward), out hit, currentDstFromTarget + 0.2f))
                {
                    if (hit.transform.name == "unicorn")
                    {
                       // Debug.Log("hit " + hit.transform.name);
                       // currentDstFromTarget += 0.1f;
                    }
                }
            }
        }*/
        }

    void LateUpdate()
    {
       // Vector2 input = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        Vector2 input2 = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        if (!jumped)
        {
            //yaw += joystick.Horizontal * mouseSensitivity;
            yaw += input2.x;
        }
        // yaw2 += joystick2.Horizontal * mouseSensitivity;
        //pitch -= joystick2.Vertical * mouseSensitivity;
        //pitch = 0f;
        //pitch -= input2.y * mouseSensitivity;
        yaw2 += input2.x * mouseSensitivity;
        yaw += input2.x * mouseSensitivity;
        //yaw += CrossPlatformInputManager.GetAxis("Mouse X") * mouseSensitivity/2;
        //pitch -= CrossPlatformInputManager.GetAxis("Mouse Y") * mouseSensitivity;
        //yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        //pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
        currentPitch = pitch;
        if (yaw >40)
        {
            eka = false;
        }
        if (eka)
        {
            yaw += 1f;
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
            
        }/*
        if (joystick.Horizontal !=0 || joystick.Vertical != 0 )
        {
               // currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
            
        }
        if (joystick2.Horizontal != 0 || joystick2.Vertical != 0)
        {
            yaw = yaw2;
        }*/
            /*
            if (moving && !backing )
            {

                //currentRotation = target.eulerAngles;

                    yaw2 = yaw;
                    currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw2), ref rotationSmoothVelocity, rotationSmoothTime);

                //currentRotation = Vector3.SmoothDamp(currentRotation, target.eulerAngles, ref rotationSmoothVelocity, rotationSmoothTime);


                //Debug.Log(target.eulerAngles + " " + transform.eulerAngles);
                //currentRotation = Vector3.SmoothDamp(currentRotation, target.eulerAngles, ref rotationSmoothVelocity, rotationSmoothTime);
            }
            else if (!moving)
            {
                if (joystick.Horizontal == 0)
                {
                    currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw2), ref rotationSmoothVelocity, rotationSmoothTime);

                }
                else 
                {
                   // currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
                }

                //currentRotation = target.eulerAngles;
                //currentRotation = Vector3.SmoothDamp(currentRotation, target.eulerAngles, ref rotationSmoothVelocity, rotationSmoothTime);
            }*/
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - (transform.forward + new Vector3(0f, camPosY, 0f)) * currentDstFromTarget;
        //nextPos = target.position - transform.forward * (currentDstFromTarget + 0.1f);
    }
    void OnDrawGizmosSelected()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward) * dstFromTarget;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction);
    }
}