﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // ==============================================
    // Constants.
    public enum eId {
        Zako,
    };

    // ==============================================
    // Variables: Objects.
    public GameObject bullet;
    public GameObject particle;
    Rigidbody2D _rigidbody2D;
    Player _player;

    // ==============================================
    // Variables.
    eId id = eId.Zako;
    int interval = 0;

    // ==============================================
    public void Init(eId id, float degree, float speed, Player player) {

        this.id = id;
        _player = player;

        _rigidbody2D = GetComponent<Rigidbody2D>();
        Utils.SetVelocity(_rigidbody2D, degree, speed);

        transform.localScale = new Vector3(0.5f, 0.5f, 1);

        Destroy(gameObject, 3);
    }

    // Use this for initialization
    void Start () {
        _rigidbody2D = GetComponent<Rigidbody2D>();
	}

    void DoBullet(float degree, float speed, float msWait=0f) {
        StartCoroutine(DoBulletCoroutine(degree, speed, msWait));
    }

    IEnumerator DoBulletCoroutine(float degree, float speed, float msWait) {
        if(msWait > 0f) {
            // Wait
            yield return new WaitForSeconds(msWait * 0.001f);
        }

        Vector3 v = new Vector3(transform.position.x, transform.position.y);
        GameObject obj = Instantiate(bullet, v, Quaternion.identity);
        Bullet b = obj.GetComponent<Bullet>();
        b.SetVelocity(degree, speed);
    }

    void DoBulletAim(float speed, float msWait=0f) {
        float aim = Utils.GetAim2D(this.gameObject, _player.gameObject);
        DoBullet(aim, speed, msWait);
    }


    // Update is called once per frame
    void Update () {
	}

    private void FixedUpdate() {

        {
            float vx = _rigidbody2D.velocity.x * 0.97f;
            float vy = _rigidbody2D.velocity.y * 0.97f;
            _rigidbody2D.velocity = new Vector2(vx, vy);
        }

        interval++;
        if(interval%60 == 1) {
            for (int i = 0; i < 6; i++) {
                float msWait = 100 * i;
                DoBulletAim(2 + 0.5f * i, msWait);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch(collision.tag) {
        case "Shot":
            {
                Shot shot = collision.gameObject.GetComponent<Shot>();
                shot.Vanish();
            }
            break;

        case "Horming": {
                Homing homing = collision.gameObject.GetComponent<Homing>();
                homing.Vanish();
            }
            break;
        }
    }
}
