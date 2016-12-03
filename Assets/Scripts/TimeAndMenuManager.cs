using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeAndMenuManager : MonoBehaviour {

    public float timeLeft;
    public Text timerText;
    public bool shouldCountDown = false;
    [SerializeField]
    public GameObject selectionPanel;

    public void selectEasy()
    {
        BoardManager.Instance.StartCoroutine(BoardManager.Instance.IEasyMode());
        selectionPanel.SetActive(false);
        shouldCountDown = true;
    }
    public void selectMedium()
    {
        BoardManager.Instance.StartCoroutine(BoardManager.Instance.IMediumMode());
        selectionPanel.SetActive(false);
        shouldCountDown = true;
    }
    public void selectHard()
    {
        BoardManager.Instance.StartCoroutine(BoardManager.Instance.IHardMode());
        selectionPanel.SetActive(false);
        shouldCountDown = true;
    }
    private static TimeAndMenuManager _Instance;
    public static TimeAndMenuManager Instance
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
