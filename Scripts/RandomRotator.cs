using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class RandomRotator, used to implement 
 * rotation of asteroid objects around their axis in a random direction.
 */
public class RandomRotator : MonoBehaviour
{
    public float tumble;  // rotation rate
    private new Rigidbody rigidbody;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        // set direction of the angular velocity of rotation of the object
        rigidbody.angularVelocity = new Vector3(1, 1, 1) * tumble; 

        rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
    }

}
