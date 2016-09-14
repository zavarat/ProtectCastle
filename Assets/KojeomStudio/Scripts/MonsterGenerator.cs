using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterGenerator : MonoBehaviour {
    [SerializeField]
    private SpawnDataFile spawnDataFile;
    [SerializeField]
    private Transform monsterBucket;
    [SerializeField]
    private GameObject monsterPrefab0;
    private List<BT_base> monsters = new List<BT_base>();

    private HashSet<int> randomIdxSet = new HashSet<int>();
    private int[] randomIdxs;

    [Range(1, 30)]
    public int maxSpawnNum = 1;

	// Use this for initialization
	void Start () {
        spawnDataFile.Init();
        randomIdxs = new int[maxSpawnNum];
        NoDupRandomIdx(spawnDataFile.countCoords, maxSpawnNum);
        for (int idx = 0; idx < maxSpawnNum; idx++)
        {
            GameObject monster = Instantiate(monsterPrefab0,
                spawnDataFile.spawnCoordinates[randomIdxs[idx]],
                new Quaternion(0, 0, 0, 0)) as GameObject;
            monster.transform.parent = monsterBucket;
            BT_base bt_AI = monster.GetComponent<BT_base>();
            bt_AI.Init();
            bt_AI.StartBT();
            monsters.Add(bt_AI);
        }
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
	
}
