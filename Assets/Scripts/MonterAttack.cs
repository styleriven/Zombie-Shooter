using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonterAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public void AttackHitEvent() {

        transform.parent.GetComponent<Monster>().AttackHitEvent();
    }
}
