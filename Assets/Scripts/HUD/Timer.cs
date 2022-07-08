using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Color CommonColor;

    public Color EndingColor;

    public float endingTime;

    private Text _text;

    // Start is called before the first frame update
    void Start() 
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void OnGUI() 
    {
        float time = GameManager.Instance?.RemainTime ?? 0.0f;
        _text.color = time < endingTime ? EndingColor : CommonColor;
        
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        _text.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}
