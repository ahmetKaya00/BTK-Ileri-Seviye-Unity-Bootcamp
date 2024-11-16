using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    void Update()
    {
        if(Input.touchCount > 0){

            Touch touch = Input.GetTouch(0);
 
            switch(touch.phase){
                case TouchPhase.Began:
                      Debug.Log("Dokunma başladı!." + touch.position);
                      break;
                case TouchPhase.Moved:
                      Debug.Log("Dokunma hareket etti" + touch.position);
                      break;
                case TouchPhase.Stationary:
                      Debug.Log("Dokunma sabit" + touch.position); 
                      break;
                case TouchPhase.Ended:
                      Debug.Log("Dokunma Bitti") ;
                      break;
                case TouchPhase.Canceled:
                      Debug.Log("Dokunma iptal edildi");
                      break;
            }

            float screenWitdh = Screen.width;
            float screenHitdh = Screen.height;

            if(touch.position.x < screenWitdh / 2){
                Debug.Log("Sol tarafa tıklandı");
            }
            else{
                Debug.Log("Sağ tarafa tıklandı");
            }
            if(touch.position.y < screenHitdh / 2){
                Debug.Log("Alt tarafa tıklandı");
            }
            else {
                Debug.Log("Üst tarafa tıklandı");
            }
        }
    }
}
