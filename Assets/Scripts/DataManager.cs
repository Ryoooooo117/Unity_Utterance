using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour {

	public List<Object> formattedObjectList;

	#region Singleton
	private static DataManager instance;
	public static DataManager Instance
	{
		get
		{
			if (!instance)
			{
				instance = FindObjectOfType<DataManager>();
				if (!instance)
				{
					var obj = new GameObject("DataManager");
					obj.AddComponent<DataManager>();
					instance = obj.GetComponent<DataManager>();
				}
			}
			return instance;
		}
	}
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	static void Initialize()
	{
		DontDestroyOnLoad(Instance);
	}

	#endregion

	public void LoadGameData(string fileName)
    {
		instance.formattedObjectList = new List<Object>();
		string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if(File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
			DataWrapper dataWrapper = JsonUtility.FromJson<DataWrapper>(dataAsJson);
			//Debug.Log("dataWrapper " + dataWrapper.utteranceObjects.Count);
			foreach (Object obj in dataWrapper.utteranceObjects) 
			{
				obj.pos = new Vector3(obj.position[0], obj.position[1], obj.position[2]);
				obj.rot = new Vector3(obj.rotation[0],obj.rotation[1],obj.rotation[2]);
				obj.offsetPos = new Vector3(obj.offsetPosition[0], obj.offsetPosition[1], obj.offsetPosition[2]);
				//Debug.Log("load game success! " + obj.name + " and " + obj.tags[0] + " posx: " + obj.pos + " rot: "+obj.rot);
				instance.formattedObjectList.Add(obj);
			}
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }
}
