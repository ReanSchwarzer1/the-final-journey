using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class DataTracking : MonoBehaviour
{
    public string data;
    [SerializeField] private GameObject gameManager;

	// Start is called before the first frame update
	void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator LoadData(int ending)
    {
        yield return new WaitForSeconds(3f);
        UnityWebRequest request = UnityWebRequest.Get("https://firebasestorage.googleapis.com/v0/b/the-final-journey-41772.appspot.com/o/SaveData01.dat?alt=media&token=2f978712-84dd-4cfa-8c1a-b23c06882762");
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
            data = "0,0,0";
        }
        else
        {
            data = "";
            string[] dataString = request.downloadHandler.text.Split("%2c");
            for(int i = 0; i < dataString.Length; i++)
            {
                data += dataString[i] + ",";
            }

            data = data.Substring(0, data.Length - 1);
			Debug.Log(data);
		}

        gameManager.GetComponent<GameManager>().UpdateUIAndData(ending);
    }

    public IEnumerator SaveData(string postData)
    {
		UnityWebRequest request = UnityWebRequest.Post("https://firebasestorage.googleapis.com/v0/b/the-final-journey-41772.appspot.com/o/SaveData01.dat?alt=media&token=2f978712-84dd-4cfa-8c1a-b23c06882762", postData);
		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.Log(request.error);
		}
		else
		{
			Debug.Log("Success!");
		}
	}
}
