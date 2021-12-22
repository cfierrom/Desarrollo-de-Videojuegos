using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director_IA : MonoBehaviour
{
    [Header("MARCADORES")]
    public Transform objetivo; //hace referencia al jugador, de esta forma se sabe donde esta en todo momento 
    public Vector3 target; //esta es una variable probablemente temporal que solo se usa para no andar extrallendo la posicion del jugador cada vez que se necesita 
    public GameObject spawn;  //se crea para que se tenga un registro de que es un spawn 
    public GameObject mob;    //o un mob, de preferencia se ocupan prefabs para asi poder destruir el original en el juego sin que eso altere algo en el resto del proyecto
    public GameObject vacio; //se presentaba un problema con la gestion de la lista por temas de ir liverando sus miembros, asi que opte por crear un transform vacio que no afectara en nada, so esto es un NULL 

    [Header("GESTION de los mobs")]
    [Range(9, 1000)]
    public int max_enemy; //cantidad maxima de mobs que puede haber 
    int cant_enemys; //cantidad actual
    public List<GameObject> mobs_list; //listado para gestionarlos

    [Header("GESTION de los spawn")]
    [Range(5, 25)]
    public int max_spawn; //cantidad maxima de spawns que puede haber 
    int cant_spawn; //cantidad actual
    public List<GameObject> spawns_list; //listado para gestionarlos

    [Header("GESTION de la UI v1")]
    public GameObject infotext;
    GameObject text_info;

    void Start()
    {
        max_spawn = 5;
        cant_spawn = spawns_list.Count;
        cant_enemys = mobs_list.Count;
        target = objetivo.position;
        //text_info = infotext.GetComponent<Text>();
    }

    void Update()
    {
        act_target_pos();

    }

    void act_target_pos(){ //esta funcion actualiza la posicion del jugador en cada paso 
        target = objetivo.position;
        for (int i = 0; i < mobs_list.Count; i++){
            if (mobs_list[i].gameObject.tag == "mobs")
            {
                mobs_list[i].gameObject.GetComponent<gulybad>().Set_target(target);
            }
        }
    }
    
    public void crear_mob(Vector3 coor, Transform place){
        GameObject aux = Instantiate(mob, coor, Quaternion.identity,place);
        aux.gameObject.name = "mob"+(mobs_list.Count + 1);
        mobs_list.Add(aux);
        aux.gameObject.GetComponent<gulybad>().ID = mobs_list.Count-1;
    }

    public void matamob(int index){

        //mobs_list.Remove(mobs_list[index]);
        Destroy(mobs_list[index]);
        mobs_list[index] = vacio;
    }

    private void ActCantEnemy()
    {

    }

}
