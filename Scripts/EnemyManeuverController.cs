using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManeuverController : MonoBehaviour
{
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;

    public Boundary boundary;

    private float targetManeuver;
    public float dodge;
    public float maneuverSpeed;
    private float currentSpeed;
    public float tilt;

    private void Start()
    {
        currentSpeed = GetComponent<Rigidbody>().velocity.z;

        StartCoroutine(Evade());
    }

    IEnumerator Evade() // coroutine func
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y)); // pause

        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);

            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));

            targetManeuver = 0;

            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }
    private void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(GetComponent<Rigidbody>().velocity.x ,targetManeuver, maneuverSpeed * Time.deltaTime);

        GetComponent<Rigidbody>().velocity = new Vector3(newManeuver, 0.0f, currentSpeed); // движение корабля

        GetComponent<Rigidbody>().position = new Vector3
            (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
            0.0f, 
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}
