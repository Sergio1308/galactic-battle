using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]  // serialization to display data in the inspector
public class Boundary {  // screen boundaries
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

    public bool AndroidRemoteControl;

    public float speed = 10;
    public float tilt;

    public Boundary boundary;
    public new Rigidbody rigidbody;

    public GameObject pauseMenu;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.5f;   // how often we can shoot
    public float nextFire = 0.0f;   // firing delay between bullets

    public Quaternion calibrationQuaternion;
    private Vector3 movement;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        CalibrateAccelerometer();
    }

    public void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);

        Debug.Log("Calibrated");
    }

    public Vector3 FixedAcceleraton (Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }

    public void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)  // if the shot button is pressed and
                                                               // the time elapsed since the start of the game is greater than firing delay
        {
            if (!pauseMenu.GetComponent<PauseMenu>().paused)  // pause check
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);  // creation a clone from an instance
                GetComponent<AudioSource>().Play();
            }
            else Debug.Log("paused, can't shoot");
        }

        if (pauseMenu.GetComponent<PauseMenu>().paused)
        {
            // CalibrateAccelerometer();
        }

    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 accelerationRaw = Input.acceleration;
        Vector3 acceleration = FixedAcceleraton(accelerationRaw);

        if (!AndroidRemoteControl)  // the ability to change control for debugging
        {
            // switched to keyboard control

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rigidbody.velocity = movement * speed;
        }
        else
        {
            // switched to android control

            Vector3 movement = new Vector3(acceleration.x, 0.0f, acceleration.y);
            rigidbody.velocity = movement * speed;
        }
        

        rigidbody.position = new Vector3
            (
                Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
            );

        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);  // tilt the player object when it moves

    }
}
