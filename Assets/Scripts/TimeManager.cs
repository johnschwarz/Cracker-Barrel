using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public float timeLeft;
    public Text timerText;
    public bool shouldCountDown;

    private static TimeManager _Instance;
    public static TimeManager Instance
    {
        get { return _Instance; }
    }
    void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (shouldCountDown)
        {
            timeLeft -= Time.deltaTime;
            int mintutes = Mathf.FloorToInt(timeLeft / 60f);
            int seconds = Mathf.FloorToInt(timeLeft - mintutes * 60);

            string timeToDisplay = string.Format("{0:0}: {1:00}", mintutes,seconds);

            timerText.text = timeToDisplay;
        }
    }

}
