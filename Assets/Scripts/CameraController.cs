using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    MusicController musicCtrl;
    void Start() {
        if (GameObject.Find("Music")) {
            musicCtrl = GameObject.Find("Music").GetComponentInChildren<MusicController>();
            musicCtrl.PlayMusic();
        }
	}

}
