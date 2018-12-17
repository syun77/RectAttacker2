using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float MOVE_SPEED;

    // ======================================
    // Variables. Objects.
    public GameObject _shot;
    public GameObject _horming;
    Rigidbody2D _rigidbody2D;

    // ======================================
    // Variables.
    bool _enableShot = false;

    // ======================================
    // Functions.
    // Use this for initialization
    void Start () {
        _rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
        Move();
    }

    private void FixedUpdate() {
        _enableShot = Input.GetKey(KeyCode.Space);

        if (_enableShot) {
            Vector3 v = new Vector3(transform.position.x, transform.position.y);
            GameObject obj = Instantiate(_shot, v, Quaternion.identity);
            Shot shot = obj.GetComponent<Shot>();
            float degree = 90;
            degree += Random.Range(-10, 10);
            shot.SetVelocity(degree, shot.MOVE_SPEED);
        }
    }

    void Move() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 v = new Vector2(horizontal, vertical);
        v.Normalize();
        v *= MOVE_SPEED;
        _rigidbody2D.velocity = v;

        // Check to outside.
        transform.localPosition = Utils.ClampPosition(transform.localPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch(collision.tag) {
        case "Bullet":
            // TODO:
            {
                Vector3 v = new Vector3(transform.position.x, transform.position.y);
                GameObject obj = Instantiate(_horming, v, Quaternion.identity);
                Homing h = obj.GetComponent<Homing>();
                Bullet b = collision.gameObject.GetComponent<Bullet>();
                h.SetDirection(Utils.GetDegree(b.GetComponent<Rigidbody2D>()));
                //h.SetDirection(Random.Range(0, 360));
                b.Vanish();
            }
            break;

        case "Enemy":
            break;
        }
    }
}
