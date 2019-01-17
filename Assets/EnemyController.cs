using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Vector2 direction;
    Rigidbody2D rgdb;
    Rigidbody2D[] rgdbChildren;
    SpriteRenderer rend;
    int lastBulletId;
    int maxBulletId;
    bool active;
    GameObject player;

    void Start () {
        RefreshDirection();
        rgdb = GetComponent<Rigidbody2D>();
        rgdbChildren = GetComponentsInChildren<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        maxBulletId = rgdbChildren.Length - 1;
        lastBulletId = maxBulletId;
        foreach (Rigidbody2D r in rgdbChildren) r.gameObject.SetActive(false);
        rgdb.gameObject.SetActive(true);
        Invoke("Fire", 1);
        active = true;
        GameController.enemiesActive[GetInstanceID()] = true;
        player = GameObject.Find("Player");
    }

    int GetBulletID() {
        lastBulletId = lastBulletId == maxBulletId ? 1 : lastBulletId + 1;
        return lastBulletId;
    }

    void Update () {
        if (active) {
            rgdb.velocity = GameController.playerWasDefeated ? Vector2.zero : direction * 2;
            if (!GameController.playerWasDefeated) transform.rotation = GameController.directionsRotations[direction];
        } else rgdb.velocity = Vector2.zero;
    }

    void Fire() {
        if (GameController.playerWasDefeated || !active) return;
        int i = Random.Range(0, 2);
        if (i == 0) {
            int bulletId = GetBulletID();
            BulletController ctrl = rgdbChildren[bulletId].GetComponent<BulletController>();
            ctrl.Resume(direction, transform.position, 1);
        }
        Invoke("Fire", 1);
    }

    void RefreshDirection() {

        Vector2 pos = transform.position;
        Vector2 ppos = Vector2.zero;
        if (player) ppos = player.transform.position;

        float x = pos.x > ppos.x ? -1 : 1;
        float y = pos.y > ppos.y ? -1 : 1;
        int i = Random.Range(0, 2);
        direction = i == 0 ? new Vector2(x, 0) : new Vector2(0, y);

        Invoke("RefreshDirection", 2);
    }

    public bool IsActive() {
        return active;
    }

    public void Deactivate() {
        rend.enabled = false;
        active = false;
        AudioSource explosion = Camera.main.GetComponent<AudioSource>();
        if (!explosion.isPlaying) explosion.Play();
        GameController.enemiesActive.Remove(GetInstanceID());
    }

}
