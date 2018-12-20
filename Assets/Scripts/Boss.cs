using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    // ==============================================
    // Variables: Objects.
    public GameObject bullet;
    public GameObject particle;
    public GameObject enemy;
    public Player _player;

    // ==============================================
    // Variables.
    int interval = 0;

    // ==============================================
    // Functions.
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

    private void FixedUpdate() {
        interval++;

        if(interval%60 == 0) {
            float rot = Random.Range(0, 360);
            AddEnemy(Enemy.eId.Zako, rot, 3);
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
