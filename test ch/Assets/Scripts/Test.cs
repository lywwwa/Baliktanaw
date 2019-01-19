using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayesServer;
using BayesServer.Inference.RelevanceTree;
using System;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour {

    static Variable question;
    static Variable c1;
    static Variable c2;
    static Variable c3;

    Variable scriptDatu;
    Variable scriptLupas;
    Variable scriptLAma;
    Variable scriptEnita;
    Variable scriptMakindo;
    Variable scriptPiyan;
    Variable scriptTimawa;


    public GameObject Lupas;
    public GameObject nameDatu;
    public GameObject nameEnita;
    public GameObject dialogueBox;
    public GameObject questPanel;


    bool[] Quest = new bool[9];
    static bool[] q = new bool[12];

    bool dlBoxEnabler = true;

    int weedQuestCount = 0;
    int fishQuestCount = 0;
    int taroQuestCount = 0;

    bool finishWeed = false;


    int x;
    int scoreCount = 0;

    Rigidbody P_Rigidbody;

    LoadScreen loadScreen;


    void SetUp()
    {
        for (int z = 0; z < 12; z++)
            q[z] = false;

        // x is count that increments
        x = 0;

        //Variable Setup

        question = new Variable("Question", new string[] { "1.)	Ang lipunan ay nahahati sa tatlong antas. Ano ang tawag sa pinakamataas na antas?",
                                                               "2.)	Ang mga oripun ay may iba’t ibang uri. Ano ang tawag sa mga naninirahan sa bahay ng kanilang panginoon (master) at nagtatrabaho para sa kanila ng tatlong araw sa kada apat na araw?",
                                                               "3.)	Sa pagtatanim, mahalaga ang prosesong ito upang masiguro na makukuha ng mga pananim ang sustansiya ng pataba.",
                                                               "4.)	Sa Kabisayaan, ang karaniwang itinatamin ay mga halamang ugat. Sa mga halamang ugat, ano ang itinuturing nilang pinakamasustansiya?",
                                                               "5.)	Ano ang prosesong ginagawa upang magkaroon ng palatandaan ang kanilang itinanim sa mga damus (a field of root crop)?",
                                                               "6.)	Ang buhis (tribute) na natatanggap ng datu mula sa kanyang nasasakupan ay maaaring _______.",
                                                               "7.)	Sino ang mga hindi kabilang sa pagbabayad ng buhis (tribute)?",
                                                               "8.)	Isa sa mga pangunahing ikinabubuhay ng mga tao ay ang pangingisda. Gumamit sila ng iba’t ibang kagamitan tulad ng busog at pana, paggiyod (a type of net), at ____________.",
                                                               "9.)	Ang mga anyong tubig ay sagana sa isda. Ang mga naninirahan malapit sa dagat ay mas madalas mangisda sa mga _________ upang hindi na gumamit ng pansag (a large net).",
                                                               "10.)    Ang batuk (tattoo) ay nagpapahiwatig ng katapangan ng mga lalaki at nagpapatunay na sila ay may naitulong sa mga digmaan. Saang bahagi ng katawan inuumpisahan ang paglalagay ng batuk?",
                                                               "11.)	Ang datu ay may kapangyarihang gumawa at magpatupad ng mga batas. Sino naman ang nagpapahayag nito sa buong barangay?",
                                                               "12.)	Ang pagiging datu ay namamana. Kapag maraming anak ang isang datu, kadalasan ay sa ________ niya ito ipinapasa.",});


        c1 = new Variable("Choice1", new string[] {"a.	Timawa",
                                                    "a.	tumataban",
                                                    "a.	paghihiwalay ng mga pananim",
                                                    "a.	ubi",
                                                    "a.	Nagpuputol ng isang puno malapit sa damus",
                                                    "a.	bahagdan ng ani",
                                                    "a.	mga miyembro ng pamilya ng datu",
                                                    "a.	sibat (spear)",
                                                    "a.	lawa",
                                                    "a.	braso",
                                                    "a.	ang umalohokan",
                                                    "a.	pinakamatandang babae na nabubuhay pa"});

        c2 = new Variable("Choice2", new string[] {"b.	Datu",
                                                   "b.	ayuey",
                                                   "b.	paglalagay ng pestisidyo",
                                                   "b.	taro",
                                                   "b.	kinukulayan ang mga nakapaligid na bakod",
                                                   "b.	serbisyo",
                                                   "b.	mga pamilyang malaki ang bilang",
                                                   "b.	kawayan",
                                                   "b.	ilog",
                                                   "b.	dibdib",
                                                   "b.	ang datu mismo",
                                                   "b.	pinakamatandang lalaki na nabubuhay pa"});

        c3 = new Variable("Choice3", new string[] {"c.	oripun",
                                                   "c.	tumaranpok",
                                                   "c.	pagtatanggal ng mga ligaw na damo",
                                                   "c.	camote",
                                                   "c.	nagtutusok ng kahoy sa lupa",
                                                   "c.	lahat ng nabanggit",
                                                   "c.	mga pamilya ng sandig sa datu (supporters of datu)",
                                                   "c.	sarapang (trident)",
                                                   "c.	talon",
                                                   "c.	paa",
                                                   "c.	isang maginoo (elder)",
                                                   "c.	pinakapaboritong anak na nabubuhay pa"});

    }

    void ScriptSetUp()
    {
        scriptDatu = new Variable("DatuScript", new string[] { "Kabani, alam kong gusto mong pumunta sa iyong mga kaibigan. Pinapayagan kita ngayong araw. Lagi ka lamang mag-iingat.",
                                                               "Siguro ay nagtataka ka kung bakit may nagbibigay ng mga ani sa atin o kaya naman ay pinagsisilbihan tayo. Iyon ay tinatawag na buhis. Ibinibigay nila iyon sa atin kapalit ng pamumuno ko sa kanila. Hindi na kabilang sa mga nagbabayad ang mga kapamilya natin.  "});

        scriptEnita = new Variable("EnitaScript", new string[] { "Puntahan mo na ang iyong mga kaibigan. Pumayag naman ang iyong ama." });

        scriptLupas = new Variable("LupasScript", new string[] { "Marahil ay magsabi ka muna kay Datu Gunsad bago tayo maglaro.",
                                                                 "Mabuti naman at pinayagan ka ngayong araw. Tamang tama!Pupunta ako kay ama sa bukid upang magsaka. Malaking bagay ang iyong tulong.",
                                                                 "Sinabi sa akin ni Makindo na siya ay pupunta sa ilog. Kaya na namin ito ni ama. Puntahan mo siya at mangisda kayo."});

        scriptLAma = new Variable("LAmaScript", new string[] { "Masyado pa kayong bata para magtanim ng mga halamang ugat. Ngunit maganda ang naisip niyong pagtulong kung gusto niyong matuto. Sa ngayon, ang gawin niyo na lang muna ay magtanggal ng mga ligaw na damo.",
                                                               "Maganda ang mga inani naming taro. Tulungan mo kaming itabi ang mga ito."});

        scriptMakindo = new Variable("MakindoScript", new string[] { "Kabani! Sumama ka sa akin na mangisda sa ilog. Siguradong may matututunan ka at matutuwa ang iyong ama kapag marami kang mahuhuli.",
                                                                     "At dahil wala ka pang karanasan sa paghuhuli ng isda, tuturuan kita. Kunin mo iyong sarapang.",
                                                                     "Ang kailangan lamang nating gawin ay mag-abang tayo ng isdang lalapit. Kapag may lumapit na, saka natin tutusukin ng sarapang. Sapat na makakahuli ka ng limang isda.",
                                                                     "Natututo ka pa lang ngunit nakahuli ka na agad. Hindi ko iyon inaasahan. Ngayon ay marunong ka nang mangisda. "});

        scriptPiyan = new Variable("PiyanScript", new string[] { "Mukhang may bagong batas na nabuo si Datu Gunsad. Marahil ay ipapahayag na ito ni Ibusun. Siya ang ating Umalohokan. Anumang batas ang mabuo ng datu, hindi ito agad ipinatutupad hangga’t hindi ito naipapahayag ng Umalohokan." });

        scriptTimawa = new Variable("EnitaScript", new string[] { "Oh, Kabani. Nasaan nga pala ang iyong ama? Magbabayad kase ako ng buhis. Pagsisilbihan ko na lang siya. Hindi ko maibabayad ang mga inani namin dahil kakaunti lamang ang mga ito." });
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
        else
        {
            Debug.Log("Loadlevel");
            loadScreen.LoadLevel();
            //levelLoader.FadeToLevel();
            //SceneManager.LoadScene("Barangay");
        }
    }


    // Use this for initialization
    void Start() {
        Scene currentScene = SceneManager.GetActiveScene();

        loadScreen=GetComponent<LoadScreen>();
        string sceneName = currentScene.name;

        if (sceneName == "Test")
        {
            SetUp();
        }
        else if(sceneName == "Barangay")
        {
            ScriptSetUp();

            P_Rigidbody = this.GetComponent<Rigidbody>();

            for (int z = 0; z < 9; z++)
                Quest[z] = false;

            q[8] = true;
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

        if (sceneName == "Barangay")
        {
            DynamicDecisionNetwork();
        }

        DDNRaycastDetection();

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

    int count = 0;

    void DynamicDecisionNetwork()
    {
        var network = new BayesServer.Network();

        //Pre-Test Nodes

        var question1 = new Variable("QuestionOne", new string[] { "False", "True" });
        var question2 = new Variable("QuestionTwo", new string[] { "False", "True" });
        var question3 = new Variable("QuestionThree", new string[] { "False", "True" });
        var question4 = new Variable("QuestionFour", new string[] { "False", "True" });
        var question5 = new Variable("QuestionFive", new string[] { "False", "True" });
        var question6 = new Variable("QuestionSix", new string[] { "False", "True" });
        var question7 = new Variable("QuestionSeven", new string[] { "False", "True" });
        var question8 = new Variable("QuestionEight", new string[] { "False", "True" });
        var question9 = new Variable("QuestionNine", new string[] { "False", "True" });
        var question10 = new Variable("QuestionTen", new string[] { "False", "True" });
        var question11 = new Variable("QuestionEleven", new string[] { "False", "True" });
        var question12 = new Variable("QuestionTwelve", new string[] { "False", "True" });

        var nodeQuestion1 = new Node(question1);
        network.Nodes.Add(nodeQuestion1);
        var nodeQuestion2 = new Node(question2);
        network.Nodes.Add(nodeQuestion2);
        var nodeQuestion3 = new Node(question3);
        network.Nodes.Add(nodeQuestion3);
        var nodeQuestion4 = new Node(question4);
        network.Nodes.Add(nodeQuestion4);
        var nodeQuestion5 = new Node(question5);
        network.Nodes.Add(nodeQuestion5);
        var nodeQuestion6 = new Node(question6);
        network.Nodes.Add(nodeQuestion6);
        var nodeQuestion7 = new Node(question7);
        network.Nodes.Add(nodeQuestion7);
        var nodeQuestion8 = new Node(question8);
        network.Nodes.Add(nodeQuestion8);
        var nodeQuestion9 = new Node(question9);
        network.Nodes.Add(nodeQuestion9);
        var nodeQuestion10 = new Node(question10);
        network.Nodes.Add(nodeQuestion10);
        var nodeQuestion11 = new Node(question11);
        network.Nodes.Add(nodeQuestion11);
        var nodeQuestion12 = new Node(question12);
        network.Nodes.Add(nodeQuestion12);

        //Quest Nodes
        var quest1 = new Variable("QuestOne", new string[] { "False", "True" });
        var quest2 = new Variable("QuestTwo", new string[] { "False", "True" });
        var quest3 = new Variable("QuestThree", new string[] { "False", "True" });
        var quest4 = new Variable("QuestFour", new string[] { "False", "True" });
        var quest5 = new Variable("QuestFive", new string[] { "False", "True" });
        var quest6 = new Variable("QuestSix", new string[] { "False", "True" });
        var quest7 = new Variable("QuestSeven", new string[] { "False", "True" });
        var quest8 = new Variable("QuestEight", new string[] { "False", "True" });
        var quest9 = new Variable("QuestNine", new string[] { "False", "True" });
        var quest10 = new Variable("QuestTen", new string[] { "False", "True" });

        var intro = new Node(quest1);
        network.Nodes.Add(intro);

        var infoOripun = new Node(quest2);
        network.Nodes.Add(infoOripun);

        var weedQuest = new Node(quest3);
        network.Nodes.Add(weedQuest);

        var storingQuest = new Node(quest4);
        network.Nodes.Add(storingQuest);

        var infoStick = new Node(quest5);
        network.Nodes.Add(infoStick);

        var infoTax = new Node(quest6);
        network.Nodes.Add(infoTax);

        var fishingQuest = new Node(quest7);
        network.Nodes.Add(fishingQuest);

        var infoTats = new Node(quest8);
        network.Nodes.Add(infoTats);

        var umalQuest = new Node(quest9);
        network.Nodes.Add(umalQuest);

        var ending = new Node(quest10);
        network.Nodes.Add(ending);

        //Node for Ending
        var endGame = new Variable("EndGame", new string[] { "False", "True" });
        var finishWeedQuest = new Variable("FinishedWeedQuest", new string[] { "False", "True" });

        var nodeEnd = new Node(endGame);
        network.Nodes.Add(nodeEnd);
        var nodeFinishWeed = new Node(finishWeedQuest);
        network.Nodes.Add(nodeFinishWeed);


        //adding directed links

        network.Links.Add(new Link(intro, nodeQuestion1));
        //network.Links.Add(new Link(infoOripun, nodeQuestion2));
        network.Links.Add(new Link(weedQuest, nodeQuestion3));
        network.Links.Add(new Link(storingQuest, nodeQuestion4));
        network.Links.Add(new Link(storingQuest, nodeFinishWeed));
        //network.Links.Add(new Link(infoStick, nodeQuestion5));
        network.Links.Add(new Link(infoTax, nodeQuestion6));
        network.Links.Add(new Link(infoTax, nodeQuestion7));
        network.Links.Add(new Link(fishingQuest, nodeQuestion8));
        network.Links.Add(new Link(fishingQuest, nodeQuestion9));
        //network.Links.Add(new Link(infoTats, nodeQuestion10));
        network.Links.Add(new Link(umalQuest, nodeQuestion11));

        //ending links
        network.Links.Add(new Link(ending, nodeEnd));

        //Setting new Distribution of Probability
        var tableA = intro.NewDistribution().Table;
        
        tableA[quest1.States[0]] = 0.51;
        tableA[quest1.States[1]] = 0.49;

        intro.Distribution = tableA;

        var tableB = nodeQuestion1.NewDistribution().Table;
        tableB[quest1.States[0], question1.States[0]] = 0.3;
        tableB[quest1.States[0], question1.States[1]] = 0.7;
        tableB[quest1.States[1], question1.States[0]] = 0.9;
        tableB[quest1.States[1], question1.States[1]] = 0.1;

        nodeQuestion1.Distribution = tableB;

        //Weed Quest Distribution
        var tableC = weedQuest.NewDistribution().Table;

        tableC[quest3.States[0]] = 0.51;
        tableC[quest3.States[1]] = 0.49;

        weedQuest.Distribution = tableC;

        var tableD = nodeQuestion3.NewDistribution().Table;
        tableD[quest3.States[0], question3.States[0]] = 0.3;
        tableD[quest3.States[0], question3.States[1]] = 0.7;
        tableD[quest3.States[1], question3.States[0]] = 0.9;
        tableD[quest3.States[1], question3.States[1]] = 0.1;

        nodeQuestion3.Distribution = tableD;

        //Fishing Quest Distribution
        var tableE = fishingQuest.NewDistribution().Table;

        tableE[quest7.States[0]] = 0.51;
        tableE[quest7.States[1]] = 0.49;

        fishingQuest.Distribution = tableE;

        var tableF = nodeQuestion8.NewDistribution().Table;
        tableF[quest7.States[0], question8.States[0]] = 0.3;
        tableF[quest7.States[0], question8.States[1]] = 0.7;
        tableF[quest7.States[1], question8.States[0]] = 0.9;
        tableF[quest7.States[1], question8.States[1]] = 0.1;

        nodeQuestion8.Distribution = tableF;

        var tableG = nodeQuestion9.NewDistribution().Table;
        tableG[quest7.States[0], question9.States[0]] = 0.3;
        tableG[quest7.States[0], question9.States[1]] = 0.7;
        tableG[quest7.States[1], question9.States[0]] = 0.9;
        tableG[quest7.States[1], question9.States[1]] = 0.1;

        nodeQuestion9.Distribution = tableG;

        //taro Quest
        var tableH = storingQuest.NewDistribution().Table;

        tableH[quest4.States[0]] = 0.51;
        tableH[quest4.States[1]] = 0.49;

        storingQuest.Distribution = tableH;

        var tableI = nodeQuestion4.NewDistribution().Table;
        tableI[quest4.States[0], question4.States[0]] = 0.3;
        tableI[quest4.States[0], question4.States[1]] = 0.7;
        tableI[quest4.States[1], question4.States[0]] = 0.9;
        tableI[quest4.States[1], question4.States[1]] = 0.1;

        nodeQuestion4.Distribution = tableI;

        var tableJ = nodeFinishWeed.NewDistribution().Table;
        tableJ[quest4.States[0], finishWeedQuest.States[0]] = 0.3;
        tableJ[quest4.States[0], finishWeedQuest.States[1]] = 0.7;
        tableJ[quest4.States[1], finishWeedQuest.States[0]] = 0.9;
        tableJ[quest4.States[1], finishWeedQuest.States[1]] = 0.1;

        nodeFinishWeed.Distribution = tableJ;

        //Umalohokan
        var tableK = umalQuest.NewDistribution().Table;

        tableK[quest9.States[0]] = 0.51;
        tableK[quest9.States[1]] = 0.49;

        umalQuest.Distribution = tableK;

        var tableL = nodeQuestion11.NewDistribution().Table;
        tableL[quest9.States[0], question11.States[0]] = 0.3;
        tableL[quest9.States[0], question11.States[1]] = 0.7;
        tableL[quest9.States[1], question11.States[0]] = 0.9;
        tableL[quest9.States[1], question11.States[1]] = 0.1;

        nodeQuestion11.Distribution = tableL;

        //Tax
        var tableM = infoTax.NewDistribution().Table;

        tableM[quest6.States[0]] = 0.51;
        tableM[quest6.States[1]] = 0.49;

        infoTax.Distribution = tableM;

        var tableN = nodeQuestion6.NewDistribution().Table;
        tableN[quest6.States[0], question6.States[0]] = 0.3;
        tableN[quest6.States[0], question6.States[1]] = 0.7;
        tableN[quest6.States[1], question6.States[0]] = 0.9;
        tableN[quest6.States[1], question6.States[1]] = 0.1;

        nodeQuestion6.Distribution = tableN;

        var tableO = nodeQuestion7.NewDistribution().Table;
        tableO[quest6.States[0], question7.States[0]] = 0.3;
        tableO[quest6.States[0], question7.States[1]] = 0.7;
        tableO[quest6.States[1], question7.States[0]] = 0.9;
        tableO[quest6.States[1], question7.States[1]] = 0.1;

        nodeQuestion7.Distribution = tableO;

        //instantiate

        var factory = new RelevanceTreeInferenceFactory();
        var inference = factory.CreateInferenceEngine(network);
        var queryOptions = factory.CreateQueryOptions();
        var queryOutput = factory.CreateQueryOutput();

        //Setting a State True

        if (!q[2])
            inference.Evidence.SetState(question3.States[0]);

        if (!q[7] || !q[8])
        {
            if (q[7])
                inference.Evidence.SetState(question8.States[0]);
            if (q[8])
                inference.Evidence.SetState(question9.States[0]);
        }

        if (!q[3] && finishWeed)
        {
            inference.Evidence.SetState(question4.States[0]);
            inference.Evidence.SetState(finishWeedQuest.States[0]);
        }

        if ((!q[5] || !q[6]) && finishWeed)
        {
            inference.Evidence.SetState(question6.States[0]);
            inference.Evidence.SetState(question7.States[0]);
        }

        if (!q[8])
        {
            inference.Evidence.SetState(question11.States[0]);
        }



        //Activating the probability of Weed Quest

        var queryA = new Table(weedQuest);
        inference.QueryDistributions.Add(queryA);
        inference.Query(queryOptions, queryOutput);

        var queryB = new Table(fishingQuest);
        inference.QueryDistributions.Add(queryB);
        inference.Query(queryOptions, queryOutput);

        var queryC = new Table(storingQuest);
        inference.QueryDistributions.Add(queryC);
        inference.Query(queryOptions, queryOutput);

        var queryD = new Table(umalQuest);
        inference.QueryDistributions.Add(queryD);
        inference.Query(queryOptions, queryOutput);

        var queryE = new Table(infoTax);
        inference.QueryDistributions.Add(queryE);
        inference.Query(queryOptions, queryOutput);


        if (queryA[quest3.States[1]] < queryA[quest3.States[0]])
        {
            Quest[2] = true;
        }
        else
        {
            Quest[2] = false;
        }

        if (queryB[quest7.States[1]] < queryB[quest7.States[0]])
        {
            Quest[6] = true;
        }
        else
        {
            Quest[6] = false;
        }

        if (queryC[quest4.States[1]] < queryC[quest4.States[0]])
        {
            Quest[3] = true;
        }
        else
        {
            Quest[3] = false;
        }


        if (queryD[quest9.States[1]] < queryD[quest9.States[0]])
        {
            Quest[8] = true;
        }
        else
        {
            Quest[8] = false;
        }


        if (queryE[quest6.States[1]] < queryE[quest6.States[0]])
        {
            Quest[5] = true;
        }
        else
        {
            Quest[5] = false;
        }
    }

    int weedCount = 0;
    int fishCount = 0;
    int taroCount = 0;
    int timawa = 1;

    void DDNRaycastDetection()
    {
        RaycastHit hit;

        var fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, 300))
        {
            //Debug.Log(hit.collider.gameObject.tag);

            //if (false)
            if (true)
            {
                Debug.Log(hit.collider.gameObject.tag);
                if (Input.GetButton("AButton"))
                {

                    if (hit.collider.gameObject.tag == "Datu")
                    {
                        dialogueBox.SetActive(true);

                        GameObject.Find("CharacterName").GetComponentInChildren<Text>().text = "Datu Gunsad";
                        GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptDatu.States[0]);

                        weedQuestCount = 1;
                    }
                    else if (hit.collider.gameObject.tag == "Enita")
                    {
                        dialogueBox.SetActive(true);

                        GameObject.Find("CharacterName").GetComponentInChildren<Text>().text = "Enita";
                        GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptEnita.States[0]);

                        weedQuestCount = 1;
                    }

                    if (hit.collider.gameObject.tag == "Lupas")
                    {

                        dialogueBox.SetActive(true);
                        GameObject.Find("CharacterName").GetComponentInChildren<Text>().text = "Lupas";

                        if (weedQuestCount == 0)
                        {
                            GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptLupas.States[0]);
                        }
                        else if (weedQuestCount == 1)
                        {
                            GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptLupas.States[1]);
                            //Teleport player and lupas sa bukid

                            weedQuestCount = 2;
                        }
                    }

                    if (hit.collider.gameObject.tag == "AmaLupas" && weedQuestCount == 2)
                    {
                        dialogueBox.SetActive(true);

                        GameObject.Find("CharacterName").GetComponentInChildren<Text>().text = "Ama ni Lupas";
                        GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptLAma.States[0]);

                        questPanel.SetActive(true);
                        GameObject.Find("QuestText").GetComponentInChildren<Text>().text = Convert.ToString("Bumunot ng mga damo");

                        weedQuestCount = 3;
                    }
                    if (hit.collider.gameObject.tag == "Grass" && weedQuestCount == 3)
                    {
                        Destroy(hit.transform.gameObject);
                        weedCount++;
                        if (weedCount == 6)
                        {
                            questPanel.SetActive(false);
                            q[2] = false;
                            finishWeed = true;
                        }
                        Debug.Log(weedCount);
                    }
                }
            }

            if (Quest[3])
            {
                Debug.Log(hit.collider.gameObject.tag);
                if (Input.GetButton("AButton"))
                {
                    if (hit.collider.gameObject.tag == "AmaLupas" && weedQuestCount == 2)
                    {
                        dialogueBox.SetActive(true);

                        GameObject.Find("CharacterName").GetComponentInChildren<Text>().text = "Ama ni Lupas";
                        GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptLAma.States[1]);

                        questPanel.SetActive(true);
                        GameObject.Find("QuestText").GetComponentInChildren<Text>().text = Convert.ToString("Kunin ang mga Taro");

                        taroQuestCount = 1;
                    }
                    if (hit.collider.gameObject.tag == "Taro" && taroQuestCount == 1)
                    {
                        Destroy(hit.transform.gameObject);
                        taroCount++;
                        if (taroCount == 4)
                        {
                            questPanel.SetActive(false);
                            q[3] = false;
                        }
                    }
                }
            }

            if (Quest[5])
            {
                Debug.Log(hit.collider.gameObject.tag);
                if (Input.GetButton("AButton"))
                {
                    if (hit.collider.gameObject.tag == "TimawaBuwis")
                    {
                        dialogueBox.SetActive(true);

                        GameObject.Find("CharacterName").GetComponentInChildren<Text>().text = "Timawang nagbabayad ng buwis";
                        GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptTimawa.States[0]);

                        timawa = 1;
                    }

                    if ((hit.collider.gameObject.tag == "Datu") && (timawa == 1))
                    {
                        GameObject.Find("CharacterName").GetComponentInChildren<Text>().text = "Datu Gunsad";
                        GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptDatu.States[1]);

                        q[5] = false;
                    }
                }
            }

            //if (true)
            if (Quest[6])
            {
                Debug.Log(hit.collider.gameObject.tag);
                if (Input.GetButton("AButton"))
                {
                    if (hit.collider.gameObject.tag == "Lupas")
                    {
                        dialogueBox.SetActive(true);

                        GameObject.Find("CharacterName").GetComponentInChildren<Text>().text = "Lupas";
                        GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptLupas.States[2]);

                        fishQuestCount = 1;
                    }

                    if (hit.collider.gameObject.tag == "Makindo")
                    {
                        dialogueBox.SetActive(true);
                        GameObject.Find("CharacterName").GetComponentInChildren<Text>().text = "Makindo";

                        if (fishQuestCount == 1 || fishQuestCount == 0)
                        {
                            GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptMakindo.States[0]);
                            fishQuestCount = 2;
                        }
                        if (fishQuestCount == 2)
                        {
                            questPanel.SetActive(true);
                            GameObject.Find("QuestText").GetComponentInChildren<Text>().text = Convert.ToString("Manghuli ng Isda");

                            GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptMakindo.States[2]);
                            fishQuestCount = 2;
                        }
                        if (fishQuestCount == 3)
                        {
                            GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptMakindo.States[3]);
                        }
                    }

                    if (hit.collider.gameObject.tag == "Fish" && fishQuestCount == 2)
                    {
                        Destroy(hit.transform.gameObject);
                        fishCount++;
                        if (fishCount == 5)
                        {
                            questPanel.SetActive(false);
                            q[6] = false;

                            fishQuestCount = 3;
                        }
                    }
                }
            }

            if (Quest[8])
            {
                Debug.Log(hit.collider.gameObject.tag);
                if (Input.GetButton("AButton"))
                {
                    if (hit.collider.gameObject.tag == "Piyan")
                    {
                        dialogueBox.SetActive(true);

                        GameObject.Find("CharacterName").GetComponentInChildren<Text>().text = "Piyan";
                        GameObject.Find("dialoguetext").GetComponentInChildren<Text>().text = Convert.ToString(scriptPiyan.States[0]);

                        q[8] = false;
                    }
                }
            }


            if (hit.collider.gameObject.tag == "Untagged")
            {
                dialogueBox.SetActive(false);
                //P_Rigidbody.constraints = RigidbodyConstraints.None;
            }
        }
    }
    //void OnCollisionExit(Collision other)
    //{
    //    dialogueBox.SetActive(false);
    //}






    //Button OnCliCK

    public void ChoiceOne()
    {
        if(x == 6 || x == 10)
        {
            scoreCount++;
            x++;
        }
        else
        {
            q[x] = true;
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
            q[x] = true;
            //this lies the effect if not answered
            x++;
        }
    }

    public void ChoiceThree()
    {
        if (x == 2 || x == 4 || x == 5 || x == 7 || x == 9)
        {
            scoreCount++;
            x++;
        }
        else
        {
            q[x] = true;
            //this lies the effect if not answered
            x++;
        }
    }
}
