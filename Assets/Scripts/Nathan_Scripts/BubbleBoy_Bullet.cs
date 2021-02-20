using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBoy_Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    private float damage_to_cause = 0.04f;

    // Start is called before the first frame update
    void Start()
    {
        Transform player_transform = GameObject.Find("Player").transform;
        Vector3 aim = (player_transform.position - transform.position);
        rb.AddForce(aim * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BubbleBoy_1 bubble_boy_1 = collision.GetComponent<BubbleBoy_1>();
        Player player = collision.GetComponent<Player>();
        Bullet bullet = collision.GetComponent<Bullet>();
        BubbleBoy_Bullet bubbleboy_bullet = collision.GetComponent<BubbleBoy_Bullet>();

        if (bubble_boy_1 != null) {
            return;
        }
        if (player != null) {
            Static_Vars_1.player_health_static = Static_Vars_1.player_health_static - damage_to_cause;
        }
        if (bullet != null) {
            return;
        }
        if (bubbleboy_bullet != null) {
            return;
        }

        Destroy(gameObject);
    }
}
