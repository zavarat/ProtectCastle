using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameLevelDataFile levelDataFile;
    private GameLevel curGameLevel;
    [SerializeField]
    private GameTimer gameTimer;
    [SerializeField]
    private MonsterGenerator monGenerator;
    [SerializeField]
    private InGameUIManager inGameUIMgr;
    [SerializeField]
    private CoinManager coinMgr;
    [SerializeField]
    private PlanetManager planetMgr;

    private IEnumerator levelController;
    private IEnumerator respawnPrcoess;
	
	void Start () {
        gameTimer.InitGameTimer();
        levelDataFile.Init();
        coinMgr.Init();
        planetMgr.Init();
        monGenerator.Init();

        levelController = GameLevelController();
        respawnPrcoess = MonsterRespawnProcess();

        StartCoroutine(CountDownProcess());
    }
    private IEnumerator CountDownProcess()
    {
        float countDown = 3.0f;
        while (countDown > 0)
        {
            inGameUIMgr.GetCountDownLbl().text = countDown.ToString();
            yield return new WaitForSeconds(1.0f);
            countDown--;
        }
        inGameUIMgr.GetCountDownLbl().gameObject.SetActive(false);
        gameTimer.StartGameTimer();

        StartCoroutine(levelController);
    }

    private IEnumerator GameLevelController()
    {
        NextLevel();
        while (true)
        {
            if(gameTimer.GetSeconds() == 59)
            {
                NextLevel();
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void NextLevel()
    {
        if (levelDataFile.gameLevels.Count == 0) return;

        curGameLevel = levelDataFile.gameLevels.Dequeue();
        Debug.Log("현재 레벨 : " + curGameLevel.name);
        monGenerator.PrepareSailing(curGameLevel.totalUnit);
        StopCoroutine(respawnPrcoess);
        StartCoroutine(respawnPrcoess);
    }

    private IEnumerator MonsterRespawnProcess()
    {
        float sec = 0f;
        while(sec <= curGameLevel.reSpawnSeconds)
        {
            sec++;
            if (sec == curGameLevel.reSpawnSeconds)
            {
                sec = 0f;
                monGenerator.PrepareSailing(curGameLevel.totalUnit);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
