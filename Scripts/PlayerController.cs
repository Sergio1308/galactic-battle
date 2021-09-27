using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // сериализация для отображения данных в инспекторе
public class Boundary { // границы поля
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
    public float fireRate = 0.5f; // как часто будут вылетать пули
    public float nextFire = 0.0f; // регулирование разрешения на стрельбу

    public Quaternion calibrationQuaternion;
    private Vector3 movement;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        CalibrateAccelerometer();
    }

    public void CalibrateAccelerometer()
    {
        // 
        Vector3 accelerationSnapshot = Input.acceleration;

        //
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);

        //
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
        Debug.Log("Calibrated");
    }

    public Vector3 FixedAcceleraton (Vector3 acceleration)
    {
        //
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }

    public void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire) // если нажата кнопка выстрела и время прошедшее от начала игры больше чем переменная 
        {
            if (!pauseMenu.GetComponent<PauseMenu>().paused)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // создание клона
                GetComponent<AudioSource>().Play();
            }
            else Debug.Log("paused, cant shoot");
        }

        if (pauseMenu.GetComponent<PauseMenu>().paused)
        {
            CalibrateAccelerometer();
        }

    }

    private void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 accelerationRaw = Input.acceleration; //
        Vector3 acceleration = FixedAcceleraton(accelerationRaw); //

        if (!AndroidRemoteControl)
        {
            Debug.Log("keyboard");
            // keyboard
            //float moveHorizontal = Input.GetAxis("Horizontal");
            //float moveVertical = Input.GetAxis("Vertical");
            //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            float moveX = moveHorizontal;
            float moveY = moveVertical;
            Vector3 movement = new Vector3(moveX, 0.0f, moveY);
            rigidbody.velocity = movement * speed;
        }
        else
        {
            Debug.Log("android");
            // android acceleration

            //Vector3 accelerationRaw = Input.acceleration; //
            //Vector3 acceleration = FixedAcceleraton(accelerationRaw); //

            //Vector3 movement = new Vector3(acceleration.x, 0.0f, acceleration.y);
            Vector3 moveX = accelerationRaw; //
            Vector3 moveY = acceleration; //
            Vector3 movement = new Vector3(moveX.x, 0.0f, moveY.y);
            rigidbody.velocity = movement * speed;
        }
        
        rigidbody.position = new Vector3
            (
                Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
            );

        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt); // наклон корабля

    }
}
