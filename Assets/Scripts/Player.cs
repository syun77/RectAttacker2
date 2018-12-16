using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float MOVE_SPEED = 5;

    // ======================================
    // Variables. Objects.
    Rigidbody2D _rigidbody2D;

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
