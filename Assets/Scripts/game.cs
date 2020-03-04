using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class game : MonoBehaviour
{
    public float scale = 0.0f;
    public float speed = 0.0f;
    private Rigidbody2D rb2D;
    public bool on_air = true;
    public int score = 0;
    public float gamestart_delay = 0.0f;
    public int combo_ = 1;
    public float combo_speed_limit = 6f;

    public bool gamestart = false;
    public bool gameover = false;
    public float gameover_delay = 0f;
    public bool do_score_plus = true;

    
  
    void Start()
    {
        scale = Screen.height/22.5f;
        rb2D = GetComponent<Rigidbody2D>();
        DOTween.To( get_gamestart, set_gamestart, 5.0f, 2f );
    }
    
    void Update()
    {
        var vel = rb2D.velocity;      
        speed = vel.magnitude;  

        if(gameover && PlayerPrefs.GetInt("click", 0) > 0 && gameover_delay > 4.9f)
        {
            Debug.Log("oyun yeniden başlıyor(restart)");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(score >= 1 && speed < 4f)
        {
            DOTween.To( get_gameover, set_gameover, 5.0f, 3f );
            gameover = true;
            PlayerPrefs.SetInt("gameover", 1);
        }

        if(gamestart == false && PlayerPrefs.GetInt("click", 0) > 0 && gamestart_delay > 4.9f)
        {
            gamestart = true;
            rb2D.isKinematic = false;
            Debug.Log("oyun başladı");
            PlayerPrefs.SetInt("gamestart", 1);
        }
        else if(gamestart == false)
        {
            transform.position = new Vector3(36.93f,2.49f,0f);
            rb2D.isKinematic = true;
        }

        if(transform.position.y > 3f)
        {
            on_air=true;
        }
        else
        {
            on_air=false;
        }

        if(speed < combo_speed_limit)
        {
            combo_ = 1;
        }
        if(on_air && do_score_plus == true)
        {
            Debug.Log("karakter uçuyor");
            score += 1 * combo_;
            if(score > PlayerPrefs.GetInt("score", 0))
            {
                PlayerPrefs.SetInt("score", score);
            }
            if(combo_ >= 2)
            {
                Debug.Log("combo sayısı: "+combo_);
                PlayerPrefs.SetInt("combo_gui", combo_);
            }
            if(combo_ < 4)
            {
                combo_ *= 2;
            }
            do_score_plus = false;
        }
        else if(on_air == false && do_score_plus == false)
        {
            Debug.Log("karakter düşüyor");
            do_score_plus = true;
        }
    }

    void OnGUI()
    {
        // scale = 50 2436x1125
        GUI.skin.label.fontSize = (int)scale;
        
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;

        Vector2 size_score = centeredStyle.CalcSize (new GUIContent (score.ToString()));
        GUI.Label(new Rect(Screen.width/2-(size_score.x/2), Screen.height/2-(scale*7), size_score.x, size_score.y), score.ToString());

        size_score = centeredStyle.CalcSize (new GUIContent ("BEST SCORE: "+PlayerPrefs.GetInt("score", 0).ToString()));
        GUI.Label(new Rect(10f, 10f, size_score.x, size_score.y),"BEST SCORE: "+PlayerPrefs.GetInt("score", 0).ToString());

        if(gamestart == false && gamestart_delay > 4.9f)
        {
            Vector2 size = centeredStyle.CalcSize (new GUIContent ("TAP TO PLAY"));
            GUI.Label(new Rect(Screen.width/2-(size.x/2), Screen.height/2-(scale*2), size.x, size.y), "TAP TO PLAY");
        }
        if(gameover)
        {
            Vector2 size = centeredStyle.CalcSize (new GUIContent (" GAME OVER "));
            GUI.Label(new Rect(Screen.width/2-(size.x/2), Screen.height/2-(scale*4)-size.y, size.x, size.y), "GAME OVER");
        }
        if(gameover_delay > 4.9f)
        {
            Vector2 size = centeredStyle.CalcSize (new GUIContent ("TAP TO CONTINUE"));
            GUI.Label(new Rect(Screen.width/2-(size.x/2), Screen.height/2-(scale*4), size.x, size.y), "TAP TO CONTINUE");
        }
        //GUI.Label(new Rect(200, 200, 200, 200),scale.ToString());
    }

    float get_gameover(){
        return gameover_delay;
    }
    void set_gameover( float new_val ){
        gameover_delay = new_val;
    }

    float get_gamestart(){
        return gamestart_delay;
    }
    void set_gamestart( float new_val ){
        gamestart_delay = new_val;
    }
}
