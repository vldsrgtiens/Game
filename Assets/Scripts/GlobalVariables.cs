using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public enum  TypePerson { Hero, Robot, Enemy };
    public enum  MotionStatus { isWaiting, isMoving, isRotating, isBeforeTargetPosition,isBeforeTargetRotation };
    public enum TypeDirection { North=0, NorthEast=1, SouthEast=2, South=3, SouthWest=4, NorthWest=5, NorthNorth=6}
    public static float globVal = 5f;
    public static float angleRotate = 60f;
    const int scaleTerrain = 2;
    
   

    

    



}
