using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerControls : MonoBehaviour {
    [SerializeField]
    float moverSpeed = 10f;

    [SerializeField]
    Animator animator;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    float zValue;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform bulletSpawnPoint;

    [SerializeField]
    float bulletSpeed = 20f;

    [SerializeField]
    float fireRate = 0.5f; // Thời gian giữa các lần bắn (tốc độ bắn)

    private float nextFireTime = 0f; // Thời điểm có thể bắn tiếp theo

    [SerializeField] float HP = 100f;

    bool isDie = false;
    bool playGame;

    [SerializeField]
    Transform canvas;

    TMP_Text HPText;

    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioSource audioSource;

    private void Awake() {
        animator = transform.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        this.audioSource = FindObjectOfType<AudioSource>();
        canvas.Find("PlayGame").gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    void Start() {
        
        HPText = canvas.Find("Game").Find("ImgHP").GetChild(0).GetComponent<TMP_Text>();
        if(HPText != null) {
            HPText.text = HP.ToString();
        }
        this.playGame = false;

        StartCoroutine(PlayGame());
    }

    void Update() {
        if(!playGame)
            return;
            

        MoverPlayer();
        RotateWithMouse();
        if(Input.GetButton("Fire1")) {
            animator.SetBool("Shooting", true);
            if(Time.time >= nextFireTime) {
                PlayFireSound();
                Shoot();
                nextFireTime = Time.time + fireRate;
            }

        }
        else {
            animator.SetBool("Shooting", false);
        }
        if(zValue < 0.01 && zValue > -0.01f) {
            animator.SetBool("Moving", false);
        }
        else {
            animator.SetBool("Moving", true);
        }


    }

    void RotateWithMouse() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)) {
            Vector3 direction = hit.point - transform.position;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }

    void MoverPlayer() {
        zValue = Input.GetAxis("Vertical") * Time.deltaTime * moverSpeed;
        rb.AddRelativeForce(Vector3.forward * 1000 * zValue);
        animator.SetFloat("WalkFront", zValue);
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, 3f);
    }

    public void TakeDamage(float damage) {
        if(HP == 0)
            return;
        HP -= damage;
        if(HP <= 0) {
            playGame = false;
            HP = 0;
            animator.SetTrigger("Die");
            GetComponent<DieHandle>().HandleDie();
        }
        HPText.text = HP.ToString();
    }


    public void PlayFireSound() {
        audioSource.PlayOneShot(fireSound);
    }
    public IEnumerator PlayGame() {
        yield return new WaitForSeconds(1.5f);
        canvas.Find("PlayGame").gameObject.SetActive(false);
        this.playGame = true;
    }
}
