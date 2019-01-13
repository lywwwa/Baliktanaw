using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayesServer;
using System;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour {

    static Variable question;
    static Variable c1;
    static Variable c2;
    static Variable c3;

    static Variable scriptDatu;
    static Variable scriptLupas;
    static Variable scriptLAma;

    public GameObject Lupas;
    public GameObject nameDatu;
    public GameObject nameEnita;

    public GameObject dialogueBox;

    int weedCount = 0;
    bool weedQuest = false;

    bool dlBoxEnabler = true;

    int x;
    int scoreCount = 0;

    

    void SetUp()
    {
        // x is count that increments
        x = 0;

        //Variable Setup

        question = new Variable("Question", new string[] { "1.)	Ang lipunan ay nahahati sa tatlong antas. Ano ang tawag sa pinakamataas na antas?",
                                                               "2.)	Ang mga alipin ay nahahati pa sa tatlo: Tumarampuk, Tumataban, at Ayuey. Alin sa mga ito ang nagsisilbi sa bahay ng kanilang amo at nagtatrabaho ng tatlong araw sa kada apat na araw?",
                                                               "3.)	Isa sa mga pangunahing ikinabubuhay ng mga tao ay ang pangingisda. Gumamit sila ng iba’t ibang kagamitan tulad ng lambat, sibat, at ____________.",
                                                               "4.)	Mahalaga ang prosesong ito upang masiguro na makukuha ng mga pananim ang sustansiya ng pataba.",
                                                               "5.)	Ang datu ay may kapangyarihang gumawa at magpatupad ng mga batas. Sino naman ang nagpapahayag nito sa buong barangay?",
                                                               "6.)	Upang mapatunayan na walang sala ang naakusahan, siya ay haharap sa mga pagsubok gaya ng?",
                                                               "7.)	Bilang paggalang, ang katawan ng mga pumanaw ay inilalagay sa _______ bago ilibing. ",
                                                               "8.)	Ang buwis na ipinapataw ng mga datu sa kanyang nasasakupan ay maaaring _______.",
                                                               "9.)	Sino ang mga hindi kabilang sa pagbabayad ng buwis?",
                                                               "10.)	Iba’t ibang mga diyos ang pinaniniwalaan ng mga Pilipino noon. Sinasamba nila ang mga ito at nag-aalay sila ng mga ritwal. Sino ang Diyos ng Pag-aani?",
                                                               "11.)	Sinasamba nila at nag-aalay sila sa Diyos ng Pag-aani upang _______________.",
                                                               "12.)	Ang proseso ng kasal ay sinisimulan ng pagpapahayag ng lalaki sa buong barangay na siya ay may minamahal. Pagkatapos nito, susundan ng _____________________.",});


        c1 = new Variable("Choice1", new string[] {"a.	Timawa",
                                                    "a.	Aliping Tumataban",
                                                    "a.	matalas na bato",
                                                    "a.	paghihiwalay ng mga pananim",
                                                    "a.	isa sa mga Maginoo",
                                                    "a.	paglubog sa tubig ng matagal",
                                                    "a.	kabaong na yari sa kahoy",
                                                    "a.	bahagdan ng ani",
                                                    "a.	mga pamilyang malaki ang bilang",
                                                    "a.	Pandaki",
                                                    "a.	maiwasan ang pagkasira ng pananim dahil sa mga balang",
                                                    "a.	pagluhod ng lalaki sa harap ng bahay ng kanyang minamahal"});

        c2 = new Variable("Choice1", new string[] {"b.	Datu",
                                                   "b.	Aliping Ayuey",
                                                   "b.	kawayan",
                                                   "b.	pagtatanggal ng mga ligaw na damo",
                                                   "b.	ang datu mismo",
                                                   "b.	pagtakbo ng ilang kimetro",
                                                   "b.	sakong may simbolo",
                                                   "b.	serbisyo",
                                                   "b.	mga miyembro ng pamilya ng datu",
                                                   "b.	Sidapa",
                                                   "b.	mas mapabilis ang panahon ng anihan ",
                                                   "b.	pagpukol ng lalaki ng isang sibat sa harap ng bahay ng kanyang minamahal"});

        c3 = new Variable("Choice1", new string[] {"c.	Alipin",
                                                   "c.	Aliping Tumarampuk",
                                                   "c.	busog at pana",
                                                   "c.	paglalagay ng pestisidyo",
                                                   "c.	ang Umalohokan",
                                                   "c.	pag-akyat at pagtalon sa pinakamataas na puno",
                                                   "c.	malaking banga",
                                                   "c.	lahat ng nabanggit",
                                                   "c.	mga pamilya ng tagasunod ng datu",
                                                   "c.	Lalahon",
                                                   "c.	walang maaaksaya sa mga ani",
                                                   "c.	pagwagayway ng lalaki ng isang bandila sa harap ng bahay ng kanyang minamahal"});

    }

    void ScriptSetUp()
    {
        scriptDatu = new Variable("DatuScript", new string[] { "Kabani, alam kong gusto mong pumunta sa iyong mga kaibigan. Pinapayagan kita ngayong araw. Lagi ka lamang mag-iingat." });

        scriptLupas = new Variable("LupasScript", new string[] { "Tamang tama!Pupunta ako kay ama sa bukid upang magsaka. Malaking bagay ang iyong tulong."});

        scriptLAma = new Variable("LAmaScript", new string[] { "Masyado pa kayong bata para magsaka. Ngunit maganda ang naisip niyong pagtulong kung gusto niyong matuto. Sa ngayon ay nakapagtanim na kami ng mga palay. Ang gawin niyo na lang muna ay magtanggal ng mga ligaw na damo" });
    }

    void TextUpdates()
    {
        //Changing the texts of questions and choices every time the player answers

        if (x < 12)
        {
            Text txt = gameObject.GetComponent<Text>();

            GameObject.Find("btn1").GetComponentInChildren<Text>().text = Convert.ToString(c1.States[x]);
            GameObject.Find("btn2").GetComponentInChildren<Text>().text = Convert.ToString(c2.States[x]);
            GameObject.Find("btn3").GetComponentInChildren<Text>().text = Convert.ToString(c3.States[x]);

            txt.text = Convert.ToString(question.States[x]);
        }
    }


    // Use this for initialization
    void Start() {
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName == "Test")
        {
            SetUp();
        }
        else if(sceneName == "Barangay")
        {
            ScriptSetUp();
        }
        
    }
	
	// Update is called once per frame
	void Update () {

        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName == "Test")
        {
            TextUpdates();
        }

        //Debug.Log(scoreCount);

        NpcName();
    }

    void NpcName()
    {
        //Lupas.transform.LookAt(Camera.main.transform.position);
        //nameLupas.transform.Rotate(0,180,0);
        //nameDatu.transform.LookAt(Camera.main.transform.position);
        //nameDatu.transform.Rotate(0, 180, 0);
        //nameEnita.transform.LookAt(Camera.main.transform.position);
        //nameEnita.transform.Rotate(0, 180, 0);
    }

    void OnCollisionStay(Collision other)
    {
        Debug.Log(other.gameObject.tag);


        if (other.gameObject.tag == "Datu")
        {
            Debug.Log("Collide Success!");
            if(Input.GetButton("AButton"))
            {
                Debug.Log("Enter Success!");
                if (dlBoxEnabler)
                {
                    dialogueBox.SetActive(true);
                }
                GameObject.Find("CharacterName").GetComponentInChildren<Text>().text = "Datu Gunsad";
                GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptDatu.States[0]);

                weedCount = 1;
            }
        }
        else if (other.gameObject.tag == "Lupas")
        {
            Debug.Log("Collide Success!");
            if (Input.GetButton("AButton"))
            {
                Debug.Log("Enter Success!");
                if (dlBoxEnabler)
                {
                    dialogueBox.SetActive(true);
                }
                GameObject.Find("CharacterName").GetComponentInChildren<Text>().text = "Lupas";
                GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptLupas.States[0]);

                weedCount = 2;
            }
        }

        if (other.gameObject.tag == "AmaLupas")
        {
            Debug.Log("Collide Success!");
            if (Input.GetButton("AButton"))
            {
                Debug.Log("Enter Success!");
                if (dlBoxEnabler)
                {
                    dialogueBox.SetActive(true);
                }
                GameObject.Find("CharacterName").GetComponentInChildren<Text>().text = "Ama ni Lupas";
                GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptLAma.States[0]);

                weedCount = 3;
            }
        }

        if (other.gameObject.tag == "Grass")
        {
            Debug.Log("Grass Collide");
            if (Input.GetButton("AButton") && weedCount == 3)
            {
                DestroyObject(other.gameObject);
                Debug.Log("Grass Destroyed~!");
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        dialogueBox.SetActive(false);
    }
    





    //Button OnCliCK

    public void ChoiceOne()
    {
        if(x == 5 || x == 6 || x == 10)
        {
            scoreCount++;
            x++;
        }
        else
        {
            //this lies the effect if not answered
            x++;
        }
    }

    public void ChoiceTwo()
    {
        if (x == 0 || x == 1 || x == 3 || x == 8 || x == 11)
        {
            scoreCount++;
            x++;
        }
        else
        {
            //this lies the effect if not answered
            x++;
        }
    }

    public void ChoiceThree()
    {
        if (x == 2 || x == 4 || x == 7 || x == 9)
        {
            scoreCount++;
            x++;
        }
        else
        {
            //this lies the effect if not answered
            x++;
        }
    }
}
