using UnityEngine;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

public class RealDateTime : MonoBehaviour
{
	#region Singleton

	public static RealDateTime Instance { get; private set; }
	public void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	#endregion

	struct TimeData
	{
		public string datetime;
	}

	const string API_URL = "http://worldtimeapi.org/api/ip";

	[HideInInspector] public bool IsTimeLodaed = false;

	private DateTime _currentDateTime = DateTime.Now;

	void Start()
	{
		StartCoroutine(GetRealDateTimeFromAPI());
	}

	public DateTime GetCurrentDateTime()
	{
		return _currentDateTime.AddSeconds(Time.realtimeSinceStartup);
	}

	IEnumerator GetRealDateTimeFromAPI()
	{
		UnityWebRequest webRequest = UnityWebRequest.Get(API_URL);
		Debug.Log("getting real datetime...");

		yield return webRequest.SendWebRequest();

		if (webRequest.isNetworkError)
		{
			//error
			Debug.Log("Error: " + webRequest.error);

		}
		else
		{
			//success
			TimeData timeData = JsonUtility.FromJson<TimeData>(webRequest.downloadHandler.text);

			_currentDateTime = ParseDateTime(timeData.datetime);
			IsTimeLodaed = true;

			Debug.Log("Success.");
		}
	}
	//datetime format => 2020-08-14T15:54:04+01:00
	DateTime ParseDateTime(string datetime)
	{
		//match 0000-00-00
		string date = Regex.Match(datetime, @"^\d{4}-\d{2}-\d{2}").Value;

		//match 00:00:00
		string time = Regex.Match(datetime, @"\d{2}:\d{2}:\d{2}").Value;

		return DateTime.Parse(string.Format("{0} {1}", date, time));
	}
}