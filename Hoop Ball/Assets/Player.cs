using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float ziplama = 10f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public string CurrentColor;
    public Color ColorTur, ColorSari,ColorKir,ColorMor;

    public Text score, PScore, PHScore;
    public int deger, number;

    public GameObject bir,iki,Panel;

    private void Start(){
        rb = GetComponent<Rigidbody2D> ();
        sr = GetComponent<SpriteRenderer> ();

        rb.bodyType = RigidbodyType2D.Static;
        Panel.SetActive (false);
        RandomColor();
        PHScore.text = PlayerPrefs.GetInt ("HighScore", 0).ToString();
    }
    private void Update(){
        score.text = deger.ToString("f0");
        if(Input.GetButtonDown ("Jump") || Input.GetMouseButtonDown (0) || Input.touchCount > 0) {
            rb.velocity = Vector2.up * ziplama;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "ColorChanger"){
            RandomColor();
            collider.gameObject.transform.position = transform.position + new Vector3(0f,11f,0f);
            bir.gameObject.transform.position = transform.position + new Vector3(0f,16f,0f);
            deger++;
            number++;
            PScore.text = number.ToString();
            if(number > PlayerPrefs.GetInt ("HighScore", 0)){
                PlayerPrefs.SetInt ("HighScore", number);
                PHScore.text = number.ToString();
            }
            return;
        }
        if(collider.tag == "Respawn"){
            RandomColor();
            collider.gameObject.transform.position = transform.position + new Vector3(0f,11f,0f);
            bir.gameObject.transform.position = transform.position + new Vector3(0f,16f,0f);
            deger++;
            number++;
            PScore.text = number.ToString();
            if(number > PlayerPrefs.GetInt ("HighScore", 0)){
                PlayerPrefs.SetInt ("HighScore", number);
                PHScore.text = number.ToString();
            }
            return;
        }
        if (collider.tag != CurrentColor){
            Panel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void RandomColor(){
        int index = Random.Range(0, 4);

        switch(index){
            case 0:
                CurrentColor = "Turkuaz";
                sr.color = ColorTur;
                break;
            case 1:
                CurrentColor = "Sari";
                sr.color = ColorSari;
                break;
            case 2:
                CurrentColor = "Kirmizi";
                sr.color = ColorKir;
                break;
            case 3:
                CurrentColor = "Mor";
                sr.color= ColorMor;
                break;
        }
    }
}
