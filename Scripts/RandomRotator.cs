using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    public float tumble;
    private Rigidbody rigidbody;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.angularVelocity = new Vector3(1, 1, 1) * tumble; // задаем направление угловой скорости вращения объекта

        rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
    }

}
