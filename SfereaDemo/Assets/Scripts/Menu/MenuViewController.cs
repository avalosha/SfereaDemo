using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuViewController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame(int number)
    {
        switch (number)
        {
            case 0:
                SceneManager.LoadSceneAsync(1);
                break;
            case 1:
                SceneManager.LoadSceneAsync(2);
                break;
            default:
                break;
        }
    }
}
