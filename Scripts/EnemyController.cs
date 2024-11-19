using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    GameObject scoreUITextGO; //reference to the text UI game object
    public GameObject ExplosionGO; //this is our explosion prefab

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        //get the score text UI
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {

        //Get the player's current position
        Vector2 position = transform.position;

        //compute the enemy new position
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        //update the bullet's position
        transform.position = position;

        //this is the bottom-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //detect collision of the enemy ship with an player ship, or with an player bullet
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();

            //add 100 points to the score
            scoreUITextGO.GetComponent<GameScore>().Score += 100;

            //for testing purposes temporarily comment the line below
            Destroy(gameObject); //destroy this enemy ship
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }
}
