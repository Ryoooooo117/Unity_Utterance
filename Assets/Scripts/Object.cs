using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Object 
{
    public string name;
	public List<string> tags;
    public string resource;
    public Vector3 pos;
    public Vector3 rot;
	public int priority;
	public Vector3 offsetPos;

	public List<float> rotation;
	public List<float> position;
	public List<float> offsetPosition;
    
}
