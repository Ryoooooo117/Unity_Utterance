using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UtteranceManager : MonoBehaviour {

	public string dataFile = "data.json";
    public List<Object> objs;
    public List<string> tags;

	public Dictionary<GameObject,Object> activeGameObjs;

	private void OnEnable()
	{
		UIManager.enterClickDelegate += GetMessage;
		UIManager.clearClickDelegate += ClearObject;
	}

	private void OnDisable()
	{
		UIManager.enterClickDelegate -= GetMessage;
		UIManager.clearClickDelegate -= ClearObject;
	}

	private void Awake()
    {
        InitObjs();
    }

	/*
	 * call function from DataManager to get data
	 * generate Object and put them in List<Object> objs
	 * put all tags in List<string> tags;
	 */
    private void InitObjs()
    {
		if (objs == null)
		{
			objs = new List<Object>();
		}
		if (activeGameObjs == null)
		{
			activeGameObjs = new Dictionary<GameObject, Object>();
		}
		if (tags == null)
		{
			tags = new List<string>();
		}

		DataManager.Instance.LoadGameData(dataFile);

		foreach (Object obj in DataManager.Instance.formattedObjectList)
		{
			tags.AddRange(obj.tags);
			objs.Add(obj);
		}

		tags = tags.Distinct().ToList();

    }

	private GameObject SpawnGameObject(string resource, Vector3 pos, Vector3 rot)
	{
		return GameObject.Instantiate(Resources.Load(resource), pos, Quaternion.Euler(rot)) as GameObject;
	}
    
	// after user click the Enter Button, invoke this function
    public void GetMessage(string message)
    {
		// clear all activeObject first, add them to filterObjs, and resort based on priority
		ClearObject();
		List<Object> filterObjs = FilterObjects(message);
		filterObjs.Sort((a, b) => a.priority.CompareTo(b.priority));

		//Debug.Log("get message from UImanager: " + message +" after filter: "+filterObjs.Count);
		Vector3 mainObjPos = Vector3.zero;
		for (int i = 0; i < filterObjs.Count; i++)
		{
			Object o = filterObjs[i];
			try
			{
				GameObject go = null;
				if (i == 0)// sort filterObjs based on priority, if rank first, no need offset
				{
					go = SpawnGameObject(o.resource, o.pos, o.rot);
					mainObjPos = o.pos;
				}
				else // rank not first, need to offset from prev
				{
					go = SpawnGameObject(o.resource, mainObjPos+o.offsetPos, o.rot);
				}
				activeGameObjs.Add(go, o);
			}
			catch
			{
				Debug.LogError("Prefab resource path error ");
			}
		}

    }
    
	// filter all objects in List<Object> objs, find those whose tag matches with message
    private List<Object> FilterObjects(string message)
    {
        List<Object> filterObjs = new List<Object>();
        foreach (string tag in tags)
        {
            if (message.ToLower().Contains(tag.ToLower()))
			{
				Object obj = GetObjectByTag(tag.ToLower());
                if (obj != null && !filterObjs.Contains(obj))
                {
                    filterObjs.Add(obj);
                }
            }
        }

        return filterObjs;
    }
    
	// find object by tag, and then check if object is active in scene, if already in scene, return null
    private Object GetObjectByTag(string targetTag)
    {
		Object returnObj = null;
        foreach (Object obj in objs)
        {
            foreach (string tag in obj.tags)
            {
				if (tag.ToLower().Equals(targetTag))
                {
					returnObj = obj;
					break;
                }
            }
			if (returnObj != null)
			{
				break;
			}
        }

		return IsActvie(returnObj) == true ? null : returnObj;
    }

	private bool IsActvie(Object obj)
	{
		if (obj == null)
		{
			return false;
		}
		foreach (KeyValuePair<GameObject, Object> e in activeGameObjs)
		{
			if (e.Value == obj)
			{
				Debug.Log("already in scence");
				return true;
			}
		}
		return false;
	}

	//private List<Object> GetActiveObject()
	//{
	//	List<Object> activeObjs = new List<Object>();
	//	foreach (KeyValuePair<GameObject, Object> e in activeGameObjs)
	//	{
	//		activeObjs.Add(e.Value);
	//	}
	//	return activeObjs;
	//}

	private void ClearObject()
	{
		foreach (KeyValuePair<GameObject,Object> e in activeGameObjs)
		{
			GameObject.Destroy(e.Key);
		}

		activeGameObjs.Clear();
	}
}
