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
            collider.gameObject.transform.position = transform.position + new Vector3(0f,13f,0f);
            bir.gameObject.transform.position = transform.position + new Vector3(0f,10f,0f);
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
            collider.gameObject.transform.position = transform.position + new Vector3(0f,13f,0f);
            iki.gameObject.transform.position = transform.position + new Vector3(0f,10f,0f);
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
                sr.color = new Color(ColorTur.r,ColorTur.g,ColorTur.b,1f);
                break;
            case 1:
                CurrentColor = "Sari";
                sr.color = new Color(ColorSari.r,ColorSari.g,ColorSari.b,1f);
                break;
            case 2:
                CurrentColor = "Kirmizi";
                sr.color = new Color(ColorKir.r,ColorKir.g,ColorKir.b,1f);
                break;
            case 3:
                CurrentColor = "Mor";
                sr.color= new Color(ColorMor.r,ColorMor.g,ColorMor.b,1f);
                break;
        }
    }
}
