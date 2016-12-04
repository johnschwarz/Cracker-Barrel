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

    public Text titleText;
    public Text howToText;

    public Text pegCount;

    public GameObject infoPanel;
    public GameObject difficultyPanel;

    Vector3 cameraStart;

    public bool isPlaying = false;

    private bool isMoving = false;

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

    void Start()
    {
        cameraStart = Camera.main.transform.position;
    }

    IEnumerator IMoveCamera(float startSize, float endSize, Vector3 startPos, Vector3 endPos, float time, bool shouldTurnInfoOn)
    {
        if (isMoving == false)
        {
            isMoving = true;
        }
        if (isMoving)
        {
            float elapsedTime = 0;
            while (elapsedTime < time)
            {
                Camera.main.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / time);
                Camera.main.orthographicSize = Mathf.Lerp(startSize, endSize, elapsedTime / time);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            infoPanel.SetActive(shouldTurnInfoOn);


            if (shouldTurnInfoOn == false)
            {
                //time//time
            }
            isMoving = false;
        }
    }

    public void selectEasy()
    {
        timeLeft = 180;
        BoardManager.Instance.StartCoroutine(BoardManager.Instance.IEasyMode());
        selectionPanel.SetActive(false);
        shouldCountDown = true;
        StartCoroutine(IMoveCamera(51, 32, cameraStart, new Vector3(-10, -5, -10), 1f, true));
        isPlaying = true;
    }
    public void selectMedium()
    {
        timeLeft = 180;
        BoardManager.Instance.StartCoroutine(BoardManager.Instance.IMediumMode());
        selectionPanel.SetActive(false);
        shouldCountDown = true;
        StartCoroutine(IMoveCamera(51, 32, cameraStart, new Vector3(-10, -5, -10), 1f, true));
        isPlaying = true;
    }
    public void selectHard()
    {
        timeLeft = 180;
        BoardManager.Instance.StartCoroutine(BoardManager.Instance.IHardMode());
        selectionPanel.SetActive(false);
        shouldCountDown = true;
        StartCoroutine(IMoveCamera(51, 32, cameraStart, new Vector3(-10, -5, -10), 1f, true));
        isPlaying = true;
    }
    public void returnToMenu()
    {
        if (!BoardManager.Instance.isHeld)
        {
            isPlaying = false;
            shouldCountDown = false;
            timerText.text = "";
            timeLeft = 180;
            for (int i = 0; i < Grid.Instance.Holes.Count; i++)
            {
                Grid.Instance.Holes.ElementAt(i).Value.hasPeg = false;
                Grid.Instance.Holes.ElementAt(i).Value.CheckForPeg();
                Grid.Instance.Holes.ElementAt(i).Value.ReturnPegsToDefault();
            }
            selectionPanel.SetActive(true);
            titleText.text = "Cracker Barrel \nPeg! ";
            howToText.text = "Jump   the   pegs \nLeave   only   1";
            StartCoroutine(IMoveCamera(32, 51, new Vector3(-10, -5, -10), cameraStart, 0.5f, false));
        }
    }
   

    void Update()
    {
        CountdownTimer();
    }

    void CountdownTimer()
    {
        if (shouldCountDown)
        {
            timeLeft -= Time.deltaTime;
            int mintutes = Mathf.FloorToInt(timeLeft / 60f);
            int seconds = Mathf.FloorToInt(timeLeft - mintutes * 60);
            string timeToDisplay = string.Format("{0:0}: {1:00}", mintutes, seconds);
            timerText.text = timeToDisplay;
            if (timeLeft <= 0)
            {
                StartCoroutine(ILose());
                
            }
        }
    }

   public IEnumerator ILose()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.winMusic);
        StartCoroutine(IMoveCamera(32, 51, new Vector3(-10, -5, -10), cameraStart, 2.5f, false));
        shouldCountDown = false;
        infoPanel.SetActive(false);
        timerText.text = "0:00";
        yield return new WaitForSeconds(0.1f);
        selectionPanel.SetActive(true);
        difficultyPanel.SetActive(false);
        titleText.text = "Timeup!";
        howToText.text = "";
        yield return new WaitForSeconds(1.1f);
        titleText.text = "Why   not   try   again?";
        yield return new WaitForSeconds(1.1f);
        titleText.text = "But   on   an   easier   difficulty!";
        yield return new WaitForSeconds(1.1f);
        for (int i = 0; i < Grid.Instance.Holes.Count; i++)
        {
            Grid.Instance.Holes.ElementAt(i).Value.hasPeg = false;
        }
        isPlaying = false;
        timeLeft = 1;
        difficultyPanel.SetActive(true);
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
        if (amount == 1 && isPlaying)
        {
            StartCoroutine(IWin());
            amount = 0;
            pegCount.text = "";
        }
        //StartCoroutine(BoardManager.Instance.ICheckForLoss());
        return amount;
    }

    IEnumerator IWin()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.winMusic);
        StartCoroutine(IMoveCamera(32, 51, new Vector3(-10, -5, -10), cameraStart, 2.5f, false));
        shouldCountDown = false;
        infoPanel.SetActive(false);
        timerText.text = "";
        yield return new WaitForSeconds(0.1f);
        selectionPanel.SetActive(true);
        difficultyPanel.SetActive(false);
        titleText.text = "You've   won!";
        howToText.text = "";
        yield return new WaitForSeconds(1.1f);
        titleText.text = "Why   not   try   again?";
        yield return new WaitForSeconds(1.1f);
        titleText.text = "But   on   a   harder   difficulty!";
        yield return new WaitForSeconds(1.1f);
        for (int i = 0; i < Grid.Instance.Holes.Count; i++)
        {
            Grid.Instance.Holes.ElementAt(i).Value.hasPeg = false;
        }
        isPlaying = false;
        difficultyPanel.SetActive(true);
    }

}
