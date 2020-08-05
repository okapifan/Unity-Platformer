using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string player_name;
    public Color32 body_color;
    public float movement_scaler;
    public float max_speed;
    public float jump_scaler;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public SpriteRenderer eyeRenderer;
    public GameObject player;
    private bool is_on_ground;
    private GameObject sign;
    public DataManager dataManager;

    void Start()
    {
        dataManager.load();
        player_name = dataManager.data.name;
        sr.color = body_color;

        GameObject sign = new GameObject("player_label");
        sign.transform.rotation = Camera.main.transform.rotation; // Causes the text faces camera.
        TextMesh tm = sign.AddComponent<TextMesh>();
        tm.text = dataManager.data.name;
        tm.color = new Color(0.8f, 0.8f, 0.8f);
        tm.fontStyle = FontStyle.Bold;
        tm.alignment = TextAlignment.Center;
        tm.anchor = TextAnchor.MiddleCenter;
        tm.characterSize = 0.065f;
        tm.fontSize = 60;
        sign.transform.SetParent(player.transform);
        sign.transform.position = player.transform.position + Vector3.up * 1f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x_movement = Input.GetAxis("Horizontal");
        if (rb.velocity.magnitude < max_speed)
        {
            Vector2 movement = new Vector2(x_movement, 0);
            rb.AddForce(movement_scaler * movement);
            if (x_movement < 0)
            {
                sr.flipX = true;
                eyeRenderer.flipX = true;
            } else if(x_movement > 0)
            {
                sr.flipX = false;
                eyeRenderer.flipX = false;
            }
        }

        if (Input.GetButtonDown("Jump") && is_on_ground)
        {
            Vector2 jump_force = new Vector2(0, jump_scaler);
            rb.AddForce(jump_force);
        }

    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (CollisionIsWithGround(c))
        {
            is_on_ground = true;
        }
    }

    void OnCollisionExit2D(Collision2D c)
    {
        if (!CollisionIsWithGround(c))
        {
            is_on_ground = false;
        }
    }

    private bool CollisionIsWithGround(Collision2D collision)
    {
        bool is_with_ground = false;
        foreach (ContactPoint2D c in collision.contacts)
        {
            Vector2 collision_direction_vector = c.point - rb.position;
            if(collision_direction_vector.y < 0)
            {
                is_with_ground = true;
            }
        }
        return is_with_ground;
    }
}
