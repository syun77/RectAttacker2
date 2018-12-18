using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    // ======================================
    // Constants.
    public float MOVE_SPEED;
    public float REFLECT_OFFSET;

    // Constants: Powers.
    public int MAX_POWER;
    public int INITIALE_POWER;
    public int DECREASE_SHOT;
    public int DECREASE_SHIELD;

    // ======================================
    // Variables. Objects.
    public GameObject _shot;
    public GameObject _horming;
    public Text _textShot;
    public Text _textShield;
    Reflect _reflect;
    Rigidbody2D _rigidbody2D;

    // ======================================
    // Variables.
    bool _enableShot = false;
    int _shotPower;
    int _shieldPower;
    int _shotInterval = 0;

    // ======================================
    // Functions.
    // Use this for initialization
    void Start () {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _shotPower = INITIALE_POWER;
        _shieldPower = INITIALE_POWER;
	}

    /// <summary>
    /// Get the reflect.
    /// </summary>
    /// <returns>The reflect.</returns>
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
        _UpdateUI();
    }

    /// <summary>
    /// Fixed update.
    /// </summary>
    private void FixedUpdate() {
        // Check to fire a shot.
        _UpdateShot();

        // Check to use a shield.
        UpdateReflect();
    }

    /// <summary>
    /// Move this instance.
    /// </summary>
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

    /// <summary>
    /// Update the reflect.
    /// </summary>
    void UpdateReflect() {

        Reflect r = _GetReflect();
        if(r == null) {
            return;
        }

        bool b = Input.GetKey(KeyCode.X);
        if (b == false) {
            _shieldPower += 5;
            if (_shieldPower > MAX_POWER) {
                _shieldPower = MAX_POWER;
            }
        }
        if (_shieldPower <= 0) {
            b = false;
        }

        if (b) {
            r.SetVisible(true);

            _shieldPower -= DECREASE_SHIELD;
            if(_shieldPower < 0) {
                _shieldPower = 0;
            }
        }
        else {
            r.SetVisible(false);
        }

        float x = transform.position.x;
        float y = transform.position.y;
        y += REFLECT_OFFSET;
        r.SetPosition(x, y);
    }

    /// <summary>
    /// Get the shot ratio.
    /// </summary>
    /// <returns>The shot ratio.</returns>
    float _GetShotRatio() {
        return 1.0f * _shotPower / MAX_POWER;
    }

    /// <summary>
    /// Update the shot.
    /// </summary>
    void _UpdateShot() {

        // Increase power.
        _shotPower += 5;
        if (_shotPower > MAX_POWER) {
            _shotPower = MAX_POWER;
        }
        _enableShot = Input.GetKey(KeyCode.Z);
        if(_enableShot) {
            _shotPower -= DECREASE_SHOT;
            if (_shotPower < 0) {
                _shotPower = 0;
            }
        }

        if (_shotPower <= 0) {
            _enableShot = false;
        }
        if(_shotInterval > 0) {
            _shotInterval--;
            _enableShot = false;
        }

        if (_enableShot) {
            Vector3 v = new Vector3(transform.position.x, transform.position.y);
            GameObject obj = Instantiate(_shot, v, Quaternion.identity);
            Shot shot = obj.GetComponent<Shot>();
            float degree = 90;
            degree += Random.Range(-10, 10);
            shot.SetVelocity(degree, shot.MOVE_SPEED);

            float rate = _GetShotRatio();
            if(rate < 0.2f) {
                _shotInterval = 5;
            }
            else if(rate < 0.5f) {
                _shotInterval = 4;
            }
            else if (rate < 0.75f) {
                _shotInterval = 3;
            }
            else if (rate < 0.9f) {
                _shotInterval = 1;
            }
        }
    }

    /// <summary>
    /// Update the user interface.
    /// </summary>
    void _UpdateUI() {
        _textShot.text = "Shot: " + (100 * _shotPower / MAX_POWER) + "%";
        _textShield.text = "Shield: " + (100 * _shieldPower / MAX_POWER) + "%";
    }

    /// <summary>
    /// Trigger enter2D.
    /// </summary>
    /// <param name="collision">Collision.</param>
    private void OnTriggerEnter2D(Collider2D collision) {
        switch(collision.tag) {
        case "Bullet":
            break;

        case "Enemy":
            break;
        }
    }
}
