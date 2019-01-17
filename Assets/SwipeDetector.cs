using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour {

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public void SwipeUp() {
        GameController.playerDirection = Vector2.up;
    }
    public void SwipeRight() {
        GameController.playerDirection = Vector2.right;
    }
    public void SwipeDown() {
        GameController.playerDirection = Vector2.down;
    }
    public void SwipeLeft() {
        GameController.playerDirection = Vector2.left;
    }

    public void FingerDown() {
        firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    public void FingerUp() {
        secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
        currentSwipe.Normalize();

        if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) SwipeUp();
        if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) SwipeRight();
        if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) SwipeDown();
        if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) SwipeLeft();
    }

    public void Swipe() {
        if (Input.touches.Length > 0) {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began) FingerDown();
            if (t.phase == TouchPhase.Ended) FingerUp();
        } else {
            if (Input.GetMouseButtonDown(0)) FingerDown();
            if (Input.GetMouseButtonUp(0)) FingerUp();
        }
    }

    public void Update() {
        Swipe();
    }
}
