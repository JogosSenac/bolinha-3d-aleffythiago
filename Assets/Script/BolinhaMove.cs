using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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



    // Start is called before the first frame update
    void Start()
    
    {
        

        rb = GetComponent<Rigidbody>();
        audioPlayer = GetComponent<AudioSource>();
        estarVivo = true;

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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CuboBrilhante"))
        {
            Destroy(other.gameObject);
            audioPlayer.PlayOneShot(pegaCubo);
            pontos++;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Lava"))
        {
            estarVivo = false;
        }
    }
}
        
