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
    [SerializeField] private AudioClip Perdeu;

    private AudioSource audioPlayer;

    private TextMeshProUGUI textoPontos;

    private TextMeshProUGUI textoTotal;

    [Header("Emojis")]

    [SerializeField] private List<Sprite> emojis = new List<Sprite>();

    void Start()

    {

        rb = GetComponent<Rigidbody>();
        estarVivo = true;
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

        if (other.gameObject.CompareTag("PassaFase") && pontos >= 6)

        {

            SceneManager.LoadScene("Fase2");

            pontos = 0;

        }

        if (other.gameObject.CompareTag("PassaFase1") && pontos >= 7)

        {

            SceneManager.LoadScene("Win");

            pontos = 0;

        }

    }

    private void OnCollisionEnter(Collision collision)

    {

        if (collision.gameObject.CompareTag("Lava"))
            audioPlayer.PlayOneShot(Perdeu);

        {

            estaVivo = false;

            telaMorte.SetActive(true);

        }

        if (collision.gameObject.CompareTag("Floor"))

        {

            estaPulando = false;

        }

    }

    public bool RetornaVida()

    {

        return estaVivo;

    }

    public Vector3 Posicao()

    {

        return posInicial;

    }

    private void VerificaObjetivos()

    {

        int totalCubos = Int32.Parse(textoTotal.text);

        TextMeshProUGUI objetivo = GameObject.Find("Objetivo").GetComponent<TextMeshProUGUI>();

        Image emoji = GameObject.Find("Emoji").GetComponent<Image>();

        if (pontos < totalCubos)

        {

            emoji.sprite = emojis[0];

            objetivo.text = "COLETE OS CUBOS";

        }

        if (pontos >= totalCubos / 2)

        {

            emoji.sprite = emojis[1];

            objetivo.text = "METADE JÁ COLETADA";

        }

        if (pontos >= totalCubos - 1)

        {

            emoji.sprite = emojis[2];

            objetivo.text = "RESTA APENAS 1";

        }

        if (pontos == totalCubos)

        {

            emoji.sprite = emojis[3];

            objetivo.text = "PASSAGEM LIBERADA";

        }

    }

}
