using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using extOSC.Examples;
using TMPro;
using System;
public class GameManager : MonoBehaviour {

    public GameObject gameButtonPrefab;

    public List<ButtonSetting> buttonSettings;

    public Transform gameFieldPanelTransform;

    public string passPhrase ;
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    List<GameObject> gameButtons;

    int bleepCount = 3;

    public int maxScore;
    int score;
    List<int> bleeps;
    List<int> playerBleeps;

    System.Random rg;

    bool inputEnabled = false;
    bool gameOver = false;

    public TMP_Text scoreText;
    public TMP_Text maxScoreText;
    public TMP_Text compteText;
    

    void Start() {
        gameButtons = new List<GameObject>();

        CreateGameButton(0, new Vector3(-64, 64));
        CreateGameButton(1, new Vector3(64, 64));
        CreateGameButton(2, new Vector3(-64, -64));
        CreateGameButton(3, new Vector3(64, -64));
        // CreateGameButton(4, new Vector3(128, -128));
        scoreText.text= bleepCount.ToString();
        passPhrase = GenerateRandomString(20);
        score = 0;
        StartCoroutine(SimonSays());
        
    }
    private string GenerateRandomString(int length)
    {
        // Initialisation de Random
        System.Random random = new System.Random();

        // Générer une chaîne aléatoire en utilisant des caractères alphanumériques
        char[] randomChars = new char[length];
        for (int i = 0; i < length; i++)
        {
            randomChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(randomChars);
    }
    void CreateGameButton(int index, Vector3 position) {
        GameObject gameButton = Instantiate(gameButtonPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        gameButton.transform.SetParent(gameFieldPanelTransform);
        gameButton.transform.localPosition = position;

        gameButton.GetComponent<Image>().color = buttonSettings[index].normalColor;
        gameButton.GetComponent<Button>().onClick.AddListener(() => {
            OnGameButtonClick(index);
        });

        gameButtons.Add(gameButton);
    }

    void PlayAudio(int index) {
        float length = 0.5f;
        float frequency = 0.001f * ((float)index + 1f);

        AnimationCurve volumeCurve = new AnimationCurve(new Keyframe(0f, 1f, 0f, -1f), new Keyframe(length, 0f, -1f, 0f));
        AnimationCurve frequencyCurve = new AnimationCurve(new Keyframe(0f, frequency, 0f, 0f), new Keyframe(length, frequency, 0f, 0f));

        LeanAudioOptions audioOptions = LeanAudio.options();
        audioOptions.setWaveSine();
        audioOptions.setFrequency(44100);

        AudioClip audioClip = LeanAudio.createAudio(volumeCurve, frequencyCurve, audioOptions);

        LeanAudio.play(audioClip, 0.5f);
    }

    void OnGameButtonClick(int index) {
        if(!inputEnabled) {
            return;
        }

        Bleep(index);

        playerBleeps.Add(index);

        if(bleeps[playerBleeps.Count - 1] != index) {
            GameOver();
            return;
        }

        if(bleeps.Count == playerBleeps.Count) {
            StartCoroutine(SimonSays());
        }
    }

    void GameOver() {
        gameOver = true;
        inputEnabled = false;
        for (int i=4;i>0; i--){
            PlayAudio(i);
        }
        maxScoreText.text= "$ " + maxScore.ToString() + " $";
        StartCoroutine(restart());
        
    }
    IEnumerator restart(){
        score = 0;
        float delay = 0.5f;
        bleepCount = 3;
        gameOver = false;
        inputEnabled = false;
        passPhrase = GenerateRandomString(20);
        for (int i=0;i<5; i++){
            PlayAudio(i);
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(SimonSays());
    }

    IEnumerator SimonSays() {
        inputEnabled = false;

        rg = new System.Random(passPhrase.GetHashCode());
        score = bleepCount-1;
        if (score<3){
            score = 0;
        }
        scoreText.text= score.ToString();
        
        SetBleeps();
        compteText.text= (bleepCount-1).ToString();
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < bleeps.Count; i++) {
            Bleep(bleeps[i]);
            

            yield return new WaitForSeconds(0.6f);
        }

        inputEnabled = true; 
        
        if(score>=maxScore){
            maxScore=score;
        }
        
        yield return null;
    }

    void Bleep(int index) {
        LeanTween.value(gameButtons[index], buttonSettings[index].normalColor, buttonSettings[index].highlightColor, 0.25f).setOnUpdate((Color color) => {
            gameButtons[index].GetComponent<Image>().color = color;
        });

        LeanTween.value(gameButtons[index], buttonSettings[index].highlightColor, buttonSettings[index].normalColor, 0.25f)
            .setDelay(0.5f)
            .setOnUpdate((Color color) => {
                gameButtons[index].GetComponent<Image>().color = color;
            });
        GetComponent<OscSimon>().SendMessage("/simonsays/index",index);
        GetComponent<OscSimon>().SendMessage("/simonsays/index",10);
        PlayAudio(index);
    }

    void SetBleeps() {
        bleeps = new List<int>();
        playerBleeps = new List<int>();

        for(int i = 0; i < bleepCount; i++) {
            bleeps.Add(rg.Next(0, gameButtons.Count));
        }

        bleepCount++;
    }
}
