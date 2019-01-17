using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour {

    bool blocked = false;

    private void OnMouseDown() {
        if (!blocked) {
            Invoke("Unblock", .75f);
            blocked = true;
            GameController.playerFire = true;
        }
    }

    private void Unblock () {
        blocked = false;
    }

}
