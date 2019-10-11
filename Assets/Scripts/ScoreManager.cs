using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    public TMP_Text blueTeam;
    public TMP_Text redTeam;

    private int blueTeamScore;
    private int redTeamScore;

    public static int scoreToGet = 5;

    public Transform[] blueTeamPlayers;
    public Transform[] blueTeamZones;

    public Transform[] redTeamPlayers;
    public Transform[] redTeamZones;

    public static bool scored = false;
    public GameObject countDown;
    public TMP_Text countDownText;
    public TMP_Text WinningText;
    public TMP_ColorGradient presetred;
    public TMP_ColorGradient presetblue;
    public  GameObject winPanel;
    public TMP_Text gameEndingText;
    public AudioSource gameEndingSound;

    public AudioSource backgroundMusic;
    bool hasWon = false;

    private void Start()
    {
        gameEndingText.text = "Win condition: " + scoreToGet+" goals";
        Time.timeScale = 1f;
        if (PlayerPrefs.HasKey("Volume"))
        {
            backgroundMusic.volume = PlayerPrefs.GetFloat("Volume");
            gameEndingSound.volume = PlayerPrefs.GetFloat("Volume");
        }
  
        scored = true;
        StartCoroutine("CountDown");
        blueTeamScore = 0;
        redTeamScore = 0;
    }

    private void Update()
    {
        if (hasWon)
            return;

        if (Input.GetKeyDown(KeyCode.Escape) && !winPanel.activeSelf)
        {
            winPanel.SetActive(true);
            WinningText.text = "PAUSE";
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && winPanel.activeSelf)
        {
            winPanel.SetActive(false);
            Time.timeScale = 1;
        }
        blueTeam.text = blueTeamScore.ToString();
        redTeam.text = redTeamScore.ToString();
        if(blueTeamScore >= scoreToGet)
        {
            winPanel.SetActive(true);
            gameEndingSound.Play();
            WinningText.colorGradientPreset = presetblue;
            WinningText.text = "Blue team won!\n " +
                "Congratulations!";
            Time.timeScale = 0;
            hasWon = true;
        }
        else if(redTeamScore >= scoreToGet)
        {
          
            winPanel.SetActive(true);
            WinningText.colorGradientPreset = presetred;
            WinningText.text = "Red team won!\n" +
                "Better luck next time!";
            gameEndingSound.Play();
            Time.timeScale = 0;
            hasWon = true;
        }

    }
    IEnumerator CountDown()
    {
        for (int i = 0; i < blueTeamPlayers.Length; i++)
        {
            blueTeamPlayers[i].position = blueTeamZones[i].position;
            blueTeamPlayers[i].rotation = Quaternion.identity;
        }
        for (int i = 0; i < redTeamPlayers.Length; i++)
        {
            redTeamPlayers[i].position = redTeamZones[i].position;
            redTeamPlayers[i].rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        transform.position = Vector3.zero;
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.GetComponent<Rigidbody2D>().angularVelocity = 0;
        countDown.SetActive(true);
        countDownText.text = "3";
        yield return new WaitForSeconds(0.5f);
        countDownText.text = "2";
        yield return new WaitForSeconds(1f);
        countDownText.text = "1";
        yield return new WaitForSeconds(1f);
        countDownText.text = "GO";
        scored = false;
        yield return new WaitForSeconds(1f);
        countDown.SetActive(false);


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Goal"))
        {
            scored = true;
            StartCoroutine("CountDown");
            if (other.gameObject.name == "Goal Left")
                redTeamScore++;
            else if (other.gameObject.name == "Goal Right")
                blueTeamScore++;
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
