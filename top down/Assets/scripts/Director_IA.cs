using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director_IA : MonoBehaviour
{
    [Header("MARCADORES")]
    public Transform objetivo; //hace referencia al jugador, de esta forma se sabe donde esta en todo momento 
    public Vector3 target; //esta es una variable probablemente temporal que solo se usa para no andar extrallendo la posicion del jugador cada vez que se necesita 
    public GameObject spawn;  //se crea para que se tenga un registro de que es un spawn 
    public GameObject mob;    //o un mob, se ocuparan mas adelante cuando se termine la creacion de entidades. 
    public GameObject vacio;

    [Header("GESTION de los mobs")]
    [Range(9, 1000)]
    public int max_enemy; //cantidad maxima de mobs que puede haber 
    [SerializeField] private int cant_enemys; //cantidad actual
    public List<GameObject> mobs_list; //listado para gestionarlos

    [Header("GESTION de los spawn")]
    [Range(5, 25)]
    public int max_spawn; //cantidad maxima de spawns que puede haber 
    int cant_spawn; //cantidad actual
    public List<GameObject> spawns_list; //listado para gestionarlos
    public int areaCreacion;

    private Vector3 area ;


    void Start()
    {
        //mobs_list.AddRange(GameObject.FindGameObjectsWithTag("mobs")); //addrange se usa para agregar arreglos a la lista, esto agrega todos los mobs que ya existan 
        cant_enemys = mobs_list.Count;
        target = objetivo.position;


    }

    void Update()
    {
        act_target_pos();
        area = new Vector3(areaCreacion,1,areaCreacion);
        if (Input.GetButtonDown("Fire3"))
        {
            crear_spawn();
        }
        {

        }

    }

    void act_target_pos(){ //esta funcion actualiza la posicion del jugador en cada paso 
        target = objetivo.position;
        for (int i = 0; i < mobs_list.Count; i++){
            if (mobs_list[i])
            {
                mobs_list[i].gameObject.GetComponent<gulybad>().Set_target(objetivo.position);
            }
        }
    }
    
    public void crear_mob(Vector3 coor, Transform place){
        GameObject aux = Instantiate(mob, coor, Quaternion.identity,place);
        aux.gameObject.name = "mob"+(mobs_list.Count + 1);
        aux.gameObject.GetComponent<gulybad>().ID = mobs_list.Count;
        mobs_list.Add(aux);
        cant_enemys++;
    }

    public void matamob(int index){

        //mobs_list.Remove(mobs_list[index]);
        Destroy(mobs_list[index]);
        mobs_list[index] = null;
        cant_enemys--;
    }

    private void crear_spawn()
    {
        Vector3 pos = transform.position + new Vector3(Random.Range(-(areaCreacion / 2), (areaCreacion / 2)), 0, Random.Range(-(areaCreacion / 2), (areaCreacion / 2)));
        GameObject aux = Instantiate(spawn, pos, Quaternion.identity);
        aux.gameObject.name = "spawn " + (spawns_list.Count);
        aux.gameObject.GetComponent<spawn>().ID = spawns_list.Count;
        spawns_list.Add(aux);
        cant_spawn++;
    }

    public void mataSpawn(int index)
    {
        Destroy(spawns_list[index]);
        spawns_list[index] = null;
        cant_spawn--;

    }

    public void FailState(GameObject aux){

        Debug.Log("muerto");
        aux.gameObject.GetComponent<character>().vidaActual = 10;
        //Destroy(aux);
    }

    private void OnDrawGizmos(){

        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireCube(Vector3.zero, area);
        //Gizmos.DrawCube(new Vector3(0,1,0), area);

        }

    }
