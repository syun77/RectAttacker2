using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

    static Vector2 MOVE_LIMIT = new Vector2(2.8f, 4.3f);

    public static Vector3 ClampPosition(Vector3 position) {
        return new Vector3(
            Mathf.Clamp( position.x, -MOVE_LIMIT.x, MOVE_LIMIT.x ),
            Mathf.Clamp( position.y, -MOVE_LIMIT.y, MOVE_LIMIT.y ),
            0
        );
    }

    public static bool OutOfScreen(Vector3 position) {
        if(position.x < -MOVE_LIMIT.x) {
            return true;
        }
        if(position.y < -MOVE_LIMIT.y) {
            return true;
        }
        if(position.x > MOVE_LIMIT.x) {
            return true;
        }
        if(position.y > MOVE_LIMIT.y) {
            return true;
        }

        return false;
    }

    public static void SetVelocity(Rigidbody2D rd, float degree, float speed) {

        float radian = Mathf.Deg2Rad * degree;

        rd.velocity = new Vector2(
            speed * Mathf.Cos(radian),
            speed * Mathf.Sin(radian)
        );
    }
}
