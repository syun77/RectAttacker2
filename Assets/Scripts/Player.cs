using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float MOVE_SPEED;

    // ======================================
    // Variables. Objects.
    public GameObject _shot;
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
        transform.localPosition = Utils.ClampPosition(transform.localPosition);

        if (Input.GetKeyDown("space")) {
            _enableShot = true;
        }
        else if(Input.GetKeyUp("space")) {
            _enableShot = false;
        }

        if(_enableShot) {
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
    }
}
