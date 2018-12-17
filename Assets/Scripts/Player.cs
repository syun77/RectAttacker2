using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float MOVE_SPEED;
    public float REFLECT_OFFSET;

    // ======================================
    // Variables. Objects.
    public GameObject _shot;
    public GameObject _horming;
    Reflect _reflect;
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

    Reflect _GetReflect() {
        if(_reflect == null) {
            GameObject obj = GameObject.Find("Reflect");
            if(obj != null) {
                _reflect = obj.GetComponent<Reflect>();
            }
        }

        return _reflect;
    }

	// Update is called once per frame
	void Update () {
        Move();
    }

    private void FixedUpdate() {
        // Check to fire a shot.
        _UpdateShot();

        // Check to use a shield.
        UpdateReflect();
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

    void UpdateReflect() {

        Reflect r = _GetReflect();
        if(r != null) {
            bool b = Input.GetKey(KeyCode.X);
            if(b) {
                r.SetVisible(true);
            }
            else {
                r.SetVisible(false);
            }

            float x = transform.position.x;
            float y = transform.position.y;
            y += REFLECT_OFFSET;
            r.SetPosition(x, y);
        }
    }

    void _UpdateShot() {

        _enableShot = Input.GetKey(KeyCode.Z);
        if (_enableShot) {
            Vector3 v = new Vector3(transform.position.x, transform.position.y);
            GameObject obj = Instantiate(_shot, v, Quaternion.identity);
            Shot shot = obj.GetComponent<Shot>();
            float degree = 90;
            degree += Random.Range(-10, 10);
            shot.SetVelocity(degree, shot.MOVE_SPEED);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        switch(collision.tag) {
        case "Bullet":
            break;

        case "Enemy":
            break;
        }
    }
}
