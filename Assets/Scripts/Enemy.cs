﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    // ==============================================
    // Variables: Objects.
    public GameObject player;
    public GameObject bullet;
    public GameObject particle;
    Rigidbody2D _rigidbody2D;

    // ==============================================
    // Variables.
    int interval = 0;

    // ==============================================
    // Use this for initialization
    void Start () {
        _rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
    void DoBullet(float degree, float speed) {
        Vector3 v = new Vector3(transform.position.x, transform.position.y);
        GameObject obj = Instantiate(bullet, v, Quaternion.identity);
        Bullet b = obj.GetComponent<Bullet>();
        b.SetVelocity(degree, speed);
    }

	// Update is called once per frame
	void Update () {
	}

    private void FixedUpdate() {
        interval++;
        DoBullet(interval * 3, 5);
        DoBullet(interval * -3, 5);
        DoBullet(interval * 1, 5);
        DoBullet(interval * -1, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Shot") {
            Shot shot = collision.gameObject.GetComponent<Shot>();
            shot.Vanish();
        }
    }
}