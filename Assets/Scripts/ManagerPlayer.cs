using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPlayer : MonoBehaviour
{
    public static ManagerPlayer instance;

    //public List<Item> Items;

    void Awake() {

        if(ManagerPlayer.instance != null) Debug.LogError("Only 1 ManagerPlayer allow");

        ManagerPlayer.instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
