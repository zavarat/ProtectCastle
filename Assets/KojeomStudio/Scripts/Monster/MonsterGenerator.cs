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
    private Queue<MonsterManager> monsters = new Queue<MonsterManager>();
    private Queue<MonsterManager> preparedMonsters = new Queue<MonsterManager>();
    
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
            monster.transform.parent = monsterBucket;
            BT_base bt_AI = monster.GetComponent<MonsterManager>().GetMonsterPrefab().GetComponent<BT_base>();
            bt_AI.Init();
            monsters.Enqueue(monster.GetComponent<MonsterManager>());
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
        int seedStep = 1;
        while (randomIdxSet.Count < maxIdx)
        {
            Random.InitState(seedStep);
            seedStep++;
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

    public void PrepareSailing(int spawnNum)
    {
        for(int i = 0; i < spawnNum; ++i)
        {
            if (monsters.Count == 0) return;
            MonsterManager preparedMon = monsters.Dequeue();
            if (preparedMon.GetMonsterPrefab().activeSelf == true)
            {
                monsters.Enqueue(preparedMon);
                continue;
            }
            preparedMonsters.Enqueue(preparedMon);
            monsters.Enqueue(preparedMon);

            int randIdx = randomIdxs.Dequeue();
            randomIdxs.Enqueue(randIdx);
            preparedMon.GetMonsterPrefab().transform.position = spawnDataFile.spawnCoordinates[randIdx];
            preparedMon.GetCreateEffect().transform.position = preparedMon.GetMonsterPrefab().transform.position;
            preparedMon.GetCreateEffect().SetActive(true);
        }
    }

	public void Sailing()
    {
        if(preparedMonsters.Count > 0)
        {
            MonsterManager preparedMon = preparedMonsters.Dequeue();
            preparedMon.GetMonsterPrefab().SetActive(true);
            preparedMon.GetMonsterPrefab().GetComponent<BT_base>().StartBT();
        }
    }
}
