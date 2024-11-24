using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]private float donusHizi = 100f;

    private void Update(){
        transform.Rotate(0f,0f,donusHizi * Time.deltaTime);
    }
}
