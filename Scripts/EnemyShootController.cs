using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootController : MonoBehaviour
{

    public GameObject shot;
    public Transform shotSpawn; // coords

    public float fireRate; // frequency
    public float delay; 

    private new AudioSource audio;

    // Start is called before the first frame update
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate); // вызывает метод(fire) через n-секунд с момента запуска(delay) и повторяется через fireRate
    }

    private void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // создание клона
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
