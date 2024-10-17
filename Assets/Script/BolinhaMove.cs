using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BolinhaMove : MonoBehaviour
{
    private float moveH;
    private float moveV;
    private Rigidbody rb;
    [SerializeField] private float velocidade;
    [SerializeField] private float forcaPulo;
    [SerializeField] private bool invertH;
     [SerializeField] private int pontos;
     [SerializeField] private int invertV;
    [Header("Sons da Bolinha")]
     [SerializeField] private AudioClip pulo;
     [SerializeField] private AudioClip pegaCubo; 
     private AudioSource audioPlayer;
     private bool estarVivo = true;
     private TextMeshProUGUI textoPontos;
     private TextMeshProUGUI textoTotal;

    [Header("Emojis")]
    [SerializeField] private List<Sprite> emojis = new List<Sprite>();


    // Start is called before the first frame update
    void Start()
    
    {


        rb = GetComponent<Rigidbody>();
        audioPlayer = GetComponent<AudioSource>();
        estarVivo = true;
        textoPontos = GameObject.Find("pontos").GetComponent<TextMeshProUGUI>();
        textoTotal = GameObject.Find("totalPontos").GetComponent<TextMeshProUGUI>();
        textoTotal.text = GameObject.FindGameObjectsWithTag("CuboBrilhante").Length.ToString();
        

    }

    // Update is called once per frame
    void Update()
    
    {
        if(estarVivo)
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        transform.position += new Vector3 (moveH * velocidade * Time.deltaTime, 0,  moveV * velocidade * Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up* forcaPulo, ForceMode.Impulse);
            audioPlayer.PlayOneShot(pulo);
        }

        VerificaObjetivo();
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
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Lava"))
        {
            estarVivo = false;
        }
    }
    private void VerificaObjetivo()
    {
        int totalCubos = Int32.Parse(textoTotal.text);
        TextMeshProUGUI objetivo = GameObject.Find("Objetivo").GetComponent<TextMeshProUGUI>();

        Image emoji = GameObject.Find("Emoji").GetComponent<Image>();

        Debug.LogFormat($"Pontos: {pontos},Total cubos: {totalCubos}");
        if(pontos < totalCubos)
        {
           
            objetivo.text = "pegue todos os cubos";
            emoji.sprite = emojis[0];
        }
         if(pontos >= totalCubos / 2)
        {
            
            objetivo.text = "continue assim,você ja pegou a metade";
            emoji.sprite = emojis[1];
        }
        if(pontos >= totalCubos -5)
        {
           
            objetivo.text ="quase no fim";
            emoji.sprite = emojis[2];

        }
        if(pontos == totalCubos)
        {
            
            objetivo.text = "todos os cubos coletados,passagem liberada";
            emoji.sprite = emojis[3];
        }
    } 
}
        
