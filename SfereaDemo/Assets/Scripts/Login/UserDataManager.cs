using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserDataManager : MonoBehaviour
{
    //Delegate and Event to report data from backend
    public delegate void DelegateUserDataManager(UserData userData);
    public static event DelegateUserDataManager loaded;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCommunication());
    }

    private IEnumerator StartCommunication()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/Chezzar/PruebaUnityLogin/master/LoginUser");
        yield return www.SendWebRequest();

        if(www.isNetworkError && www.isHttpError)
        {
            Debug.LogWarning(www.error);
        } else
        {
            Debug.Log(www.downloadHandler.text);
            SetSave(www.downloadHandler.text);
        }
    }

    private void SetSave(string saveData)
    {
        UserData userData;
        if (string.IsNullOrEmpty(saveData))
        {
            Debug.LogWarning("Can not deserialized");
        } else
        {
            userData = JsonUtility.FromJson<UserData>(saveData);
            if (string.IsNullOrEmpty(saveData))
            {
                Debug.LogWarning("Can not deserialized");
            } else
            {
                try
                {
                    //Debug.Log("User: " + userData.user + " Password: " + userData.password);
                    if (loaded != null)
                    {
                        loaded(userData);
                    }
                } catch { }
            }
        }
    }

}
