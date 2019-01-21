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
    string[] scriptAmal = new string[10];
    string[] scriptEnita = new string[10];
    string[] scriptMakindo = new string[10];
    string[] scriptPiyan = new string[10];
    string[] scriptTimawa = new string[10];
    string[] scriptIbusun = new string[10];
    string[] scriptSilanday = new string[10];
    string[] scriptBiraman = new string[10];
    string[] scriptRarak = new string[10];
    string[] scriptDarok = new string[10];

    //3D Text
    //public GameObject Lupas;
    //public GameObject nameDatu;
    //public GameObject nameEnita;
    public GameObject dialogueBox;
    public GameObject questPanel;


    bool[] Quest = new bool[9];
    static bool[] q = new bool[12];

    bool dlBoxEnabler = true;

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
        string[] scriptDatu = { "Puntahan mo ang kaibigan mong si Lupas. Kanina ay hinahanap ka niya.",
                                "Samahan mo muna ang iyong mga kaibigan at ako ay may iniisip.",
                                "Puntahan mo ang kaibigan mong si Makindo. Kanina ay hinahanap ka niya.",
                                "Puntahan mo ang kaibigan mong si Piyan. Kanina ay hinahanap ka niya.",
                                "Galing ito kay Silanday? Siguro ay nagtataka ka kung bakit may nagbibigay ng mga ani sa atin o kaya alagang hayop. Iyon ay tinatawag na buwis. Ibinibigay nila iyon sa atin kapalit ng pamumuno ko sa kanila. Hindi na kabilang sa mga nagbabayad ang mga kapamilya natin.",
                                "Pumunta ka sa iyong ina at tulungan mo siya.",
                                "Ang imbakan natin ay nasa baba ng bahay."};

        string[] scriptEnita = { "Bisitahin mo ang iyong kaibigang si Lupas.",
                                 "Kabani, bisitahin mo ang iyong mga kaibigan.",
                                 "Bisitahin mo ang iyong kaibigang si Piyan.",
                                 "Kabani, anak. Maghahanda na ako ng ating kakainin. Pumunta ka sa baba sa ating imbakan at kumuha ka ng isang taro.",
                                 "Ang taro ay halamang ugat na bilugan, kayumanggi ang balat, at maputi ang laman.",
                                 "Salamat sa pagkuha ng taro, anak. Sagana man tayo sa mga halamang ugat, ang pinakamasustansiya sa lahat ay ang taro. Bukod pa roon, maraming gamit ang halamang iyon – ang apay (leaves) ay ginagamit sa pagbalot ng pagkaing iihawin at ang laon (edible leaves) ay maaaring kainin."};

        string[] scriptLupas = { "Kabani, pupunta ako kay ama. Sumama ka sa akin upang may matutunan ka.",
                                 "Alamin natin kay ama kung ano ang maaari nating maitulong.",
                                 "Marahil ay inihahanda pa lang natin ang lupa.",
                                 "Ngayon ay may natutunan na tayo sa pagtatanim",
                                 "Sinabi sa akin ni Makindo ay pupunta siya sa ilog mamaya.",
                                 "Baka nasa ilog na si Makindo.",
                                 "Nahiligan ko din ang pagtatanim tulad ni ama.",
                                 "Hindi ko akalaing magkakaroon ako ng kaibigang datu. Isa lang naman kasi akong tumataban. Hindi tulad mo, nagsisilbi ako sa aking panginoon ng limang araw sa isang buwan."};

        string[] scriptAmal = { "Ang pagtatanim ay gawain naming mga oripun. Ngunit maganda ang naisip mong pagtulong kung gusto mong matuto. Sa ngayon, ang gawin mo na lang muna ay magtanggal ng mga ligaw na damo. Kailangan ay matanggal mo lahat.",
                                "Kailangan ay walang matitirang damo.",
                                "Mahalaga ang naitulong mong pagtatanggal ng mga ligaw na damo sa paghahanda ng lupang tataniman. Kapag hindi tinanggal ang mga iyon, sila ang kukuha ng sustansiya ng pataba na dapat ay sa mga pananim. Huwag mo iyong kakalimutan. Sa ngayon, ako na lang muna ang magtatanim.",
                                "Para sa akin, masaya ang pagtatanim kahit matagal ang paghihintay bago mag-ani.",
                                "Mukhang nakapagtanim na ang asawa ng aming kapitbahay na si Darok. Ang asawa niya ang  kumikilos sa kanila dahil siya ay lumpo.",
                                "Napansin kong wala pang palatandaan ang kanilang pananim."};

        string[] scriptMakindo = { "Napakarami talagang isda sa ilog.",
                                   "Kabani! Sumama ka sa akin na mangisda sa ilog. Kumuha ka muna ng sarapang kay Rarak. Pagkakuha mo ay puntahan mo ako sa ilog.",
                                   "Kunin mo muna kay Rarak ang sarapang bago tayo pumunta sa ilog.",
                                   "Mauna na ako sa ilog.",
                                   "Ang kailangan mo lang gawin ay mag-abang tayo ng isdang lalapit. Kapag may lumapit na, saka mo tutusukin ng sarapang. Sapat na kapg nakahuli ka ng limang isda.",
                                   "Natututo ka pa lang ngunit nakahuli ka na agad. Hindi ko iyon inaasahan. Ngayon ay marunong ka nang mangisda. Ako na ang bahala sa mga nahuli mong isda at dadalhin ko ito sa iyong ama.",
                                   "Mabuti ka pa, anak ka ng ating datu. Lahat ng oras mo ay sa iyo lamang. Ako bilang tumaranpok, may panginoon akong pinagsisilbihan ng apat na araw kada linggo."};

        string[] scriptPiyan = { "Magandang umaga, Kabani.",
                                 "Ito ang bahay ng aking panginoon na si Silanday. Mukhang hinahanap niya ang iyong ama.",
                                 "Manok lang ang inaalagaan naming hayop.",
                                 "Alam mo bang ipinanganak kang mapalad, Kabani? May sarili kang tahanan. Kaming mga ayuey, nakatira kami sa bahay ng aming panginoon at sila ang nagbibigay sa amin ng damit at pagkain. Pinagsisilbihan din namin sila ng tatlong araw sa kada apat na araw."};

        string[] scriptTimawa = { "Oh, Kabani. Nasaan nga pala ang iyong ama? Magbabayad kase ako ng buhis. Pagsisilbihan ko na lang siya. Hindi ko maibabayad ang mga inani namin dahil kakaunti lamang ang mga ito." };

        string[] scriptSilanday = { "Ngayon ang libreng araw ni Piyan kaya makakasama mo siya.",
                                    "Kabani! Nasaan nga pala ang iyong ama? Magbabayad kasi ako ng buwis. Hindi ko maibabayad ang mga inani namin dahil kakaunti lamang ang mga ito. Isang manok na lang ang ibabayad ko at sa iyo ko na lang ito ipapahatid.",
                                    "Kumuha ka ng isang manok sa aming bakuran at ibigay mo iyon kay Datu Gunsad. Pakisabi na iyon ay kabayaran sa aking buwis. Salamat!"};

        string[] scriptIbusun = { "Ako ang Umalohokan ng ating barangay.",
                                  "Mukhang abala ang iyong ina at kailangan niya ng tulong.",
                                  "Ang mabuting anak ay marunong sumunod sa anumang utos ng kanyang magulang."};

        string[] scriptDarok = { "Maganda ang panahon ngayon, hindi ba Kabani?",
                                 "Nakapagtanim na ang asawa ko ng mga halamang ugat. Ngunit umalis siya agad at may pinuntahan. Nakalimutan niyang maglagay ng palatandaan. Hindi ko ito magagawa dahil ako ay lumpo. Maaari ba akong tulungan?",
                                 "Salamat iyong tulong. Ngayon ay may palatandaan na kami na amin ang mga pananim na iyon."};

        string[] scriptBiraman = { "Kabani, nais kong magsalaysay ng isang mito. Tungkol ito sa pinagmulan ng daigdig at ang unang lalaki at babae.",
                                   "Noong unang panahon ay wala kundi langit, dagat, at isang ibong lipad ng lipad sa pagitan.",
                                   "Sa katagalan ng paglipad ay napagod ang ibon. Nais nitong magpahinga ngunit nagalit ito nang walang mahanap na madadapuan.",
                                   "Naisipan ng ibon na pagkagalitin ang langit at dagat. ",
                                   "Sa pagkakagalit ng dalawa, ang langit ay naghuho (poured) ng maraming bato at lupa sa dagat na kinalaunan ay naging mga isla.",
                                   "Sa wakas, ang ibon ay nagkaroon na ng madadapuan.",
                                   "Isang araw, ang ibon ay nasa tabing-dagat.",
                                   "Mula sa agos ng dagat, may napadpad na kawayan sa tapat ng ibon.",
                                   "Tinuka niya ito hanggang sa mahati sa dalawa at mabukas.",
                                   "Sa isang bahagi nito lumabas ang isang lalaki at sa isang bahagi naman ay isang babae.",
                                   };

        string[] scriptRarak = { "Ipinagmamalaki ko na mayroon akong mga batuk (tattoo). Patunay lamang na may silbi ako kapag mayroong digmaan. Unang beses kong magkaroon ay sa paa. Kailangan ko pang galingan kung magkakaroon muli ng digmaan. Nang sa gayon ay magkaroon na rin ako sa braso at dibdib." };

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

    //Finished Quest
    bool weedFinish = false;
    bool fishingFinish = false;
    bool taxFinish = false;
    bool taroFinish = false;
    bool stakeFinish = false;

    //Activation of Quest
    bool weedActivated = false;
    bool fishingActivated = false;
    bool taxActivated = false;
    bool taroActivated = false;
    bool stakeActivated = false;


    //QuestCounting
    int weedCount = 0;
    int fishCount = 0;
    int taroCount = 0;

    void DDNRaycastDetection()
    {
        //Debug.Log(Input.GetButton("XButton"));

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
                //ALL NOT ACTIVATED FUNCTION MUST HAVE A && QUESTION = False
                if (!weedActivated)
                {
                    if (hit.collider.gameObject.tag == "Makindo")
                    {
                        Makindo();
                    }
                    else if (hit.collider.gameObject.tag == "Piyan")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Darok")
                    {
                        Darok();
                    }
                    else if (hit.collider.gameObject.tag == "Silanday")
                    {
                        Silanday();
                    }
                    else if (hit.collider.gameObject.tag == "Ibusun")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if(hit.collider.gameObject.tag == "Lupas")
                    {
                        if(weedCount == 0)
                        {
                            //Increase probability
                            ActivateDialogue();
                            dialogueBox.GetComponent<Text>().text = scriptLupas[0];
                        }
                        else if(weedCount == 1)
                        {
                            ActivateDialogue();
                            dialogueBox.GetComponent<Text>().text = scriptLupas[1];
                        }
                    }
                    else if (hit.collider.gameObject.tag == "Enita")
                    {
                        //Increase probability
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptEnita[0];
                    }
                    else if (hit.collider.gameObject.tag == "Datu")
                    {
                        //Increase probability
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptDatu[0];
                    }
                    else if (hit.collider.gameObject.tag == "AmaLupas")
                    {
                        AmaLupas();
                    }
                }

                if (weedActivated && !weedFinish)
                {
                    if (hit.collider.gameObject.tag == "Makindo")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Piyan")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Darok")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Silanday")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Ibusun")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Lupas")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptLupas[3];
                    }
                    else if (hit.collider.gameObject.tag == "Enita")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Datu")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "AmaLupas")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptAmal[1];
                    }
                }

                if (weedFinish)
                {
                    if (hit.collider.gameObject.tag == "Lupas")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptLupas[3];
                        //Go River
                    }
                }

                if (!fishingActivated)
                {
                    if(hit.collider.gameObject.tag == "AmaLupas")
                    {
                        AmaLupas();
                    }
                    else if (hit.collider.gameObject.tag == "Piyan")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Darok")
                    {
                        Darok();
                    }
                    else if (hit.collider.gameObject.tag == "Silanday")
                    {
                        Silanday();
                    }
                    else if (hit.collider.gameObject.tag == "Ibusun")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Lupas")
                    {
                        //Hint
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptLupas[4];
                    }
                    else if (hit.collider.gameObject.tag == "Datu")
                    {
                        //Hint
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptDatu[2];
                    }
                    else if (hit.collider.gameObject.tag == "Makindo")
                    {
                        Makindo();
                    }
                }

                if (fishingActivated && !fishingFinish)
                {
                    if (hit.collider.gameObject.tag == "Makindo")
                    {
                        //if makindo is walking
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptMakindo[3];
                        //if Makindo is in location == river
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptMakindo[4];
                        //ifMakindo is in location == ?
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptMakindo[2];
                    }
                    else if (hit.collider.gameObject.tag == "Piyan")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Darok")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Silanday")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Ibusun")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Lupas")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptLupas[5];
                    }
                    else if (hit.collider.gameObject.tag == "Enita")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Datu")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "AmaLupas")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                }

                if (fishingFinish)
                {
                    if (hit.collider.gameObject.tag == "Makindo")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptMakindo[5];
                        //Move to River
                    }
                }

                if (!taxActivated)
                {
                    if (hit.collider.gameObject.tag == "AmaLupas")
                    {
                        AmaLupas();
                    }
                    else if (hit.collider.gameObject.tag == "Lupas")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptLupas[6];
                    }
                    else if (hit.collider.gameObject.tag == "Darok")
                    {
                        Darok();
                    }
                    else if (hit.collider.gameObject.tag == "Makindo")
                    {
                        Makindo();
                    }
                    else if (hit.collider.gameObject.tag == "Ibusun")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Piyan")
                    {
                        //Hint
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptPiyan[1];
                    }
                    else if (hit.collider.gameObject.tag == "Enita")
                    {
                        //Hint
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptEnita[2];
                    }
                    else if (hit.collider.gameObject.tag == "Datu")
                    {
                        //Hint
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptDatu[3];
                    }
                    else if (hit.collider.gameObject.tag == "Silanday")
                    {
                        Silanday();
                    }
                }

                if(taxActivated && !taxFinish)
                {
                    if (hit.collider.gameObject.tag == "Makindo")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Piyan")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptPiyan[2];
                    }
                    else if (hit.collider.gameObject.tag == "Darok")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Silanday")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptSilanday[2];
                    }
                    else if (hit.collider.gameObject.tag == "Ibusun")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Lupas")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptLupas[6];
                    }
                    else if (hit.collider.gameObject.tag == "Enita")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Datu")
                    {
                        //if chicken delivered
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptDatu[4];
                        //else
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "AmaLupas")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                }

                if (taxFinish)
                {
                    //Move Piyan
                }

                if (!taroActivated)
                {
                    if(hit.collider.gameObject.tag == "AmaLupas")
                    {
                        AmaLupas();
                    }
                    else if (hit.collider.gameObject.tag == "Lupas")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptLupas[6];
                    }
                    else if (hit.collider.gameObject.tag == "Piyan")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Silanday")
                    {
                        Silanday();
                    }
                    else if (hit.collider.gameObject.tag == "Darok")
                    {
                        Darok();
                    }
                    else if (hit.collider.gameObject.tag == "Makindo")
                    {
                        Makindo();
                    }
                    else if (hit.collider.gameObject.tag == "Datu")
                    {
                        //Hint
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptDatu[5];
                    }
                    else if (hit.collider.gameObject.tag == "Ibusun")
                    {
                        //Hint
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptIbusun[1];
                    }
                    else if (hit.collider.gameObject.tag == "Enita")
                    {
                        Enita();
                    }
                }

                if (taroActivated && !taroFinish)
                {
                    if(hit.collider.gameObject.tag == "Enita")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptEnita[4];
                    }
                    else if (hit.collider.gameObject.tag == "Datu")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptDatu[6];
                    }
                    else if (hit.collider.gameObject.tag == "Ibusun")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptIbusun[2];
                    }
                    else
                    {
                        Neutral(hit.collider.gameObject);
                    }
                }

                if (taroFinish)
                {
                    if (hit.collider.gameObject.tag == "Enita")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptEnita[5];
                    }
                    //Datu
                }

                if (!stakeActivated)
                {
                    if(hit.collider.gameObject.tag == "Lupas")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptLupas[6];
                    }
                    else if (hit.collider.gameObject.tag == "Silanday")
                    {
                        Silanday();
                    }
                    else if (hit.collider.gameObject.tag == "Enita")
                    {
                        Enita();
                    }
                    else if (hit.collider.gameObject.tag == "Makindo")
                    {
                        Makindo();
                    }
                    else if (hit.collider.gameObject.tag == "AmaLupas")
                    {
                        //Hint
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptAmal[4];
                    }
                    else if (hit.collider.gameObject.tag == "Darok")
                    {
                        Darok();
                    }
                    else
                    {
                        Neutral(hit.collider.gameObject);
                    }
                }

                if(stakeActivated && !stakeFinish)
                {
                    if (hit.collider.gameObject.tag == "AmaLupas")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptAmal[5];
                    }
                    else
                    {
                        Neutral(hit.collider.gameObject);
                    }
                }

                if (stakeFinish)
                {
                    if (hit.collider.gameObject.tag == "Darok")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptDarok[2];
                    }
                }

                //INFO GATHERING

                if (!weedFinish && fishingFinish && taxFinish)
                {
                    if (hit.collider.gameObject.tag == "Makindo")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = "Nasaan na kaya si Lupas?";
                    }
                    else if(hit.collider.gameObject.tag == "Piyan")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = "Nasaan na kaya si Lupas?";
                    }
                }

                if (weedFinish && !fishingFinish && taxFinish)
                {
                    if (hit.collider.gameObject.tag == "Lupas")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = "Nasaan na kaya si Makindo?";
                    }
                    else if (hit.collider.gameObject.tag == "Piyan")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = "Nasaan na kaya si Makindo?";
                    }
                }

                if (weedFinish && fishingFinish && !taxFinish)
                {
                    if (hit.collider.gameObject.tag == "Lupas")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = "Nasaan na kaya si Piyan?";
                    }
                    else if (hit.collider.gameObject.tag == "Makindo")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = "Nasaan na kaya si Piyan?";
                    }
                }

                if (!weedFinish && !fishingFinish && taxFinish)
                {
                    if (hit.collider.gameObject.tag == "Piyan")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = "Nasaan na kaya sina Lupas at Makindo?";
                    }
                }

                if (!weedFinish && fishingFinish && !taxFinish)
                {
                    if (hit.collider.gameObject.tag == "Makindo")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = "Nasaan na kaya sina Lupas at Piyan?";
                    }
                }

                if (weedFinish && !fishingFinish && !taxFinish)
                {
                    if (hit.collider.gameObject.tag == "Lupas")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = "Nasaan na kaya sina Makindo at Piyan?";
                    }
                }

                if (weedFinish && fishingFinish && taxFinish)
                {
                    if (hit.collider.gameObject.tag == "Lupas")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptLupas[7];
                    }
                    else if (hit.collider.gameObject.tag == "Makindo")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptMakindo[6];
                    }
                    else if (hit.collider.gameObject.tag == "Piyan")
                    {
                        ActivateDialogue();
                        dialogueBox.GetComponent<Text>().text = scriptPiyan[3];
                    }
                }
            }
        }
    }

    void Neutral(GameObject hit)
    {
        if (hit.tag == "Makindo")
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptMakindo[0];
        }
        else if (hit.tag == "Piyan")
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptPiyan[0];
        }
        else if (hit.tag == "Darok")
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptDarok[0];
        }
        else if (hit.tag == "Silanday")
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptSilanday[0];
        }
        else if (hit.tag == "Ibusun")
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptIbusun[0];
        }
        else if (hit.tag == "Lupas")
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptLupas[3];
        }
        else if (hit.tag == "Enita")
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptEnita[1];
        }
        else if (hit.tag == "Datu")
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptDatu[1];
        }
        else if (hit.tag == "AmaLupas")
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptAmal[3];
        }
    }

    void Makindo()
    {
        //Trigger
        if (true)
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptMakindo[1];
        }
        else
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptMakindo[0];
        }
    }

    void Silanday()
    {
        //Trigger
        if (true)
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptSilanday[1];
        }
        else
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptSilanday[0];
        }
    }

    void Darok()
    {
        //Trigger
        if (true)
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptDarok[1];
        }
        else
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptDarok[0];
        }
    }

    void Enita()
    {
        //Trigger
        if (true)
        {
            ActivateDialogue();
            //dialogueBox.GetComponent<Text>().text = scriptEnita[];
        }
        else
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptEnita[1];
        }
    }

    void AmaLupas()
    {
        //Trigger
        if (true)
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptAmal[0];
            weedCount = 1;
        }
        else
        {
            ActivateDialogue();
            dialogueBox.GetComponent<Text>().text = scriptAmal[3];
        }
    }

    void ActivateDialogue()
    {
        dialogueBox.SetActive(true);
        stop = true;
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
