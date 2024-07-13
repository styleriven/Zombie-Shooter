
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [SerializeField] float HP = 30f;
    [SerializeField] float MaxHP = 30f;

    [SerializeField] int score = 5;
    MonsterAI monsterAI;

    bool isDead = false;
    public bool IsDead() {
        return isDead;
    }

    [SerializeField] float Damage;

    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioSource audioSource;

    private void Awake() {
        monsterAI = GetComponent<MonsterAI>();
        this.audioSource = FindObjectOfType<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("bullet")) {
            TakeDamage(collision.gameObject.GetComponent<Bullet>().Damage);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(float damage) {
        BroadcastMessage("OnDamageTaken");
        HP -= damage;
        if(HP <= 0) {
            HP = 0; 
            Die();
        }
        transform.GetChild(2).GetChild(0).GetComponent<Image>().fillAmount = HP / MaxHP;
    }
    private void Die() {
        if(isDead)
            return;
        isDead = true;

        transform.GetChild(1).GetComponent<Animator>().SetTrigger("Die");
        Statistical.Instance.IncreaseScore(score);
        Statistical.Instance.IncreaseMonters();


        Destroy(gameObject,0.5f);
        if(Statistical.Instance.Monsters == ManagerLevel.Instance.NumberMonster) {
            FindObjectOfType<VictoryHanlde>().HandleVictory();
        }
    }


    public void AttackHitEvent() {
        
        if(monsterAI.target == null)
            return;
        PlayAttackSound();
        monsterAI.target.TakeDamage(Damage);
    }

    public void PlayAttackSound() {
        audioSource.PlayOneShot(attackSound);
    }

}
