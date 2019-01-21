using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    Rigidbody2D rgdb;
    Vector2 direction;
    int bulletType; // 0 = player; 1 = enemy;

    private void Start() {
        rgdb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Vector2 pos = transform.position;
        float screenLimits = 20;
        if (GameController.playerWasDefeated || GameController.enemiesActive.Count == 0) {
            rgdb.velocity = Vector2.zero;
        } else {
            rgdb.velocity = pos.x < screenLimits && pos.x > -screenLimits && pos.y < screenLimits && pos.y > -screenLimits ?
                direction * 4f : Vector2.zero;
        }
    }

    public void Resume(Vector2 dir, Vector2 pos, int type) {
        bulletType = type;
        direction = dir;
        transform.position = pos;
        transform.parent = null;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (bulletType == 0 && collision.CompareTag("Enemy")) {
            EnemyController ctrl = collision.gameObject.GetComponent<EnemyController>();
            if (ctrl.IsActive()) {
                ctrl.Deactivate();
                Camera.main.GetComponent<CameraShake>().shakeDuration = .2f;
            }
        } else if (bulletType == 1 && collision.CompareTag("Player")) {
            GameController.playerWasDefeated = true;
            Camera.main.GetComponent<CameraShake>().shakeDuration = .2f;
        }
    }


}
