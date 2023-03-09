
using System.Collections.Generic;
using UnityEngine;
//using Vector3 = UnityEngine.Vector3;

public class MotionModule : MonoBehaviour
{
    //public GlobalVariables.TypeOrientation mmOrientation;
    

    public GlobalVariables.MotionStatus mmStatus = GlobalVariables.MotionStatus.isWaiting;

    public Vector3 mmTargetPosition = Vector3.zero;
    public Compass.TypeDirection mmTargetDirection;
    
    

    private float mmSpeed;
    private float directionOfRotation = 1f;
    private float oldRotation = 360f;

    Rigidbody mm_Rigidbody;
    Transform mm_Transform;
    ObjectItem mm_ObjectItem;



    void Awake()
    {
        //mmDirection = GlobalVariables.TypeSideWorld[3];
        //transform.localEulerAngles.y / GlobalVariables.angleRotate;
        
        mm_Rigidbody = GetComponent<Rigidbody>();
        print("transform position = "+transform.position);
        mm_ObjectItem = GetComponent<ObjectItem>();

        mmSpeed = mm_ObjectItem.speed;

        mmStatus = GlobalVariables.MotionStatus.isMoving;
        
        Debug.Log("MotionDrive Awake() was called");
    }
    
    void FixedUpdate()
    {
        if (mmStatus == GlobalVariables.MotionStatus.isMoving )
        {
            // вектор направления к цели. вектор надо нормализовать, чтобы скорость была постоянной, иначе она будет зависеть от расстояния.
            Vector3 heading = (mmTargetPosition - transform.position).normalized;
            float step = Time.deltaTime * mmSpeed;

            float dist_toTarget = Vector3.Distance(mmTargetPosition, transform.position + heading * step);
            
            if (dist_toTarget < step) 
            {
                print("attention: near the target "+dist_toTarget);
                mmStatus = GlobalVariables.MotionStatus.isBeforeTargetPosition;
            }
            print("position: "+transform.position);
            mm_Rigidbody.MovePosition(transform.position + heading * step);
        }

        if (mmStatus == GlobalVariables.MotionStatus.isBeforeTargetPosition)
        {
            mm_Rigidbody.position = mmTargetPosition;
            mm_Rigidbody.velocity = Vector3.zero; 
            mmStatus = GlobalVariables.MotionStatus.isWaiting;
            
            print("attention: the target ");
        } 
        
        if (mmStatus == GlobalVariables.MotionStatus.isWaiting)
        {
            mmTargetPosition=transform.position+(2*(new Vector3(40f,0f,0f)-transform.position));
            mmStatus = GlobalVariables.MotionStatus.isMoving;
        }
        
        if (mmStatus == GlobalVariables.MotionStatus.isRotating )
        {
            //mmTargeDirectionV3 = Compass.DirectionToVector3(mmTargetDirection);
            
            float _delta = (60f * (int)mmTargetDirection) - oldRotation ;
            print("delta Rotation = "+_delta);

            
            if (_delta < 1f) 
            {
                print("attention: near the target "+_delta);
                mmStatus = GlobalVariables.MotionStatus.isBeforeTargetRotation;
            }
            print("rotationn: "+transform.rotation);
            mm_Transform.Rotate(new Vector3(0f,mmSpeed*directionOfRotation,0f));
        }
    }

    public void MoveForward(Vector3 target)
    {
        mmTargetPosition = target;
        mmStatus=GlobalVariables.MotionStatus.isMoving;
    }
        
    public void MoveBack(Rigidbody rb,Vector3 vector3To, float speed)
    {
        //rb.velocity = -1 * vector3To * speed;
        //is_moving = true;
    }

    public void RotateToLeft()
    {
        //player.transform.RotateAround(player.transform.position,player.transform.up,_rotation_y*Time.deltaTime);
        mmTargetDirection = Compass.RotateToLeft(mm_ObjectItem.direction);
        oldRotation = 60f * (int)mm_ObjectItem.direction;
        directionOfRotation = -1f;
        mmStatus = GlobalVariables.MotionStatus.isRotating;
    }
    public void RotateToRight()
    {
        //player.transform.RotateAround(player.transform.position,player.transform.up,_rotation_y*Time.deltaTime);
        mmTargetDirection = Compass.RotateToLeft(mm_ObjectItem.direction);
        oldRotation = 60f * (int)mm_ObjectItem.direction;
        directionOfRotation = 1f;
        mmStatus = GlobalVariables.MotionStatus.isRotating;
    }
}
