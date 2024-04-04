using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarneCocinandose1 : MonoBehaviour
{
    public Color colorAlContacto = Color.red;
    private Color colorOriginal;
    private Renderer rend;
    public bool enContacto = false;
    

    void Start()
    {
        rend = GetComponent<Renderer>();
        colorOriginal = rend.material.color;
    }

    public void CambiarColor()
    {
        rend.material.color = colorAlContacto;
    }

    public void RestaurarColor()
    {
        rend.material.color = colorOriginal;
    } 
}
