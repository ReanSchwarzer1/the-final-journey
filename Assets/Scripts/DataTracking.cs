using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class DataTracking : MonoBehaviour
{
    private string data;

	// Start is called before the first frame update
	void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CallSaveData(GameObject choice)
    {
        StartCoroutine(SaveData("https://firebasestorage.googleapis.com/v0/b/the-final-journey-41772.appspot.com/o/SaveData01.dat?alt=media&token=71c4fb2f-922d-4326-84fe-37b59af25926", choice.GetComponent<TMP_Text>().text));
    }

    private IEnumerator LoadData(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            data = request.downloadHandler.text;
            Debug.Log(data);
        }
    }

    private IEnumerator SaveData(string url, string postData)
    {
		UnityWebRequest request = UnityWebRequest.Post(url, postData);
		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.Log(request.error);
		}
		else
		{
			Debug.Log("Success!");
            StartCoroutine(LoadData(url));
		}
	}
}
