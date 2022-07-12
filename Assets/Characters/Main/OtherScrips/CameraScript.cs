using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]private GameObject Character;
    [SerializeField]private Camera MainCamera;
    [SerializeField]private Vector3 Offset=new Vector3(5,4,-10);
    private void FixedUpdate()
    {
        MainCamera.transform.position = Character.transform.position + Offset;
    }

}
