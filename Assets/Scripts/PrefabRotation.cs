using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabRotation : MonoBehaviour
{
    public float spinSpeed = 10f; // The speed at which the object will spin on the y-axis
    private void Update()
    {
        // Get the current rotation of the object
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Only rotate the object on the y-axis at the given speed every frame
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y + spinSpeed * Time.deltaTime, currentRotation.z);
    }
}
