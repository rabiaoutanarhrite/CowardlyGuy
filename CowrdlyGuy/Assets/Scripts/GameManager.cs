using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Player
    public Transform playerWeapon;
    public bool playerIsAfraid;
    public bool playerIsLive;

    //Enemy
    public bool enemyIsFlee;
    public List<Transform> enemies;
    public TextMeshProUGUI enemiesCountTxt;
    public int liveEnemiesCount;

    //Weapon
    public Image weaponImage;
    public List<Transform> weaponsList;
    public float totalWeaponsAmount = 0;
    private float maxAmount = 100;
    public float weaponAmount;
    private float lerpSpeed;

    //UI
    public TextMeshProUGUI gameStatus;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        playerIsLive = true;

        totalWeaponsAmount = 0;

        weaponAmount = maxAmount / weaponsList.Count;

        liveEnemiesCount = enemies.Count;
        enemiesCountTxt.text = "x " + liveEnemiesCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerIsAfraid = false;
        enemyIsFlee = false;
    }

    // Update is called once per frame
    void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;

        weaponImage.fillAmount = Mathf.Lerp(weaponImage.fillAmount, totalWeaponsAmount / maxAmount, lerpSpeed);

        if(totalWeaponsAmount == maxAmount)
        {
            enemyIsFlee = true;
            playerWeapon.gameObject.SetActive(true);
        }

        if(liveEnemiesCount == 0)
        {
            gameStatus.gameObject.SetActive(true);
            gameStatus.text = "Mission Done!";
            gameStatus.color = Color.green;
        }

        if (!playerIsLive)
        {
            gameStatus.gameObject.SetActive(true);
            gameStatus.text = "Mission Failed!";
            gameStatus.color = Color.red;
        }

        enemiesCountTxt.text = "x " + liveEnemiesCount;

    }
}
