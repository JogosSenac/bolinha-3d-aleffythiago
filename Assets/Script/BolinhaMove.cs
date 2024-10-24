using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallMoviment : MonoBehaviour
{
    private float moveH;
    private float moveV;
    private Rigidbody rb;
    private Vector3 posInicial;
    public GameObject telaMorte;
    public GameObject menu;
    public Menu script;
    [SerializeField] private float velocidade;
    [SerializeField] private float forcaPulo;
    [SerializeField] private int pontos = 0;
    [SerializeField] private bool estaVivo = true;
    [SerializeField] private bool estaPulando;

    [Header("Sons da Bolinha")]
    [SerializeField] private AudioClip pulo;
    [SerializeField] private AudioClip pegaCubo;
    private AudioSource audioPlayer;
    private TextMeshProUGUI textoPontos;
    private TextMeshProUGUI textoTotal;

   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioPlayer = GetComponent<AudioSource>();
        posInicial = transform.position;
        script = menu.GetComponent<Menu>();
        textoPontos = GameObject.FindGameObjectWithTag("Pontos").GetComponent<TextMeshProUGUI>();
        textoTotal = GameObject.Find("TotalCubos").GetComponent<TextMeshProUGUI>();
        textoTotal.text = GameObject.FindGameObjectsWithTag("CuboBrilhante").Length.ToString();
    }

    void Update()
    {
        if (estaVivo)
        {
            moveH = Input.GetAxis("Horizontal");
            moveV = Input.GetAxis("Vertical");
            transform.position += new Vector3(moveH * velocidade * Time.deltaTime, 0, moveV * velocidade * Time.deltaTime);


            if (Input.GetKeyDown(KeyCode.Space) && !estaPulando)
            {
                rb.AddForce(transform.up * forcaPulo, ForceMode.Impulse);
                audioPlayer.PlayOneShot(pulo);
                estaPulando = true;
            }

            VerificaObjetivos();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CuboBrilhante"))
        {
            Destroy(other.gameObject);
            audioPlayer.PlayOneShot(pegaCubo);
            pontos++;
            textoPontos.text = pontos.ToString();
        }

        if (other.gameObject.CompareTag("PassaFase") && pontos >= 42)
        {
            SceneManager.LoadScene("Fase2");
            pontos = 0;
        }

        if (other.gameObject.CompareTag("PassaFase1") && pontos >= 12)
        {
            SceneManager.LoadScene("Win");
            pontos = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            estaVivo = false;
            telaMorte.SetActive(true);
        }

        
    }

    private void VerificaObjetivo()
    {
        int totalCubos = Int32.Perse(textoTotal.text);
        TextMeshProUGUI objetivo = GameObject.Find("Objetivo").GetComponent<TextMeshProUGUI>();


        Debug.LogFormat($"Pontos;{pontos},Total Cubos: {totalCubos}");
         if(pontos < totalCubos)
        {

            objetivo.text = "pegue todos os cubos";

        }
        if (pontos >= totalCubos / 2)
        {

             objetivo.text = "continue assim,você ja pegou a metade"

        }
        if (pontos >= totalCubos - 5)
        {

            objetivo.text = "quase no fim";


        }
        if (pontos == totalCubos)
        {

            objetivo.text = "todos os cubos coletados,passagem liberada";

        }
    }


}
