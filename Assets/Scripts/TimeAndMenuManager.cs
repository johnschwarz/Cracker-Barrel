using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class TimeAndMenuManager : MonoBehaviour
{

    public float timeLeft;
    public Text timerText;
    public bool shouldCountDown = false;
    [SerializeField]
    public GameObject selectionPanel;

    public Text pegCount;

    public GameObject infoPanel;

    Vector3 cameraStart;

    void Start()
    {
        cameraStart = Camera.main.transform.position;
    }

    IEnumerator IMoveCamera ( float startSize, float endSize, Vector3 startPos, Vector3 endPos, float time)
    {
     
       

        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            Camera.main.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / time);
            Camera.main.orthographicSize = Mathf.Lerp(startSize, endSize, elapsedTime/time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (infoPanel.active.Equals(false))
        {
            infoPanel.SetActive(true);
        }
        else 
        {
            infoPanel.SetActive(false);
            timerText.text = "";
        }

    }
    
    public void selectEasy()
    {
        BoardManager.Instance.StartCoroutine(BoardManager.Instance.IEasyMode());
        selectionPanel.SetActive(false);
        shouldCountDown = true;
        StartCoroutine(IMoveCamera(51, 32, cameraStart, new Vector3(-10, -5, -10), 1f));
    }
    public void selectMedium()
    {
        BoardManager.Instance.StartCoroutine(BoardManager.Instance.IMediumMode());
        selectionPanel.SetActive(false);
        shouldCountDown = true;
        StartCoroutine(IMoveCamera(51, 32, cameraStart, new Vector3(-10, -5, -10), 1f));
    }
    public void selectHard()
    {
        BoardManager.Instance.StartCoroutine(BoardManager.Instance.IHardMode());
        selectionPanel.SetActive(false);
        shouldCountDown = true;
        StartCoroutine(IMoveCamera(51, 32, cameraStart, new Vector3(-10, -5, -10), 1f));
    }
    public void returnToMenu()
    {
        shouldCountDown = false;
        timeLeft = 180;
        for (int i = 0; i < Grid.Instance.Holes.Count; i++)
        {
            Grid.Instance.Holes.ElementAt(i).Value.hasPeg = false;
            Grid.Instance.Holes.ElementAt(i).Value.CheckForPeg();
            Grid.Instance.Holes.ElementAt(i).Value.ReturnPegsToDefault();
        }
        selectionPanel.SetActive(true);
        StartCoroutine(IMoveCamera(32, 51, new Vector3(-10, -5, -10), cameraStart, 0.5f));
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

            string timeToDisplay = string.Format("{0:0}: {1:00}", mintutes, seconds);

            timerText.text = timeToDisplay;
        }
    }

    public int CountPegs()
    {
        int amount = 0;
        for (int i = 0; i < Grid.Instance.Holes.Count; i++)
        {
            if (Grid.Instance.Holes.ElementAt(i).Value.hasPeg == true)
            {
                amount++;
            }

        }
        pegCount.text = amount.ToString();
        return amount;
    }

}
