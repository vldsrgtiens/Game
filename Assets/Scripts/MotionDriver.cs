using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionDriver : MonoBehaviour
{
    public GameObject obj;
    public GameObject mainCamera;//MainCamera
    public GameObject eye;
    
    // Start is called before the first frame update
    void Awake()
    {
        SetCameraToHero();
    }

    void SetCameraToHero()
    {
        
        if (this.tag == "Hero")
        {
            mainCamera = GameObject.Find("Main Camera");
            
            foreach (Transform child in transform)
            {
                if (child.gameObject.tag == "eye")
                {
                    eye = child.gameObject;
                }
            }
            if (eye != null)
            {
                if (mainCamera != null)
                {
                    print("camera found");
                    mainCamera.transform.SetParent(eye.transform, false);
                    mainCamera.transform.localPosition = Vector3.zero;
                    print("set parent camera");
                }
                else print("ATTANTION: camera not found");
            }
            else
            {
                print("ATTANTION: eye not found");
            }
        }
    }
}
