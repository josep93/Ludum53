using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabController : MonoBehaviour
{

    [SerializeField] private GameObject package;
    [SerializeField] private GameObject paralax;

    public void Throw()
    {
        package.GetComponent<PackageDeliveryScript>().ThrowPackage(20, 45);
    }

}
