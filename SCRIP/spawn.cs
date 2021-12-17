using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
   
    public float area_de_creacion = 10;
    public GameObject bastago;
    public Transform ubicacion;
    Vector3 centro = new Vector3(30,1,10);
    // Start is called before the first frame update
    void Start(){
        centro = transform.position;
        for (int i = 0; i < GameObject.Find("director").gameObject.GetComponent<Director_IA>().max_enemy; i++)
        {
            CrearBastago();
        }
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetButtonDown("Fire2")){
            CrearBastago();
        }

    }


    void CrearBastago(){

        Vector3 pos = centro + new Vector3(Random.Range(-(area_de_creacion/2),(area_de_creacion / 2)),2, Random.Range(-(area_de_creacion / 2), (area_de_creacion / 2)));
        GameObject.FindGameObjectWithTag("hivequeen").transform.GetComponent<Director_IA>().crear_mob(pos,ubicacion);
        
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(centro, area_de_creacion);
    }
}
