using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    private Image image;
    private Color originalColor;
    private Button button;

    public delegate void CustomButtonDelegate(int num);
    public static event CustomButtonDelegate customButton;

    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<Image>();
        button = this.GetComponent<Button>();
        originalColor = image.color;
    }

    public void TurnOriginalColor()
    {
        image.color = originalColor;
    }

    public void TurnWhiteColor()
    {
        image.color = Color.white;
    }

    public void PressButton(int num)
    {
        //print("Presiono: " + num);
        if (customButton != null)
        {
            customButton(num);
        }
    }
}
