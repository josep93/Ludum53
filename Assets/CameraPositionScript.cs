using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionScript : MonoBehaviour
{
    public static CameraPositionScript current;
    Transform initialPosition;
    float initialSize;
    [SerializeField] float dramaticSize;
    [SerializeField] SpriteRenderer fadeToBlack;
    Camera cameraComponent;
    [SerializeField] Vector3 dramaticPosition,dramaticRotation;
    // Start is called before the first frame update
    void Start()
    {
        current = this;
        cameraComponent = GetComponent<Camera>();
        initialSize = cameraComponent.orthographicSize;
        initialPosition = transform;
    }

    public void DramaticPose()
    {
        transform.position = dramaticPosition;
        transform.rotation = Quaternion.Euler(dramaticRotation);
        cameraComponent.orthographicSize = dramaticSize;
        //cameraComponent.backgroundColor = Color.black;
        fadeToBlack.enabled = true;
    }

}
