# Unity_Utterance
Edit the data in Assets/StreamingAssets/data.json to set utterance objects

example:
{
  "utteranceObjects":[
        {
            "name":"boy",
            "tags":[
                "Boy",
                "Nick",
                "He"
            ],
            "resource":"Prefabs/Boy_Prefab",
            "position":[0,0,0],
            "rotation":[0,0,0],
            "offsetPosition":[0,0,0],
            "priority":1
        },
        ...
  ]
}

one element of "utteranceObjects" is one Object class in Project;
Utterance of one Object is based on its "tags" array;
"resource" represents the relative path of the model;
"position" and "rotation" are default transform of the model in Unity;
"offsetPosition" is the offset of position from a higher priority model to a lower priority model
"priority": priority of model, 1 for the most important, the higher the less important
