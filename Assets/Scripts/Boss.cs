using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    // ==============================================
    // Constants.
    enum eMode {
        A,
        B,
        C,
    }

    // ==============================================
    // Variables: Objects.
    public GameObject bullet;
    public GameObject particle;
    public GameObject enemy;
    public Player _player;

    // ==============================================
    // Variables.
    int interval = 0;
    int timer = 0;

    // ==============================================
    // Functions.
    eMode GetMode() {
        return eMode.A;
    }

    // Use this for initialization
    void Start () {
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

    void AddEnemy(Enemy.eId id, float degree, float speed) {
        if(_player == null) {
            GameObject obj = GameObject.Find("Player");
            _player = obj.GetComponent<Player>();
        }

        {
            Vector3 v = new Vector3(transform.position.x, transform.position.y);
            GameObject obj = Instantiate(enemy, v, Quaternion.identity);
            Enemy e = obj.GetComponent<Enemy>();
            e.Init(id, degree, speed, _player);
        }
    }

    void _UpdateA() {
        timer++;
        if(timer < 120) {
            if(40 < timer && timer < 50) {
                if (timer % 3 == 0) {
                    float rot = 90 + Random.Range(-45, 45);
                    AddEnemy(Enemy.eId.Zako, rot, 3);
                }
            }
        }
        switch(timer) {
        case 120:
            AddEnemy(Enemy.eId.Aim, 270-45, 3);
            AddEnemy(Enemy.eId.Aim, 270+45, 3);
            break;

        case 240:
            AddEnemy(Enemy.eId.SideL, 180, 3);
            AddEnemy(Enemy.eId.SideR, 0, 3);
            break;
        }
    }

    private void FixedUpdate() {
        interval++;

        switch(GetMode()) {
        case eMode.A:
            _UpdateA();
            break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag) {
        case "Shot": {
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
