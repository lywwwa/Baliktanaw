using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayesServer;
using BayesServer.Inference.RelevanceTree;
using System;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour {

    static string[] question = new string[12];
    static string[] c1 = new string[12];
    static string[] c2 = new string[12];
    static string[] c3 = new string[12];

    string[] scriptDatu = new string[10];
    string[] scriptLupas = new string[10];
    string[] scriptLAma = new string[10];
    string[] scriptEnita = new string[10];
    string[] scriptMakindo = new string[10];
    string[] scriptPiyan = new string[10];
    string[] scriptTimawa = new string[10];

    //3D Text
    //public GameObject Lupas;
    //public GameObject nameDatu;
    //public GameObject nameEnita;
    public GameObject dialogueBox;
    public GameObject questPanel;


    bool[] Quest = new bool[9];
    static bool[] q = new bool[12];

    bool dlBoxEnabler = true;

    int weedQuestCount = 0;
    int fishQuestCount = 0;
    int taroQuestCount = 0;

    bool finishWeed = false;

    public Transform player;

    BayesServer.Network network;
    Variable weedQuest, fishQuest, taroQuest, stakeQuest, taxQuest, gatherQuest, tattooQuest, religionQuest, umalQuest;
    State WTrue, WFalse;
    State FTrue, FFalse;
    State TTrue, TFalse;
    State STrue, SFalse;
    State XTrue, XFalse;
    State GTrue, GFalse;
    State OTrue, OFalse;
    State RTrue, RFalse;
    State UTrue, UFalse;
    Node weedNode;
    Node fishNode;
    Node taroNode;
    Node stakeNode;
    Node taxNode;
    Node gatherNode;
    Node tattooNode;
    Node religionNode;
    Node umalNode;

    bool stop = false;

    struct RandomSelection
    {
        private int minValue;
        private int maxValue;
        public float probability;

        public RandomSelection(int minValue, int maxValue, float probability)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.probability = probability;
        }

        public int GetValue() { return UnityEngine.Random.Range(minValue, maxValue + 1); }
    }


    int x;
    int scoreCount = 0;

    Rigidbody P_Rigidbody;


    void Questions()
    {
        for (int z = 0; z < 12; z++)
            q[z] = false;

        // x is count that increments
        x = 0;
        //Variable Setup

        string[] question = { "1.)	Ang lipunan ay nahahati sa tatlong antas: datu, timawa, at oripun. Ang mga oripun ay may iba’t ibang uri. Ano ang tawag sa mga naninirahan sa bahay ng kanilang panginoon (master) at nagtatrabaho para sa kanila ng tatlong araw sa kada apat na araw?",
                     "2.)	Sa pagtatanim, mahalaga ang prosesong ito upang masiguro na makukuha ng mga pananim ang sustansiya ng pataba.",
                     "3.)	Ang karaniwang itinatamin ng mga tao ay mga halamang ugat. Sa mga halamang ugat, ano ang itinuturing nilang pinakamasustansiya?",
                     "4.)	Ano ang prosesong ginagawa upang magkaroon ng palatandaan ang kanilang itinanim sa mga damus (a field of root crop)?",
                     "5.)	Bukod sa serbisyo, ang buwis (tribute) na natatanggap ng datu mula sa kanyang nasasakupan ay maaaring _______.",
                     "6.)	Sino ang mga hindi kabilang sa pagbabayad ng buwis (tribute)?",
                     "7.)	Isa sa mga pangunahing ikinabubuhay ng mga tao ay ang pangingisda. Gumamit sila ng iba’t ibang kagamitan tulad ng busog at pana, paggiyod (a type of net), at ____________.",
                     "8.)	Ang mga anyong tubig ay sagana sa isda. Ngunit mas pinipili ng mga taong mangisda sa mga _________ kaysa dagat upang hindi na gumamit ng pansag (a large net).",
                     "9.)	Ang batuk (tattoo) ay nagpapahiwatig ng katapangan ng mga lalaki at nagpapatunay na sila ay may naitulong sa mga digmaan. Saang bahagi ng katawan inuumpisahan ang paglalagay ng batuk?",
                     "10.)	Ang datu ay may kapangyarihang gumawa at magpatupad ng mga batas. Sino naman ang nagpapahayag nito sa buong barangay?",
                     "11.)	May mga mito (myth) na pinaniniwalaan ang mga tao na nagsasalaysay ng pinagmulan ng bagay tulad ng araw at buwan. Sa isang mito tungkol sa pinagmulan ng daigdig, anong hayop ang nagdulot ng pagkakabuo ng mga isla?",
                     "12.)	Isinalasay din sa mito ng pinagmulan ng daigdig ang pinagmulan ng unang lalaki at babae. Saan lumabas ang unang lalaki at babae?"};


        string[] c1 = {"a.	tumataban",
                       "a.	paghihiwalay ng mga pananim",
                                                    "a.	camote",
                                                    "a.	nagpuputol ng isang puno malapit sa damus",
                                                    "a.	mga alagang hayop",
                                                    "a.	mga miyembro ng pamilya ng datu",
                                                    "a.	sarapang (trident)",
                                                    "a.	lawa",
                                                    "a.	braso",
                                                    "a.	isang maginoo",
                                                    "a.	ibon",
                                                    "a.	sa kabibe"};

        string[] c2 = {"b.	ayuey",
                       "b.	pagtatanggal ng mga ligaw na damo",
                       "b.	ubi",
                       "b.	nagtutusok ng kahoy sa lupa",
                       "b.	bahagdan ng ani",
                       "b.	mga pamilyang malaki ang bilang",
                       "b.	kawayan (bamboo)",
                       "b.	talon",
                       "b.	paa",
                       "b.	ang datu mismo",
                       "b.	buwaya",
                       "b.	sa kawayan"};

        string[] c3 = {"c.	tumaranpok",
                       "c.	paglalagay ng pestisidyo",
                       "c.	taro",
                       "c.	kinukulayan ang mga nakapaligid na bakod",
                       "c.	lahat ng nabanggit",
                       "c.	mga pamilya ng sandig sa datu (supporters of datu)",
                       "c.	spear (sibat)",
                       "c.	ilog",
                       "c.	dibdib",
                       "c.	ang umalohokan",
                       "c.	baboy",
                       "c.	sa niyog"};

    }

    void Script()
    {
        string[] scriptDatu = { "Kabani, alam kong gusto mong pumunta sa iyong mga kaibigan. Pinapayagan kita ngayong araw. Lagi ka lamang mag-iingat.",
                                "Siguro ay nagtataka ka kung bakit may nagbibigay ng mga ani sa atin o kaya naman ay pinagsisilbihan tayo. Iyon ay tinatawag na buhis. Ibinibigay nila iyon sa atin kapalit ng pamumuno ko sa kanila. Hindi na kabilang sa mga nagbabayad ang mga kapamilya natin. "};

        string[] scriptEnita = { "Puntahan mo na ang iyong mga kaibigan. Pumayag naman ang iyong ama." };

        string[] scriptLupas = { "Marahil ay magsabi ka muna kay Datu Gunsad bago tayo maglaro.",
                                 "Mabuti naman at pinayagan ka ngayong araw. Tamang tama!Pupunta ako kay ama sa bukid upang magsaka. Malaking bagay ang iyong tulong.",
                                 "Sinabi sa akin ni Makindo na siya ay pupunta sa ilog. Kaya na namin ito ni ama. Puntahan mo siya at mangisda kayo."};

        string[] scriptLAma = { "Masyado pa kayong bata para magtanim ng mga halamang ugat. Ngunit maganda ang naisip niyong pagtulong kung gusto niyong matuto. Sa ngayon, ang gawin niyo na lang muna ay magtanggal ng mga ligaw na damo.",
                                "Maganda ang mga inani naming taro. Tulungan mo kaming itabi ang mga ito."};

        string[] scriptMakindo = { "Kabani! Sumama ka sa akin na mangisda sa ilog. Siguradong may matututunan ka at matutuwa ang iyong ama kapag marami kang mahuhuli.",
                                   "At dahil wala ka pang karanasan sa paghuhuli ng isda, tuturuan kita. Kunin mo iyong sarapang.",
                                   "Ang kailangan lamang nating gawin ay mag-abang tayo ng isdang lalapit. Kapag may lumapit na, saka natin tutusukin ng sarapang. Sapat na makakahuli ka ng limang isda.",
                                   "Natututo ka pa lang ngunit nakahuli ka na agad. Hindi ko iyon inaasahan. Ngayon ay marunong ka nang mangisda. "};

        string[] scriptPiyan = { "Mukhang may bagong batas na nabuo si Datu Gunsad. Marahil ay ipapahayag na ito ni Ibusun. Siya ang ating Umalohokan. Anumang batas ang mabuo ng datu, hindi ito agad ipinatutupad hangga’t hindi ito naipapahayag ng Umalohokan." };

        string[] scriptTimawa = { "Oh, Kabani. Nasaan nga pala ang iyong ama? Magbabayad kase ako ng buhis. Pagsisilbihan ko na lang siya. Hindi ko maibabayad ang mga inani namin dahil kakaunti lamang ang mga ito." };
    }

    void Text()
    {
        //Changing the texts of questions and choices every time the player answers
        if (x < 12)
        {
            Text txt = gameObject.GetComponent<Text>();
            GameObject.Find("btn1").GetComponentInChildren<Text>().text = c1[x];
            GameObject.Find("btn2").GetComponentInChildren<Text>().text = c2[x];
            GameObject.Find("btn3").GetComponentInChildren<Text>().text = c3[x];
            txt.text = Convert.ToString(question[x]);
        }
        else
        {
            Debug.Log("Loadlevel");
            //levelLoader.FadeToLevel();
            SceneManager.LoadScene(2);
        }
    }


    // Use this for initialization
    void Start() {
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName == "Test")
        {
            Text();
        }
        else if(sceneName == "Barangay")
        {
            Script();
            DynamicDecisionNetwork();

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
            Text();
        }
        else if (sceneName == "Barangay")
        {
            DDNRaycastDetection();
        }
        //Debug.Log(scoreCount);
        //NpcName();
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
        network = new BayesServer.Network();

        //Removing Weed Quest
        WTrue = new State("WTrue");
        WFalse = new State("WFalse");
        weedQuest = new Variable("Weed", WTrue, WFalse);
        weedNode = new Node(weedQuest)
        {
            TemporalType = TemporalType.Temporal // this is a time series node, hence re-used for each time slice
        };
        network.Nodes.Add(weedNode);

        //Fishing Quest
        FTrue = new State("QTrue");
        FFalse = new State("QFalse");
        fishQuest = new Variable("Fish", FTrue, FFalse);
        fishNode = new Node(fishQuest)
        {
            TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
        };
        network.Nodes.Add(fishNode);

        //Taro Quest
        TTrue = new State("TTrue");
        TFalse = new State("TFalse");
        taroQuest = new Variable("Taro", TTrue, TFalse);
        taroNode = new Node(taroQuest)
        {
            TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
        };
        network.Nodes.Add(taroNode);

        //Stake Quest
        STrue = new State("STrue");
        SFalse = new State("SFalse");
        stakeQuest = new Variable("Stake", STrue, SFalse);
        stakeNode = new Node(stakeQuest)
        {
            TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
        };
        network.Nodes.Add(stakeNode);

        //Tax Quest
        XTrue = new State("XTrue");
        XFalse = new State("XFalse");
        taxQuest = new Variable("Tax", XTrue, XFalse);
        taxNode = new Node(taxQuest)
        {
            TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
        };
        network.Nodes.Add(taxNode);

        //Gather Quest
        GTrue = new State("GTrue");
        GFalse = new State("GFalse");
        gatherQuest = new Variable("Gather", GTrue, GFalse);
        gatherNode = new Node(gatherQuest)
        {
            TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
        };
        network.Nodes.Add(gatherNode);

        //Tattoo Quest
        OTrue = new State("OTrue");
        OFalse = new State("OFalse");
        tattooQuest = new Variable("Tattoo", OTrue, OFalse);
        tattooNode = new Node(tattooQuest)
        {
            TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
        };
        network.Nodes.Add(tattooNode);

        //Religion Quest
        RTrue = new State("RTrue");
        RFalse = new State("RFalse");
        religionQuest = new Variable("Religion", RTrue, RFalse);
        religionNode = new Node(religionQuest)
        {
            TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
        };
        network.Nodes.Add(religionNode);

        //Umalohokan Quest
        UTrue = new State("UTrue");
        UFalse = new State("UFalse");
        umalQuest = new Variable("Umalohokan", UTrue, UFalse);
        umalNode = new Node(umalQuest)
        {
            TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
        };
        network.Nodes.Add(umalNode);



        // add a link from node to node
        //network.Links.Add(new Link(weedNode, weedNode, 1));

        network.Links.Add(new Link(fishNode, weedNode, 0));
        network.Links.Add(new Link(fishNode, fishNode, 1));

        network.Links.Add(new Link(taroNode, weedNode, 0));
        network.Links.Add(new Link(taroNode, taroNode, 1));

        //network.Links.Add(new Link(stakeNode, weedNode, 0));
        //network.Links.Add(new Link(stakeNode, stakeNode, 1));

        //network.Links.Add(new Link(tattooNode, tattooNode, 1));

        //network.Links.Add(new Link(religionNode, religionNode, 1));
    }

    //Query Upon receiving data
    float QueryNetwork(bool isDecision)
    {

        //Fish Quest 

        StateContext fTrueTime0 = new StateContext(FTrue, 0);
        StateContext fFalseTime0 = new StateContext(FFalse, 0);

        Table priorKnowledgeFish = fishNode.NewDistribution(0).Table;
        priorKnowledgeFish[fTrueTime0] = 0.4;
        priorKnowledgeFish[fFalseTime0] = 0.6;
        // NewDistribution does not assign the new distribution, so it still must be assigned
        fishNode.Distribution = priorKnowledgeFish;

        // the second is specified for time >= 1
        Table learnRateFish = fishNode.NewDistribution(1).Table;
        // when specifying temporal distributions, variables which belong to temporal nodes must have times associated
        // NOTE: Each time is specified relative to the current point in time which is defined as zero, 
        // therefore the time for variables at the previous time step is -1
        StateContext fTrueTime1 = new StateContext(FTrue, -1);
        StateContext fFalseTime1 = new StateContext(FFalse, -1);
        learnRateFish[fTrueTime1, fTrueTime0] = 0.5;
        learnRateFish[fFalseTime1, fTrueTime0] = 0.5;
        learnRateFish[fTrueTime1, fFalseTime0] = 0.5;
        learnRateFish[fFalseTime1, fFalseTime0] = 0.5;
        fishNode.Distributions[1] = learnRateFish;

        //Taro Quest

        StateContext tTrueTime0 = new StateContext(TTrue, 0);
        StateContext tFalseTime0 = new StateContext(TFalse, 0);

        Table priorKnowledgeTaro = taroNode.NewDistribution(0).Table;
        priorKnowledgeTaro[tTrueTime0] = 0.3;
        priorKnowledgeTaro[tFalseTime0] = 0.7;
        // NewDistribution does not assign the new distribution, so it still must be assigned
        taroNode.Distribution = priorKnowledgeTaro;

        // the second is specified for time >= 1
        Table learnRateTaro = taroNode.NewDistribution(1).Table;
        // when specifying temporal distributions, variables which belong to temporal nodes must have times associated
        // NOTE: Each time is specified relative to the current point in time which is defined as zero, 
        // therefore the time for variables at the previous time step is -1
        StateContext tTrueTime1 = new StateContext(TTrue, -1);
        StateContext tFalseTime1 = new StateContext(TFalse, -1);
        learnRateTaro[tTrueTime1, tTrueTime0] = 0.5;
        learnRateTaro[tFalseTime1, tTrueTime0] = 0.5;
        learnRateTaro[tTrueTime1, tFalseTime0] = 0.5;
        learnRateTaro[tFalseTime1, tFalseTime0] = 0.5;
        taroNode.Distributions[1] = learnRateTaro;

        //Taro Quest

        Table choiceStatusWeed = weedNode.NewDistribution().Table;
        StateContext wTrue = new StateContext(WTrue, 0);
        StateContext wFalse = new StateContext(WFalse, 0);
        choiceStatusWeed[wTrue, fTrueTime0, tTrueTime0] = 0.9;
        choiceStatusWeed[wFalse, fTrueTime0, tTrueTime0] = 0.1;
        choiceStatusWeed[wTrue, fFalseTime0, tTrueTime0] = 0.1;
        choiceStatusWeed[wFalse, fFalseTime0, tTrueTime0] = 0.9;
        choiceStatusWeed[wTrue, fTrueTime0, tFalseTime0] = 0.9;
        choiceStatusWeed[wFalse, fTrueTime0, tFalseTime0] = 0.1;
        choiceStatusWeed[wTrue, fFalseTime0, tFalseTime0] = 0.1;
        choiceStatusWeed[wFalse, fFalseTime0, tFalseTime0] = 0.9;
        weedNode.Distribution = choiceStatusWeed;

        // optional check to validate network
        //beliefNet.Validate(new ValidationOptions());
        // at this point the network has been fully specified

        // we will now perform some queries on the network
        RelevanceTreeInference inference = new RelevanceTreeInference(network);
        RelevanceTreeQueryOptions queryOptions = new RelevanceTreeQueryOptions();
        RelevanceTreeQueryOutput queryOutput = new RelevanceTreeQueryOutput();

        inference.Evidence.SetState(WTrue, 0);

        queryOptions.LogLikelihood = true; // only ask for this if you really need it
        var queryA = new Table(fishNode.NewDistribution(0).Table);
        inference.QueryDistributions.Add(queryA);
        inference.Query(queryOptions, queryOutput); // note that this can raise an exception (see help for details)

        Debug.Log("LogLikelihood: " + queryOutput.LogLikelihood.Value);

        float probability = (float)queryA[fTrueTime0];

        probability = Mathf.Round(probability * 100f) / 100f;

        return probability;
    }

    //Probability for 1-100 random selector
    int GetRandomValue(params RandomSelection[] selections)
    {
        {
            float rand = UnityEngine.Random.value;
            float currentProb = 0;
            foreach (var selection in selections)
            {
                currentProb += selection.probability;
                if (rand <= currentProb)
                    return selection.GetValue();
            }

            //will happen if the input's probabilities sums to less than 1
            //throw error here if that's appropriate
            return -1;
        }
    }

    int check = 0;
    
    void DDNRaycastDetection()
    {
        Debug.Log(Input.GetButton("XButton"));


        if (stop)
        {
            Time.timeScale = 0;
        }
        if (Input.GetButton("XButton"))
        {
            Time.timeScale = 1;
            stop = false;
            dialogueBox.SetActive(false);
        }

        RaycastHit hit;
        var fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, 100))
        {
            if (Input.GetButton("BButton"))
            {
                if (hit.collider.gameObject.tag == "Datu")
                {
                    dialogueBox.SetActive(true);
                    stop = true;
                    LookatPlayer(hit.collider.gameObject);
                }
            }
        }
    }

    void LookatPlayer(GameObject hit)
    {
        //Vector3 tempPos = new Vector3(hit.transform.position.x, hit.transform.position.z, hit.transform.position.z);
        //Vector3 tempRot = new Vector3(hit.transform.rotation.x, hit.transform.rotation.z, hit.transform.rotation.z);

        //hit.transform.Rotate(hit.transform.rotation.x, player.);

        var lookPos = player.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        hit.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);

        Vector3 rot = hit.transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z);
        hit.transform.rotation = Quaternion.Euler(rot);
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
        if (x == 2 || x == 4  || x == 7 || x == 9)
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
