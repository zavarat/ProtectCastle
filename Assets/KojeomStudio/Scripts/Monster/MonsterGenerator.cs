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
    private Queue<MonsterManager> preparedMonsterPool = new Queue<MonsterManager>();
    
    private Queue<int> randomIdxs = new Queue<int>();

    [Range(1, 100)]
    public int maxSpawnNum = 1;
    
	public void Init () {
        spawnDataFile.Init();
        NoDupRandomIdx(spawnDataFile.countCoords, maxSpawnNum);
        for (int idx = 0; idx < maxSpawnNum; idx++)
        {
            int randIdx = randomIdxs.Dequeue();
            randomIdxs.Enqueue(randIdx);
            GameObject monster = Instantiate(monsterInfos,
                spawnDataFile.spawnCoordinates[randIdx],
                new Quaternion(0, 0, 0, 0)) as GameObject;
            monster.name += "_" + idx;
            monster.transform.parent = monsterBucket;
            monster.GetComponent<MonsterManager>().Init();
            monster.GetComponent<MonsterManager>().OnAfterCreate += Sailing;
            monster.GetComponent<MonsterManager>().OnAfterDestroy += RePrepareMonster;
            preparedMonsterPool.Enqueue(monster.GetComponent<MonsterManager>());
        }

	}
    /// <summary>
    ///  중복 없는 랜덤 인덱스들을 생성합니다.
    /// </summary>
    /// <param name="randomRange"> 랜덤 수 의 범위.</param>
    /// <param name="maxIdx">생성해낼 랜덤 인덱스들의 최대 갯수.</param>
    private void NoDupRandomIdx(int randomRange, int maxIdx)
    {
        HashSet<int> randomIdxSet = new HashSet<int>();
        int[] temp;
        while (randomIdxSet.Count < maxIdx)
        {
            Random.InitState(Random.Range(1, 10));
            randomIdxSet.Add(Random.Range(0, randomRange));
        }
        temp = new int[randomIdxSet.Count];
        randomIdxSet.CopyTo(temp);
        randomIdxSet.Clear();

        foreach(var idx in temp)
        {
            randomIdxs.Enqueue(idx);
        }
    }

    private void RePrepareMonster(MonsterManager monMgr)
    {
        preparedMonsterPool.Enqueue(monMgr);
    }

    public void PrepareSailing(int spawnNum)
    {
        for (int i = 0; i < spawnNum; ++i)
        {
            if (preparedMonsterPool.Count == 0) return;
            MonsterManager preparedMon = preparedMonsterPool.Dequeue();
            int randIdx = randomIdxs.Dequeue();
            randomIdxs.Enqueue(randIdx);
            preparedMon.GetMonsterPrefab().transform.position = spawnDataFile.spawnCoordinates[randIdx];
            preparedMon.GetCreateEffect().transform.position = preparedMon.GetMonsterPrefab().transform.position;
            preparedMon.GetCreateEffect().SetActive(true);
        }
    }

    public void Sailing(MonsterManager preparedMon)
    {
        Debug.Log("Sailing Start");
        preparedMon.GetMonsterPrefab().SetActive(true);
        preparedMon.StartMonsterAI();
    }
}
