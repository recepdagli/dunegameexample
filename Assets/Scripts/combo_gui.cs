using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combo_gui : MonoBehaviour
{
    public float time;
    public int combo_;
    public bool time_plus = false;
    public bool print_combo = false;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("combo_gui", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("combo_gui", 0) > 0)
        {
            combo_ = PlayerPrefs.GetInt("combo_gui", 0);
            print_combo = true;
            time_plus = true;
            PlayerPrefs.SetInt("combo_gui", 0);
        }
        if(time_plus && time < 1f)
        {
            time += Time.deltaTime;
        }
        else if(time_plus && time > 1f)
        {
            combo_ = 0;
            time = 0f;
            print_combo = false;
            time_plus = false;
        }
        if(time_plus && print_combo && PlayerPrefs.GetInt("combo_gui", 0) > 0)
        {
            combo_ = 0;
            time = 0f;
            print_combo = false;
            time_plus = false;
        }
    }

    void OnGUI()
    {
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;

        if(print_combo)
        {
            Vector2 size = centeredStyle.CalcSize (new GUIContent ("COMBO " +combo_.ToString()));
            GUI.Label(new Rect(Screen.width/2-(size.x/2), Screen.height/2-((Screen.height/22.5f)*2), size.x, size.y),"COMBO " +combo_.ToString());
            //Screen.height/22.5f scale
        }
    }
}
