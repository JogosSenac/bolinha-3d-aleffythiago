using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    [SerializeField] private float moveX;
    [SerializeField] private float moveY;
    [SerializeField] private float moveZ;
    [SerializeField] private float velocidade;
    [SerializeField] private float dirMove;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(dirMove * moveX * Time.deltaTime, dirMove * moveY * Time.deltaTime,dirMove * moveZ* Time.deltaTime );
    }
}
