using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotoControl : MonoBehaviour {

    public float velocidadRotacion = 50;
    public float velocidadLineal = 1;
    public Transform ruedaTrasera;
    public event eliminadoDelegate eliminado;
    public delegate void eliminadoDelegate();

    private Rigidbody2D rigidbody;
    private float radioRueda;
    private List <KeyCode> acciones = new List <KeyCode> ();

    public event triggerDelegate eliminado;
    public event triggerDelegate finalNivel;

	// Use this for initialization
	void Start () {

        rigidbody = GetComponent<Rigidbody2D>();
        radioRueda = GetComponent<CircleCollider2D>().radius + 0.1f;
	}
	
    public void MueveDerecha()
    {
        if (TocaElSuelo()) rigidbody.velocity

+= new Vector2(transform.right.x * velocidadLineal, transform.right.y * velocidadLineal) * Time.deltaTime;
        
    }

    public void MueveIzquierda()
    {
        if (TocaElSuelo()) rigidbody.velocity 
-= new Vector2(transform.right.x * velocidadLineal, transform.right.y * velocidadLineal) * Time.deltaTime;

    }

    public void RotaDerecha()
    {
        rigidbody.MoveRotation(rigidbody.rotation - velocidadRotacion * Time.deltaTime);
    }

    public void RotaIzquierda()
    {
        rigidbody.MoveRotation(rigidbody.rotation + velocidadRotacion * Time.deltaTime);
    }


    // Update is called once per frame
    void Update () {

        ActualizarAccionTeclado(KeyCode.LeftArrow);
        ActualizarAccionTeclado(KeyCode.RightArrow);
        ActualizarAccionTeclado(KeyCode.UpArrow);
        ActualizarAccionTeclado(KeyCode.DownArrow);

        if (acciones.Contains(KeyCode.LeftArrow))
        {
            MueveIzquierda();
        }


        if (acciones.Contains(KeyCode.RightArrow))
        {
            MueveDerecha();
        }


        if (acciones.Contains(KeyCode.UpArrow))
        {
            RotaIzquierda();
        }


        if (acciones.Contains(KeyCode.DownArrow))
        {
            RotaDerecha();
        }
    }


    private void ActualizarAccionTeclado (KeyCode code)
    {
        if (Input.GetKeyDown(code))
        {
            ActualizarAccionDown(code);
        }

        if (Input.GetKeyUp(code))
        {
            ActualizarAccionUp(code);
        }
    }
    
    private void ActualizarAccionDown(KeyCode code)
    {
        if (!acciones.Contains(code)) acciones.Add(code);
    }


    private void ActualizarAccionUp(KeyCode code)
    {
        if (!acciones.Contains(code)) acciones.Remove(code);
    }


    public void MueveDerechaDown()
    {
        ActualizarAccionDown(KeyCode.RightArrow);
    }

    public void MueveIzquierdaDown()
    {
        ActualizarAccionDown(KeyCode.LeftArrow);
    }

    public void RotaIzquierdaDown()
    {
        ActualizarAccionDown(KeyCode.UpArrow);
    }

    public void RotaDerechaDown()
    {
        ActualizarAccionDown(KeyCode.DownArrow);
    }

    public void MueveDerechaUp()
    {
        ActualizarAccionUp(KeyCode.RightArrow);
    }

    public void MueveIzquierdaUp()
    {
        ActualizarAccionUp(KeyCode.LeftArrow);
    }

    public void RotaIzquierdaUp()
    {
        ActualizarAccionUp(KeyCode.UpArrow);
    }

    public void RotaDerechaUp()
    {
        ActualizarAccionUp(KeyCode.DownArrow);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.CompareTo("Finish") != 0)
        {
            if (eliminado != null) eliminado();

        }
        else
            if (finalNivel != null) finalNivel();
    }

    bool TocaElSuelo()
    {
        if (Physics2D.OverlapCircleAll(ruedaTrasera.position, radioRueda).Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
