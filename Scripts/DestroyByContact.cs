using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject explosionPlayer;

    private GameObject cloneExplosion;

    public int scoreValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cloneExplosion = Instantiate(explosionPlayer, GetComponent<Rigidbody>().position, GetComponent<Rigidbody>().rotation) as GameObject;

            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GameOver();

            Destroy(other.gameObject); // удаление корабля
            Destroy(gameObject); // удаление астероида
            Destroy(cloneExplosion, 1f);
        }

        if (other.tag == "Bolt")
        {
            cloneExplosion = Instantiate(explosion, GetComponent<Rigidbody>().position, GetComponent<Rigidbody>().rotation) as GameObject;

            Destroy(other.gameObject); // удаление пули
            Destroy(gameObject); // удаление астероида и его наследников
            Destroy(cloneExplosion, 1f);

            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().IncreaseScore(scoreValue);
        }
    }
}
