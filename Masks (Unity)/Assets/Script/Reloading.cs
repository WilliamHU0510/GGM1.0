using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloading : MonoBehaviour
{
    Renderer render;
    public bool showingReloading;
    public float alphaChangeTime;
    float alpha;
    bool rising = true;
    void AlphaChange()
    {
        if (rising)
        {
            alpha += Time.deltaTime / alphaChangeTime;
        }
        else
        {
            alpha -= Time.deltaTime / alphaChangeTime;
        }
        if (alpha >= 1)
        {
            rising = false;
            alpha = 1;
        }
        if (alpha <= 0)
        {
            rising = true;
            alpha = 0;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        render = gameObject.GetComponent<Renderer>();
        showingReloading = false;
        alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AlphaChange();
        if (showingReloading)
        {
            render.material.color = new Color(1, 1, 1, alpha);
        }
        else
        {
            render.material.color = new Color(1, 1, 1, 0);
        }
        
    }
    private void FixedUpdate()
    {
        this.transform.localScale = GameObject.Find("Character").transform.localScale;
    }
}
