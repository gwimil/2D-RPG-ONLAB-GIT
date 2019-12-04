using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NeuralManager : MonoBehaviour
{
    public GameObject m_Bot;
    public GameObject m_TestHero;
    public GameObject m_TestPorjectilesToDodge;
    public GameObject m_Mage;

    public GameObject m_DodgeWalls;
    public GameObject m_NormalWalls;

    public GameObject m_MenuCanvas;
    public Button m_DodgeButton;
    public Button m_AttackButton;
    public Button m_FightButton;
    public Button m_BackToMenuButton;

    public GameObject m_BackCanvas;
    public Button m_BackButton;

    public Text m_GenerationText;
    public Text m_NumberOfTriesText;
    public Text m_PlayerBot;

    public NeuralNetwork dodgeCopyNeural;

    private GameObject m_BotHelper;

    private bool allDodgeDied;
    private int dodgeGenerationNumber;
    private int attackNumber;
    private int playerWin;
    private int botWin;

    private bool dodgeStarted;
    private bool attackStarted;

    private bool isDodge;
    private bool isAttack;
    private bool isFight;


    // Start is called before the first frame update
    void Start()
    {
        isDodge = false;
        isAttack = false;
        isFight = false;
        dodgeStarted = false;
        allDodgeDied = false;

        m_MenuCanvas.gameObject.SetActive(true);
        m_BackCanvas.gameObject.SetActive(false);

        m_DodgeButton.onClick.AddListener(ClickOnDodge);
        m_AttackButton.onClick.AddListener(ClickOnAttack);
        m_FightButton.onClick.AddListener(ClickOnFight);
        m_BackToMenuButton.onClick.AddListener(ClickOnMenuBack);
        m_BackButton.onClick.AddListener(ClickOnBack);

        dodgeGenerationNumber = 0;
        attackNumber = 0;
        playerWin = 0;
        botWin = 0;

        m_Bot.GetComponent<Bot>().attackNeuralNetwork = new NeuralNetworkBProp(new int[] { 7, 12, 8, 4 });
        m_Bot.GetComponent<Bot>().dodgeNeuralNetwork = new NeuralNetwork(new int[] { 5, 7, 7, 4 });
        
    }


    private void ClickOnDodge()
    {
        isDodge = true;
        m_DodgeWalls.SetActive(true);
        m_MenuCanvas.gameObject.SetActive(false);
        m_BackCanvas.gameObject.SetActive(true);
        LearnToDodge();
        InvokeRepeating("SpawnProjectiles", 0.0f, 0.25f);
    }
    private void ClickOnAttack()
    {
        isAttack = true;
        RespawnTestHero();
        GameObject bot = Instantiate(m_Bot, transform.position, Quaternion.Euler(0, 0, 0));
        bot.GetComponent<Bot>().attack = true;
        bot.GetComponent<Bot>().attackNeuralNetwork = new NeuralNetworkBProp(m_Bot.GetComponent<Bot>().attackNeuralNetwork);
        m_MenuCanvas.gameObject.SetActive(false);
        m_BackCanvas.gameObject.SetActive(true);

    }
    private void ClickOnFight()
    {
        
        Instantiate(m_Mage, new Vector3(this.transform.position.x - 6, this.transform.position.y, 0),Quaternion.Euler(0,0,0));
        GameObject bot = Instantiate(m_Bot, new Vector3(this.transform.position.x + 6, this.transform.position.y, 0), Quaternion.Euler(0, 0, 0));
        bot.GetComponent<Bot>().attackNeuralNetwork = new NeuralNetworkBProp(m_Bot.GetComponent<Bot>().attackNeuralNetwork);
        bot.GetComponent<Bot>().dodgeNeuralNetwork = new NeuralNetwork(m_Bot.GetComponent<Bot>().dodgeNeuralNetwork);
        bot.GetComponent<Bot>().fight = true;
        isFight = true;
        m_MenuCanvas.gameObject.SetActive(false);
        m_BackCanvas.gameObject.SetActive(true);
        m_NormalWalls.gameObject.SetActive(true);

    }
    private void ClickOnMenuBack()
    {
        SceneManager.LoadScene("MENU");
    }

    private void ClickOnBack()
    {
        if (isAttack)
        {
            isAttack = false;
            GameObject neural = GameObject.FindGameObjectWithTag("Neural");
            GameObject hero = GameObject.FindGameObjectWithTag("Hero");
            GameObject proj = GameObject.FindGameObjectWithTag("EnemyProjectile");
            if (neural != null)
            {
                m_Bot.GetComponent<Bot>().attackNeuralNetwork = new NeuralNetworkBProp(neural.GetComponent<Bot>().attackNeuralNetwork);
                Destroy(neural.gameObject);
            }
            if (hero != null)
            {
                Destroy(hero.gameObject);
            }
            if (proj != null)
            {
                Destroy(proj);
            }
            
            m_MenuCanvas.gameObject.SetActive(true);
            m_BackCanvas.gameObject.SetActive(false);
        }
        if (isDodge)
        {
            isDodge = false;
            dodgeStarted = false;
            
            CancelInvoke();
            GameObject[] neurals = GameObject.FindGameObjectsWithTag("Neural");
            if (neurals.Length == 1)
            {
                m_Bot.GetComponent<Bot>().dodgeNeuralNetwork = new NeuralNetwork(neurals[0].GetComponent<Bot>().dodgeNeuralNetwork);
            }
            for (int i = 0; i < neurals.Length; i++)
            {
                Destroy(neurals[i].gameObject);
            }
            GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
            for (int i = 0; i < projectiles.Length; i++)
            {
                Destroy(projectiles[i].gameObject);
            }
            m_Bot.GetComponent<Bot>().dodge = false;
            m_MenuCanvas.gameObject.SetActive(true);
            m_DodgeWalls.SetActive(false);
            m_BackCanvas.gameObject.SetActive(false);
        }
        if (isFight)
        {
            isFight = false;
            GameObject neural = GameObject.FindGameObjectWithTag("Neural");
            GameObject hero = GameObject.FindGameObjectWithTag("Hero");
            GameObject[] proj1 = GameObject.FindGameObjectsWithTag("EnemyProjectile");
            GameObject[] proj2 = GameObject.FindGameObjectsWithTag("PlayerProjectile");
            for (int i = 0; i < proj1.Length; i++)
            {
                Destroy(proj1[i].gameObject);
            }
            for (int i = 0; i < proj2.Length; i++)
            {
                Destroy(proj2[i].gameObject);
            }
            if (neural != null)
            {
                Destroy(neural.gameObject);
            }
            if (hero != null)
            {
                Destroy(hero.gameObject);
            }
            m_NormalWalls.gameObject.SetActive(false);
            m_MenuCanvas.gameObject.SetActive(true);
            m_BackCanvas.gameObject.SetActive(false);
        }


    }
    
    // Update is called once per frame
    void Update()
    {
        if (dodgeStarted)
        {
            GameObject[] neurals = GameObject.FindGameObjectsWithTag("Neural");
            if (neurals.Length == 0)
            {
                m_Bot.GetComponent<Bot>().dodgeNeuralNetwork = new NeuralNetwork(dodgeCopyNeural);
                dodgeStarted = false;
                dodgeEnded();
            }
        }

        if (isFight)
        {
            GameObject neural = GameObject.FindGameObjectWithTag("Neural");
            GameObject hero = GameObject.FindGameObjectWithTag("Hero");

            if (neural == null || hero == null)
            {
                if (neural == null)
                {
                    botWin++;
                }
                else
                {
                    playerWin++;
                }
                m_PlayerBot.text = "Player : Bot - " + playerWin + " : " + botWin;

                ClickOnBack();
            }
        }
        
    }

    public void dodgeEnded()
    {
        dodgeGenerationNumber++;
        m_GenerationText.text = "Generation: " + dodgeGenerationNumber;
        LearnToDodge();
    }


    public void RespawnTestHero()
    {
        attackNumber++;
        m_NumberOfTriesText.text = "Number of tries:" + attackNumber;
        float x = 0;
        float y = 0;
        while (x > -1 && 1 > x && y > -1 && 1 > y)
        {
            x = Random.Range(-6, 6);
            y = Random.Range(-6, 6);
        }

        float moveX = Random.Range(-1.0f, 1.0f);
        float moveY = Random.Range(-1.0f, 1.0f);
        Vector2 movement = new Vector2(moveX, moveY);
        Vector2 normalizedMovement = movement.normalized;
        GameObject tHero = Instantiate(m_TestHero, new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0));
        float random = Random.Range(0.0f, 1.0f);
        if (random < 0.1f)
        {
            normalizedMovement = new Vector2(0.0f, 0.0f);
        }
        tHero.GetComponent<TestHero>().normalizedVelicoty = normalizedMovement;
    }

    private void LearnToDodge()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        for (int i = 0; i < projectiles.Length; i++) {
            Destroy(projectiles[i].gameObject);
        }

        List<GameObject> dodgingNetworks = new List<GameObject>();


        Bot bot1 = Instantiate(m_Bot.gameObject, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Bot>();
        bot1.dodgeNeuralNetwork = new NeuralNetwork(m_Bot.GetComponent<Bot>().dodgeNeuralNetwork);
        dodgingNetworks.Add(bot1.gameObject);

        for (int i = 0; i < 9; i++)
        {
            Bot bot = Instantiate(m_Bot.gameObject, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Bot>();
            NeuralNetwork newNeural = new NeuralNetwork(m_Bot.GetComponent<Bot>().dodgeNeuralNetwork);
            newNeural.Mutate();
            bot.dodgeNeuralNetwork = new NeuralNetwork(newNeural);
            dodgingNetworks.Add(bot.gameObject);
        }
        for (int i = 0; i < dodgingNetworks.Count; i++)
        {
            dodgingNetworks[i].GetComponent<Bot>().dodge = true;
        }
        dodgeStarted = true;
    }

    private void SpawnProjectiles()
    {
        float distance = 0;
        float x = 0, y = 0;
        while (12 > distance)
        {
            x = Random.Range(-16, 16);
            y = Random.Range(-16, 16);
            distance = Vector2.Distance(new Vector2(0, 0), new Vector2(x, y));
        }
        GameObject projectile = Instantiate(m_TestPorjectilesToDodge, new Vector3(x, y, 0), Quaternion.Euler(0, 0, 90));
        Vector2 normalizedMovement = (new Vector3(0, 0, 0) - projectile.transform.position).normalized;

        float angleToRotate = Random.Range(-45, 45);
        float angleInRadian = angleToRotate * Mathf.Deg2Rad;
        float normalX = normalizedMovement.x * Mathf.Cos(angleInRadian) - normalizedMovement.y * Mathf.Sin(angleInRadian);
        float normalY = normalizedMovement.x * Mathf.Sin(angleInRadian) + normalizedMovement.y * Mathf.Cos(angleInRadian);
        Vector2 angledNormalVector = new Vector2(normalX, normalY);

        projectile.GetComponent<NeuralProjectile>().setDirection(angledNormalVector);
    }

    private void LearnToAim()
    {

    }
}
