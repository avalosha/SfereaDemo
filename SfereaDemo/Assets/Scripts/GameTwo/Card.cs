using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public delegate void CardDelegate(int num);
    public static event CardDelegate card;

    private Button button;
    private Image image;
    private Animation animation;

    public Sprite sprite;
    public Sprite sourceImage;

    private bool face;
    public bool Face{
        set{face = value;}
    }
    public int numberCard;
    public int numberImage;

    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponent<Button>();
        image = this.GetComponent<Image>();
        animation = this.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableButton(){
        button.interactable = false;
    }

    public void TurnCard() {
        if (!face) {
            face=true;
            animation.Play("turnCard");
            if(card!=null){
                card(numberCard);
            }
        } else {
            face = false;
            image.sprite = sourceImage;
            print("Oculta carta");
            animation.Play("turnOffCard");
        }
    }

    public void ShowCard() {
        if (face){
            print("Muestra carta");
            image.sprite = sprite;
        }
    }
}
