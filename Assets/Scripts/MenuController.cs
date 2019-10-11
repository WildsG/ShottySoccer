using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour {

    public GameObject settingsPanel;
    public Slider soundSlider;
    public Slider goalSlider;
    public TMP_Text goalSliderCount;
    private int scoreFromSettings;
    private float soundFromSettings;
    public AudioSource whistle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(OnEnter(collision.gameObject.name));
    }
    IEnumerator OnEnter(string name)
    {
        whistle.Play();
        yield return new WaitForSeconds(1.5f);
        switch (name)
        {
            case "Quit":
                Application.Quit();
                break;
            case "Settings":
                settingsPanel.SetActive(true);
                break;
            case "Start":
                SceneManager.LoadScene("Main");
                break;
        }            
    }
    public void ExitSettingsPanel()
    {
        settingsPanel.SetActive(false);
        ScoreManager.scoreToGet = (int)goalSlider.value;
        PlayerPrefs.SetFloat("Volume", soundSlider.value);
        FindObjectOfType<PointerController>().resetBall();
        
    }
    public void OnSliderValueChange(float value)
    {
        goalSliderCount.text = value.ToString();
    }

}
