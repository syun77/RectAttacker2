using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // ==============================================
    // Constants.
    public enum eId {
        Zako,
    };

    enum eSize {
        Small,  // 0.2f
        Middle, // 0.4f
        Big,    // 0.8f
    }

    struct Info {
        public eId id;
        public eSize size;
        public int life;
        public float destroy;
        public Info(eId id, eSize size, int life, float destroy) {
            this.id = id;
            this.size = size;
            this.life = life;
            this.destroy = destroy;
        }
    };

    static Info[] InfoTbl = new Info[] {
        new Info (eId.Zako, eSize.Small, 3, 5f),
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
    int life = 0;
    float tDestroy = 0f;

    // ==============================================
    /// <summary>
    /// Init the specified id, degree, speed and player.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="degree">Degree.</param>
    /// <param name="speed">Speed.</param>
    /// <param name="player">Player.</param>
    public void Init(eId id, float degree, float speed, Player player) {

        this.id = id;
        _player = player;

        _rigidbody2D = GetComponent<Rigidbody2D>();
        Utils.SetVelocity(_rigidbody2D, degree, speed);

        Info info = _GetInfo(id);
        float size = _GetSize(info.size);
        transform.localScale = new Vector3(size, size, 1);

        // Set life.
        life = info.life;

        // Set time for suicide.
        tDestroy = info.destroy;
    }

    Info _GetInfo(eId id) {
        foreach(Info info in InfoTbl) {
            if(id == info.id) {
                return info;
            }
        }

        return InfoTbl[0];
    }

    // Use this for initialization
    void Start () {
        _rigidbody2D = GetComponent<Rigidbody2D>();
	}

    /// <summary>
    /// bullet.
    /// </summary>
    /// <param name="degree">Degree.</param>
    /// <param name="speed">Speed.</param>
    /// <param name="msWait">Ms wait.</param>
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
        float aim = _GetAim();
        DoBullet(aim, speed, msWait);
    }

    void DoBulletNWay(int nway, float degree, float range, float speed, float mswait=0f) {
        if(nway <= 1) {
            nway = 2;
        }
        float rot = degree - range / 2;
        float dRot = range / (nway - 1);
        for (int i = 0; i < nway; i++) {
            DoBullet(rot, speed, mswait);
            rot += dRot;
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update () {
	}

    /// <summary>
    /// Fixed update.
    /// </summary>
    private void FixedUpdate() {
        _Move();
        _Bullet();

        tDestroy -= Time.deltaTime;
        if(tDestroy <= 0) {
            // Go to suicide.
            Vanish();
        }
    }

    /// <summary>
    /// Move this instance.
    /// </summary>
    void _Move() {
        float vx = _rigidbody2D.velocity.x;
        float vy = _rigidbody2D.velocity.y;

        float friction = 1.0f;

        switch(id) {
        case eId.Zako:
            friction = 0.97f;
            break;

        default:
            break;
        }
        vx *= friction;
        vy *= friction;

        _rigidbody2D.velocity = new Vector2(vx, vy);
    }

    /// <summary>
    /// Bullet this instance.
    /// </summary>
    void _Bullet() {
        interval++;
        switch (id) {
        case eId.Zako:
            if(interval < 60) {
                break;
            }
            if(interval%70 == 1) {
                float aim = _GetAim();
                DoBulletNWay(3, aim, 5, 1);
            }
            break;

        default:
            if (interval % 60 == 1) {
                for (int i = 0; i < 6; i++) {
                    float msWait = 100 * i;
                    DoBulletAim(2 + 0.5f * i, msWait);
                }
            }
            break;
        }
    }

    float _GetAim() {
        float aim = Utils.GetAim2D(this.gameObject, _player.gameObject);
        return aim;
    }

    void _Damage(int val) {
        life -= val;
        if(life < 1) {
            Vanish();
        }
    }

    public void Vanish() {
        Info info = _GetInfo(id);
        float scale = _GetSize(info.size) * 10f;
        Particle.Add(particle, transform.position.x, transform.position.y, 4, -1, Color.green, scale);
        Destroy(gameObject);
    }

    /// <summary>
    /// trigger enter2D.
    /// </summary>
    /// <param name="collision">Collision.</param>
    private void OnTriggerEnter2D(Collider2D collision) {
        switch(collision.tag) {
        case "Shot":
            {
                Shot shot = collision.gameObject.GetComponent<Shot>();
                shot.Vanish();
            }
            _Damage(1);
            break;

        case "Horming": {
                Homing homing = collision.gameObject.GetComponent<Homing>();
                homing.Vanish();
            }
            _Damage(10);
            break;
        }
    }

    float _GetSize(eSize Size) {
        switch(Size) {
        case eSize.Small:
            return 0.2f;
        case eSize.Middle:
            return 0.4f;
        default:
            return 0.8f;
        }
    }
}
