using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    bool fireBlocked = false;

    void Update () {

        if (Input.GetKey("up")) {

            GameController.playerDirection = Vector2.up;

        } else if (Input.GetKey("right")) {

            GameController.playerDirection = Vector2.right;

        } else if (Input.GetKey("down")) {

            GameController.playerDirection = Vector2.down;

        } else if (Input.GetKey("left")) {

            GameController.playerDirection = Vector2.left;

        }

        if (Input.GetButton("Jump")) {
            if (!fireBlocked) {
                Invoke("UnblockFire", .75f);
                fireBlocked = true;
                GameController.playerFire = true;
            }
        }

    }

    private void UnblockFire() {
        fireBlocked = false;
    }

}
