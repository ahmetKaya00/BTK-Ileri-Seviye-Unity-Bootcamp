using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveTouch : MonoBehaviour
{
    private bool isDragging = false;

    Vector3 offset;

    float speed = 5f;

    private void Update(){

        if(Input.touchCount > 0) {

            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began){
                offset = Camera.main.ScreenToWorldPoint(touch.position);
                offset.z = 0;
                isDragging = true;
            }
        }
        if(isDragging) {
            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, offset, step);

            if(transform.position == offset){
                isDragging = false;
            }
        }
    }
}
