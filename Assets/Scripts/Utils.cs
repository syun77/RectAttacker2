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
        const float MARGIN = 0.5f;
        if(position.x < -MOVE_LIMIT.x-MARGIN) {
            return true;
        }
        if(position.y < -MOVE_LIMIT.y) {
            return true;
        }
        if(position.x > MOVE_LIMIT.x+MARGIN) {
            return true;
        }
        if(position.y > MOVE_LIMIT.y+MARGIN) {
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

    public static Vector2 MakeVelocity(float degree, float speed) {
        float radian = Mathf.Deg2Rad * degree;
        return new Vector2(
            speed * Mathf.Cos(radian),
            speed * Mathf.Sin(radian)
        );
    }

    public static float GetDegree(Rigidbody2D rd) {
        return Mathf.Atan2(rd.velocity.y, rd.velocity.x) * Mathf.Rad2Deg;
    }

    public static float GetAim2D(GameObject a, GameObject b) {
        Vector3 d = b.transform.position - a.transform.position;
        return Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
    }

    public static float DeltaAngle(float a, float b) {
        float sub = b - a;
        sub -= Mathf.Floor(sub / 360.0f) * 360.0f;
        if(sub > 180.0f) {
            sub -= 360.0f;
        }

        return sub;
    }
}
