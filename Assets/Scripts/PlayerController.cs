using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] particleSystems;
    private ParticleSystem ps;

    public new AudioSource audio;
    public AudioClip sfx_shoot;
    public AudioClip sfx_hitMoney;
    public AudioClip sfx_winning;
    public TMP_Text collectedText;
    public TMP_Text countDownTimeText;
    public TMP_Text resultText;
    [SerializeField]
    public GameObject nextLevelPanel;

    private int countCollected;
    private float countDownTime;


    // Start is called before the first frame update
    void Start()
    {
        //Start the game
        Time.timeScale = 1;

        audio = GetComponent<AudioSource>();
        audio.Stop();
        audio.clip = sfx_shoot;

        countCollected = 0;
        SetCountText();

        countDownTime = 30.0f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouseClick();
    }

    private void FixedUpdate()
    {
        if (countDownTime > 0f)
        {
            countDownTime -= Time.deltaTime;
            SetTimerText();
        }
        if (countDownTime <= 0f)
        {
            //Pause the game
            Time.timeScale = 0;

            //prevents ongoing cloning of moneyObjects
            GameObject varGameObject = GameObject.FindWithTag("Money"); //then disable or enable script / component
            varGameObject.GetComponent<MoneyController>().enabled = false;

            SetResultText();
            nextLevelPanel.SetActive(true);

            audio.clip = sfx_winning;
            audio.Play();
        }
    }

    /*public void Shoot()
    {
        // If the left mouse button is pressed down...
        if (Input.GetMouseButtonDown(0) == true)
        {
            audio.clip = sfx_shoot;
            GetComponent<AudioSource>().Play();
        }
        // If the left mouse button is released...
        if (Input.GetMouseButtonUp(0) == true)
        {
            GetComponent<AudioSource>().Stop();
        }
    }
    */

    public void Shoot()
    {
        audio.clip = sfx_shoot;
        GetComponent<AudioSource>().Play();
    }

    public void HitMoney()
    {
        audio.clip = sfx_hitMoney;
        GetComponent<AudioSource>().Play();
    }
    
    private void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit != null && hit.collider != null)
            {
                switch (hit.collider.tag)
                {
                    case "BG":
                        //Debug.Log("You clicked on the BG");
                        Shoot();
                        break;
                    case "Money":
                        //Debug.Log("You clicked on the Money");
                        HitMoney();

                        ps = RandomizeParticleEffects();
                        //Debug.Log(ps.name);
                        ps.transform.position = hit.collider.gameObject.transform.position;
                        ps.Play();

                        Instantiate(hit.collider.gameObject);
                        Destroy(hit.collider.gameObject);

                        countCollected = countCollected + 10;
                        SetCountText();

                        break;
                }
            }
        }
    }

    void SetCountText()
    {
        collectedText.text = "Collected: " + countCollected.ToString();
    }

    void SetTimerText()
    {
        countDownTimeText.text = "Time: " + countDownTime.ToString("f0");
    }

    void SetResultText()
    {
        resultText.text = "You have earned " + countCollected.ToString() + " €";
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        nextLevelPanel.SetActive(false);
    }

    public ParticleSystem RandomizeParticleEffects()
    {
        int index = Random.Range(0, particleSystems.Length);

        return ps = particleSystems[index];
    }
}
