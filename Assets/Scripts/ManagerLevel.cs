using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLevel : MonoBehaviour {
    
    [SerializeField] 
    private int numberMonster;
    List<Transform> zones = new List<Transform>();
    
    [SerializeField] GameObject monster;
    private static ManagerLevel instance;

    [SerializeField] public List<GameObject> Monsters = new List<GameObject>();

    [SerializeField]
    public Transform ParentMonster;

    [SerializeField]
    public int[] numberMonsters = new int[] { 10, 15, 20, 25 };

    public int NumberMonster { get => numberMonster; set => numberMonster = value; }
    public static ManagerLevel Instance { get => instance; set => instance = value; }

    void Awake() {
        if(ManagerLevel.Instance != null)
            Debug.LogError("Only 1 ManagerPlayer allowed");
       
        ManagerLevel.Instance = this;

        Transform Rooms = GameObject.Find("Rooms").transform;
        for(int i = 0 ; i < Rooms.childCount ; i++) {
            zones.Add(Rooms.GetChild(i));
        }
        NumberMonster = numberMonsters[Random.Range(0, numberMonsters.Length)];
    }
    private void OnEnable() {
        ParentMonster = GameObject.Find("Monsters").transform;
        SpawnMonsters();
    }

    void SpawnMonsters() {
        for(int i = 0 ; i < NumberMonster ; i++) {
            int zoneIndex = i % zones.Count;
            GameObject mt = Instantiate(monster, zones[zoneIndex].position, Quaternion.identity);
            mt.transform.SetParent(ParentMonster);
            Monsters.Add(mt);
        }
    }

    void Update() {

    }
}
