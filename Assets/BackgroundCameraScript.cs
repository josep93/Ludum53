using UnityEngine;

public class BackgroundCameraScript : MonoBehaviour
{
    CameraPositionScript mainCamera;
    [SerializeField] bool rightIsUp = false;
    // Start is called before the first frame update
    void Start()
    {
        //mainCamera = CameraPositionScript.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (rightIsUp)
        {
            transform.localPosition = new Vector3(CameraPositionScript.current.transform.position.x / 100, CameraPositionScript.current.transform.position.y / 10, -1);
            return;
        }
        transform.localPosition = new Vector3(CameraPositionScript.current.transform.position.x/50, CameraPositionScript.current.transform.position.y/10, -1);
    }
}
