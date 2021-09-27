using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float speed;

    private Vector3 startPosition;

    public int numberOfDuplicates;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; // запоминаем позицию текущего объекта 
    }

    // Update is called once per frame
    public void Update()
    {
        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().gameOver == false)
        {
            float newPosition = Mathf.Repeat(Time.time * speed, transform.localScale.y * numberOfDuplicates);
            transform.position = startPosition + Vector3.back * newPosition; // сдвиг фона относительно старт.позиции назад на n ед.

            gameObject.GetComponent<AudioSource>().Play();
        }

        else numberOfDuplicates = 0; // stop scrolling

    }
}
