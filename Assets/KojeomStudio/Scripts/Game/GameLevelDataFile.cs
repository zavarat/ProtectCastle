using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct GameLevel
{
    public string name;
    public int totalUnit;
    public int reSpawnSeconds;
}

public class GameLevelDataFile : MonoBehaviour {

    private JSONObject gameLevelJsonObj;
    private TextAsset gameLevelText;
    public Queue<GameLevel> gameLevels
    {
        get { return new Queue<GameLevel>(_gameLevels); }
    }
    private Queue<GameLevel> _gameLevels = new Queue<GameLevel>();

    public void Init()
    {
        gameLevelText = Resources.Load("textAsset/LevelDataSheet") as TextAsset;
        gameLevelJsonObj = new JSONObject(gameLevelText.text);
        AccessData(gameLevelJsonObj);
    }

    public void ReleaseData()
    {
        gameLevelJsonObj.Clear();
        _gameLevels.Clear();
    }

    private void AccessData(JSONObject jsonObj)
    {
        switch (jsonObj.type)
        {
            case JSONObject.Type.OBJECT:
                Dictionary<string, string> datas = jsonObj.ToDictionary();
                string data;
                GameLevel gameLevel;

                datas.TryGetValue("LevelName", out data);
                gameLevel.name = data;
                datas.TryGetValue("TotalUnit", out data);
                gameLevel.totalUnit = int.Parse(data);
                datas.TryGetValue("ReSpawnSeconds", out data);
                gameLevel.reSpawnSeconds = int.Parse(data);

                _gameLevels.Enqueue(gameLevel);

                break;
            case JSONObject.Type.ARRAY:
                for (int idx = 0; idx < jsonObj.Count; ++idx)
                {
                    // to do
                    AccessData(jsonObj.list[idx]);
                }
                break;
            default:
                Debug.Log("Json Data Sheet Access ERROR");
                break;
        }
    }

}
