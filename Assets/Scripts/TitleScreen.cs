using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {

    Text text;

    void Start() {
        text = GetComponentInChildren<Text>();
        FlashText();
    }

    bool textEnabled = true;
    void FlashText() {
        text.enabled = !text.enabled;
        Invoke("FlashText", .5f);
    }

    public void OnClickStart() {
        // SceneManager.LoadScene(1);
    }

    private void Update() {
        if (Input.GetButton("Fire3")) {
            SceneManager.LoadScene(1);
        }
    }

}
