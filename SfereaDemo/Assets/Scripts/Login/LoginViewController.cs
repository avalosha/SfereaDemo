using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginViewController : MonoBehaviour
{

    public InputField userInputField;
    public InputField passInputField;

    public GameObject warningText;
    public GameObject menuCanvas;

    public Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.GameInitialized)
        {
            menuCanvas.SetActive(true);
        }
        else
        {
            openLogin();
        }   
    }

    private void openLogin()
    {
        anim.Play("OpenLogin");
    }

    public void CheckData()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.CheckData(userInputField.text, passInputField.text))
        {
            anim.Play("CloseLogin");
            if (warningText.activeSelf) warningText.SetActive(false);
            menuCanvas.SetActive(true);
        } else
        {
            warningText.SetActive(true);
        }
    }

    public void SkipCheckData() {
        anim.Play("CloseLogin");
        if (warningText.activeSelf) warningText.SetActive(false);
        menuCanvas.SetActive(true);
    }
}
