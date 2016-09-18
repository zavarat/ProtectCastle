using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnDataFile : MonoBehaviour {
    private JSONObject spawnJsonObj;
    private TextAsset spawnText;
    public List<Vector3> spawnCoordinates
    {
        get { return _spawnCoordinates; }
    }
    private List<Vector3> _spawnCoordinates = new List<Vector3>();
    public int countCoords
    {
        get { return _spawnCoordinates.Count; }
    }

    public void Init()
    {
        spawnText = Resources.Load("textAsset/data") as TextAsset;
        spawnJsonObj = new JSONObject(spawnText.text);
        AccessData(spawnJsonObj);
    }
    /// <summary>
    /// json 오브젝트 및 좌표리스트 데이터를 모두 해제합니다.
    /// </summary>
    public void ReleaseData()
    {
        spawnJsonObj.Clear();
        _spawnCoordinates.Clear();
    }
    private void AccessData(JSONObject jsonObj)
    {
        switch (jsonObj.type)
        {
            case JSONObject.Type.OBJECT:
                Dictionary<string, string> datas = jsonObj.ToDictionary();
                Vector3 vec;
                string value;
                datas.TryGetValue("x", out value);
                vec.x = int.Parse(value);
                datas.TryGetValue("y", out value);
                vec.y = int.Parse(value);
                datas.TryGetValue("z", out value);
                vec.z = int.Parse(value);
                _spawnCoordinates.Add(vec);
                break;
            case JSONObject.Type.ARRAY:
                for (int idx = 0; idx < jsonObj.Count; ++idx)
                {
                    AccessData(jsonObj.list[idx]);
                }
                break;
            default:
                Debug.Log("Json Data Sheet Access ERROR");
                break;
        }
    }
}
