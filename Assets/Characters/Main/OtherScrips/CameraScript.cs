using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Character;
    public Camera MainCamera;
    public Vector3 Offset=new Vector3(3,3,0);
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MainCamera.transform.position = Character.transform.position + Offset;
    }

}
