using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour {


    public MotoControl motorista;
    public Text recordText;
    public Button startButton;
    private int segundosParaEmpezar = 3;
    private Text mainText;
    private float tiempoInicial;
    private float tiempoRecord;


	// Use this for initialization
	void Start () {

        motorista.eliminado += Reiniciar;
        motorista.finalNivel += Final;
        motorista.enabled = false;
        mainText = startButton.GetComponentInChildren<Text>();
        tiempoRecord = PlayerPrefs.GetFloat("tiempo record", 0);
        if (tiempoRecord > 0) recordText.text = "Record: " + tiempoRecord.ToString("##.##");
        recordText.enabled = false;
		
	}

    void Reiniciar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        startButton.enabled = false;
        mainText.text = "" + segundosParaEmpezar;
        InvokeRepeating("CuentaAtras", 1, 1);
    }

    void CuentaAtras()
    {
        segundosParaEmpezar--;
        if (segundosParaEmpezar <= 0)
        {
            CancelInvoke();
            JuegoEmpezado();
        }
        else mainText.text = "" + segundosParaEmpezar;
    }


    void JuegoEmpezado()
    {
        motorista.enabled = true;
        tiempoInicial = Time.time;
        if (tiempoRecord > 0) recordText.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
		
        if (motorista.enabled)
        {
            mainText.text = "Tiempo: " + (Time.time - tiempoInicial).ToString("##.##");
        }
	}

    void Final()
    {
        motorista.enabled = false;
        mainText.text = "Final! " + (Time.time - tiempoInicial);
        PlayerPrefs.SetFloat("tiempo record", (Time.time
             - tiempoInicial));
    }
}
