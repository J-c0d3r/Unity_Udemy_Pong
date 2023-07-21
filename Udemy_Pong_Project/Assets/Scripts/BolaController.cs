using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BolaController : MonoBehaviour
{
    private Rigidbody2D ballRB;
    private float horizontalLimit = 10f;
    private float verticalLimit = 6f;
    public AudioClip boing;
    public Transform transfCam;
    public float delay = 2f;
    public bool isStarted = false;
    int direcao;

    [SerializeField]
    private Text secondTxt;
    [SerializeField]
    private Text minuteTxt;

    [SerializeField]
    private Text bestTimeTxt;

    private float second = 0;
    private int minute = 0;
    private bool wasTimeStarted = false;
    private int bestTimeOld = 0;
    private int bestTimeNow = 0;

    void Start()
    {
        bestTimeTxt.text = PlayerPrefs.GetInt("bestTimeMin").ToString("00") + ":" + PlayerPrefs.GetInt("bestTimeSec").ToString("00");

        ballRB = gameObject.GetComponent<Rigidbody2D>();
        direcao = Random.Range(0, 4);
    }

    void Update()
    {
        delay = delay - Time.deltaTime;

        if (delay <= 0 && isStarted == false)
        {
            if (direcao == 0)
            {
                ballRB.velocity = new Vector2(5f, 5f);
            }
            else if (direcao == 1)
            {
                ballRB.velocity = new Vector2(-5f, 5f);
            }
            else if (direcao == 2)
            {
                ballRB.velocity = new Vector2(-5f, -5f);
            }
            else
            {
                ballRB.velocity = new Vector2(5f, -5f);
            }
            isStarted = true;
            wasTimeStarted = true;
        }

        if (wasTimeStarted)
        {
            second += Time.deltaTime;
            if (second >= 60)
            {
                second = 0;
                minute++;
            }
        }
        secondTxt.text = second.ToString("00");
        minuteTxt.text = minute.ToString("00");

        if (transform.position.x > horizontalLimit || transform.position.x < -horizontalLimit)
        {
            bestTimeNow = (minute * 60) + (int)second;
            bestTimeOld = (PlayerPrefs.GetInt("bestTimeMin") * 60) + PlayerPrefs.GetInt("bestTimeSec");

            if (bestTimeNow > bestTimeOld)
            {
                PlayerPrefs.SetInt("bestTimeSec", (int)second);
                PlayerPrefs.SetInt("bestTimeMin", minute);
            }

            SceneManager.LoadScene("Jogo");
        }

        if (transform.position.y > verticalLimit || transform.position.y < -verticalLimit)
        {
            SceneManager.LoadScene("Jogo");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        AudioSource.PlayClipAtPoint(boing, transfCam.position);
    }

}
