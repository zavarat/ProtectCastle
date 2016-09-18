using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterGenerator : MonoBehaviour {
    [SerializeField]
    private SpawnDataFile spawnDataFile;
    [SerializeField]
    private Transform monsterBucket;
    [SerializeField]
    private GameObject monsterInfos;
    private Queue<GameObject> monsters = new Queue<GameObject>();
    private Queue<MonsterManager> preparedMonsters = new Queue<MonsterManager>();

    private HashSet<int> randomIdxSet = new HashSet<int>();
    private int[] randomIdxs;

    [Range(1, 30)]
    public int maxSpawnNum = 1;

	void Start () {
        spawnDataFile.Init();
        randomIdxs = new int[maxSpawnNum];
        NoDupRandomIdx(spawnDataFile.countCoords, maxSpawnNum);
        for (int idx = 0; idx < maxSpawnNum; idx++)
        {
            GameObject monster = Instantiate(monsterInfos,
                spawnDataFile.spawnCoordinates[randomIdxs[idx]],
                new Quaternion(0, 0, 0, 0)) as GameObject;
            monster.transform.parent = monsterBucket;
            BT_base bt_AI = monster.GetComponent<MonsterManager>().GetMonsterPrefab().GetComponent<BT_base>();
            bt_AI.Init();
            monsters.Enqueue(monster);
        }

        for (int idx = 0; idx < 5; idx++)
            PrepareSailing();
	}
    /// <summary>
    ///  랜덤 인덱스들을 생성합니다.
    /// </summary>
    /// <param name="randomRange"> 랜덤 수 의 범위.</param>
    /// <param name="maxIdx">생성해낼 랜덤 인덱스들의 최대 갯수.</param>
    private void NoDupRandomIdx(int randomRange, int maxIdx)
    {
        int seedStep = 1;
        while (randomIdxSet.Count < maxIdx)
        {
            Random.seed += seedStep;
            randomIdxSet.Add(Random.Range(0, randomRange));
        }
        randomIdxSet.CopyTo(randomIdxs);
    }

    public void PrepareSailing()
    {
        MonsterManager preparedMon = monsters.Dequeue().GetComponent<MonsterManager>();
        preparedMonsters.Enqueue(preparedMon);
        monsters.Enqueue(preparedMon.gameObject);
        preparedMon.GetCreateEffect().SetActive(true);
    }

	public void Sailing()
    {
        while(preparedMonsters.Count > 0)
        {
            MonsterManager preparedMon = preparedMonsters.Dequeue();
            preparedMon.GetMonsterPrefab().SetActive(true);
            preparedMon.GetMonsterPrefab().GetComponent<BT_base>().StartBT();
        }
    }
}
