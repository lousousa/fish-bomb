using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rgdb;
    Rigidbody2D[] rgdbChildren;
    SpriteRenderer rend;
    int lastBulletId;
    int maxBulletId;
    AudioSource shot;

    void Start () {
        GameController.playerDirection = Vector2.zero;
        rgdb = GetComponent<Rigidbody2D>();
        rgdbChildren = GetComponentsInChildren<Rigidbody2D>();
        maxBulletId = rgdbChildren.Length - 1;
        lastBulletId = maxBulletId;
        foreach (Rigidbody2D r in rgdbChildren) r.gameObject.SetActive(false);
        rgdb.gameObject.SetActive(true);
        rend = GetComponent<SpriteRenderer>();
        shot = GetComponent<AudioSource>();
    }

    int GetBulletID() {
        lastBulletId = lastBulletId == maxBulletId ? 1 : lastBulletId + 1;
        return lastBulletId;
    }

    bool startNextLevel = false;
	void Update () {

        if (GameController.playerWasDefeated || GameController.enemiesActive.Count == 0) {

            GameController.enemiesActive = new Dictionary<int, bool>(); 
            
            rgdb.velocity = Vector2.zero;
            if (!startNextLevel) {
                if (GameController.playerWasDefeated) {
                    AudioSource explosion = Camera.main.GetComponent<AudioSource>();
                    if (!explosion.isPlaying) explosion.Play();
                }
                startNextLevel = true;
                NextLevel();
            }

        } else {

            rgdb.velocity = GameController.playerDirection * 2;
            transform.rotation = GameController.directionsRotations[GameController.playerDirection];

            if (GameController.playerFire) {
                GameController.playerFire = false;
                shot.Play();
                int bulletId = GetBulletID();
                BulletController bulletCtrl = rgdbChildren[bulletId].GetComponent<BulletController>();
                Vector2 bulletDirection = GameController.playerDirection == Vector2.zero ? Vector2.up : GameController.playerDirection;
                bulletCtrl.Resume(bulletDirection, transform.position, 0);
            }
        }

    }

    int flashCounter = 12;
    void NextLevel () {
        if (flashCounter == 0) {
            if (GameController.playerWasDefeated) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            else {
                int nextId = SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1 ? 0 : SceneManager.GetActiveScene().buildIndex + 1;
                SceneManager.LoadScene(nextId);
            }
            GameController.playerWasDefeated = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else {
            flashCounter--;
            if (GameController.playerWasDefeated) rend.enabled = !rend.enabled;
            Invoke("NextLevel", .125f);
        }
    }

}
