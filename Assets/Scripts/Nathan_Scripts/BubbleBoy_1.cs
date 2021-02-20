using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBoy_1 : MonoBehaviour
{
    public GameObject bubble_boy_bullet_prefab;
    public Rigidbody2D bullet_rb;
    [SerializeField]
    private int health = 1000;
    private float bumped_into_damage_to_cause_player = 0.06f;
    public float movement_speed;
    private Transform thePlayer;
    public LayerMask playerLayer;
    private bool facing_right;
    public bool drops;
    public GameObject theDrops;
    public Transform dropPoint;

    IEnumerator Shoot_Time_Interval() {
        yield return new WaitForSeconds(1.5f);
        Instantiate(bubble_boy_bullet_prefab, transform.position, transform.rotation);
        StartCoroutine(Shoot_Time_Interval());
    }
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Player").transform;
        StartCoroutine(Shoot_Time_Interval());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, movement_speed * Time.deltaTime);
        Flip();
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.transform.DetachChildren();
        Destroy(gameObject);
        if (drops) Instantiate(theDrops, dropPoint.position, dropPoint.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //move enemy away from player
        transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, 0);
        //cause player damage
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            Static_Vars_1.player_health_static = Static_Vars_1.player_health_static - bumped_into_damage_to_cause_player;
        }
    }

    void Flip() {

        if (thePlayer.position.x < transform.position.x && !facing_right || thePlayer.position.x > transform.position.x && facing_right)
        {
            facing_right = !facing_right;
            transform.Rotate(0, 180f, 0);
        }
    }

}
