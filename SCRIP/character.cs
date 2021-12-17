using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    [Header("movimiento:")]
    [Range(100f,1000f)] public float velocidadMov = 0.25f; //la velocidad a la que se desplaza
    [Range(0f, 10f)] public float velocidadRot = 10; //que tan sensible es la rotacion (posiblemente es temporal)

    [Header("rotacion")]
    public bool can_rotate = true; // si la funcion de rotar esta disponible
    [SerializeField] private bool dañado;
    Rigidbody MotFis; //esto se usa para acceder al rigidbody del cuerpo
    public Vector3 mov; //vector de movimiento
    
    [Header("componentes y accesos directos")]
    public Camera camara;
    [Header("TEMPORALES")]
    public Light luz;

    
    void Start(){
        MotFis = this.GetComponent<Rigidbody>(); //declara el motfis (el motfis resibe su nombre de MOTor de FISicas)
    }

    void Update(){

        mover();

        if (Input.GetButton("Fire1"))
        {
            Vector3 pos0 = transform.position;
            Vector3 frente = transform.forward;
            //Vector3 pos0 = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
            //Vector3 frente = new Vector3(transform.position.x+transform.forward.x, transform.position.y-1, transform.position.z+transform.forward.z);

            if (Physics.Raycast(pos0,frente,out var HitInfo)){
                //Debug.Log("\npos0: "+pos0+"\tforward: "+frente);
                Debug.DrawRay(pos0, frente * 10000, Color.magenta);
                if (HitInfo.transform.tag == "mobs")
                {
                    HitInfo.transform.GetComponent<gulybad>().interactuar();
                }
            }
        }

    }

    private void mover()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            mov = new Vector3(velocidadMov * Input.GetAxis("Horizontal"), 0, velocidadMov * Input.GetAxis("Vertical"));
        }
        else
        {
            mov = Vector3.zero;
        }
        MotFis.AddForce(mov);

        if (can_rotate)
        {
            //Debug.DrawRay(this.transform.position, new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y) * 10, Color.red);
            apuntado();
            //Debug.DrawRay(transform.position,transform.forward*15,Color.red);
        }
    }
   
    //funcion sacada de un tutorial que pille 
    private (bool success, Vector3 position) getposicionmouse(){ 
        var ray = camara.ScreenPointToRay(Input.mousePosition); //genera un rayo hacia adonde apunte el mouse
        
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity,groundLayer))
        {
            //si el raycast pega con algo este mandara informacion de donde pego ;
            return (success: true, position: hitInfo.point);
        }
        else
        {
            //no ah pegado nada nadita 
            return (success: false, position: Vector3.zero);
        }
    }
    //
    private void apuntado()
    {
        var (success, position) = getposicionmouse();
        if (success){
            var direccion = position - transform.position;
            direccion.y = 0;
            transform.forward = direccion;
            //Debug.DrawRay(transform.position, transform.forward*5, Color.yellow);
        }
    }
    
}

