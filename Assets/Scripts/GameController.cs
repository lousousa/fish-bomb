using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameController {

    public static Vector2 playerDirection = Vector2.zero;
    public static bool playerFire = false;
    public static bool playerWasDefeated = false;
    public static Dictionary<int, bool> enemiesActive = new Dictionary<int, bool>();
    public static Dictionary<Vector2, Quaternion> directionsRotations = new Dictionary<Vector2, Quaternion> {
        { Vector2.zero, Quaternion.Euler(0, 0, 0) },
        { Vector2.up, Quaternion.Euler(0, 0, 0) },
        { Vector2.right, Quaternion.Euler(0, 0, -90) },
        { Vector2.down, Quaternion.Euler(0, 0, 180) },
        { Vector2.left, Quaternion.Euler(0, 0, 90) }
    };

}
