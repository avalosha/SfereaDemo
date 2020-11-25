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

    private bool running;
    public bool Running{
        set{running = value;}
        get{return running;}
    }

    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponent<Button>();
        image = this.GetComponent<Image>();
        animation = this.GetComponent<Animation>();
    }

    public void DisableButton(){
        button.interactable = false;
    }

    public void TurnCard() {
        if(!running){return;}
        if (!face) {
            face=true;
            animation.Play("turnCard");
            if(card!=null){
                card(numberCard);
            }
        } else {
            face = false;
            animation.Play("turnOffCard");
        }
    }

    public void HideCard() {
        if(!face){
            //print("Oculta carta");
            image.sprite = sourceImage;
        }
    }

    public void ShowCard() {
        if (face){
            //print("Muestra carta");
            image.sprite = sprite;
        }
    }
}
