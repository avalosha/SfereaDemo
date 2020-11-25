using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private UserData userData;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private bool gameInitialized;
    public bool GameInitialized
    {
        get { return gameInitialized; }
    }

    void OnEnable()
    {
        UserDataManager.loaded += LoadData;
    }

    void OnDisable()
    {
        UserDataManager.loaded -= LoadData;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LoadData(UserData _userData)
    {
        userData = _userData;
        gameInitialized = true;
        Debug.Log("User: " + userData.user + " Password: " + userData.password);
    }

    public bool CheckData(string user, string password)
    {
        if (user == userData.user && password == userData.password)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
