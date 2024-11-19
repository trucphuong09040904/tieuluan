using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{

    public GameObject GameManagerGO; //reference to our game manager

    public GameObject PlayerBulletGO; //this us our player's bullet prefab
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;
    public GameObject ExplosionGO; //this is our explosion prefab
    public GameManager gameOver;

    public GameManager HP;

    public float currentHP;
    public float maxHP = 10;

    public float speed;

    private bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        HP.UpdateHP(currentHP, maxHP);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space")) 
        {
            //play the laser sound effect
            GetComponent<AudioSource>().Play();

            //instantiate the first bullet
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = bulletPosition01.transform.position; //set the bullet initial position

            //instantiate the second bullet
            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = bulletPosition02.transform.position; //set the bullet initial position
        }

        float x = Input.GetAxisRaw("Horizontal"); //the value will be -1, 0, 1 (left, no input, right)
        float y = Input.GetAxisRaw("Vertical"); //the value will be -1, 0, 1 (down, no input, up)
        
        //now based on the input we compute a direction vector, and we normalize it to get a unit vector
        Vector2 direction = new Vector2(x, y).normalized;
        
        //now we call the function that computes and sets the player's position
        Move (direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2(0, 0)); //this is the bottom-left point (corner) of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //this is the top-right point (corner) of the screen

        max.x = max.x - 0.225f; //subtract the player sprite half width
        min.x = min.x + 0.225f; //add the player dprite half width

        max.y = max.y - 0.225f; //subtract the player sprite half height
        min.y = min.y + 0.225f; //add the player dprite half width

        //Get the player's current position
        Vector2 pos = transform.position;

        //Calculate the new position
        pos += direction * speed * Time.deltaTime;

        //Make sure the new position is not outside the screen
        pos.x = Mathf.Clamp (pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //Update the player's position
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //detect collision of the player ship with an enemy ship, or with an enemy bullet
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();

            currentHP -= 2;
            HP.UpdateHP(currentHP, maxHP);
            if (currentHP <= 0 && !isDead)
            {
                isDead = true;
                gameOver.Over();
                Destroy(this.gameObject);
            }
        }
    }

    //function to instantiate an explosion
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }
}
