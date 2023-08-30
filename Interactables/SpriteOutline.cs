using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Laserbean.General;
using UnityEngine.PlayerLoop;

public class SpriteOutline : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    
    GameObject outline_go;

    [SerializeField] Color outline_color; 


    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetupOutlineSprite();
        outline_go.SetActive(false); 
        
    }
    

    void SetupOutlineSprite() { 
        outline_go = new ("Outline");
        outline_go.transform.SetParent(transform);
        outline_go.transform.localPosition = Vector3.zero; 

        outline_go.transform.localScale = transform.localScale * 1.2f; 


        // SpriteRenderer outlineSpriteRenderer; 
        var outlineSpriteRenderer = outline_go.AddComponent<SpriteRenderer>(); 
        // var outlineSpriteRenderer = outline_go.AddComponent<Image>(); 
        outlineSpriteRenderer.sprite = spriteRenderer.sprite; 
        outlineSpriteRenderer.color = outline_color; 

    }

    public void Outline() {
        outline_go.SetActive(true); 
    }

    public void UnOutline() {
        outline_go.SetActive(false); 
    }



}