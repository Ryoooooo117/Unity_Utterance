# Unity_Utterance
Edit the data in Assets/StreamingAssets/data.json to set utterance objects

example:<br>
{<br>
  "utteranceObjects":[<br>
        {<br>
            "name":"boy",<br>
            "tags":[<br>
                "Boy",<br>
                "Nick",<br>
                "He"<br>
            ],<br>
            "resource":"Prefabs/Boy_Prefab",<br>
            "position":[0,0,0],<br>
            "rotation":[0,0,0],<br>
            "offsetPosition":[0,0,0],<br>
            "priority":1<br>
        },<br>
        ...<br>
  ]<br>
}<br>
<br>
one element of "utteranceObjects" is one Object class in Project;<br>
Utterance of one Object is based on its "tags" array;<br>
"resource" represents the relative path of the model;<br>
"position" and "rotation" are default transform of the model in Unity;<br>
"offsetPosition" is the offset of position from a higher priority model to a lower priority model;<br>
"priority": priority of model, 1 for the most important, the higher the less important.<br>
