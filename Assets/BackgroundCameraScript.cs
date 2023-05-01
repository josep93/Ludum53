using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCameraScript : MonoBehaviour
{
    CameraPositionScript mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        //mainCamera = CameraPositionScript.current;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(CameraPositionScript.current.transform.position.x/50, CameraPositionScript.current.transform.position.y/10, -1);
    }
}
