using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageDeliveryScript : MonoBehaviour
{
    [SerializeField] private float force = 5;
    [SerializeField] private float angle = 45;

    [SerializeField] public Rigidbody2D rb;

    public static PackageDeliveryScript realPackage;

    [Header("Paralax")]
    [SerializeField] private ParalaxControllerScript paralax;
    [SerializeField] private bool paralaxIsActive = false;
    [SerializeField] private GameObject ground;

    [SerializeField] private GameObject powerBar,distance;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (transform.position.y - ground.transform.position.y >  60 && !paralaxIsActive)
        {
            paralax.InitParalax();
            paralaxIsActive = true;
            return;
        }

        if (transform.position.y - ground.transform.position.y < 60 && paralaxIsActive)
        {
            paralax.StopParalax();
            paralaxIsActive = false;
            return;
        }*/

    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ThrowPackage(float force = 0, float angle = 0)
    {
        realPackage = this;
        if (angle == 0)
        {
            angle = this.angle;
        }
        if (force == 0)
        {
            force = this.force;
        }

        Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right);
        rb.AddForce(dir * force, ForceMode2D.Impulse);
        rb.AddTorque(360f);
        ShutDownPowerBar();
        ActiveDistance();
    }

    private void ShutDownPowerBar()
    {
        powerBar.SetActive(false);
    }

    private void ActiveDistance()
    {
        distance.SetActive(true);
    }

}
