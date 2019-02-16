using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayesServer;
using BayesServer.Inference.RelevanceTree;
using System;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Test : MonoBehaviour
{


	static string[] question = new string[]{ "Ang lipunan ay nahahati sa tatlong antas: datu, timawa, at oripun. Ang mga oripun ay may iba’t ibang uri. Ano ang tawag sa mga naninirahan sa bahay ng kanilang panginoon (master) at nagtatrabaho para sa kanila ng tatlong araw sa kada apat na araw?",
											 "Sa pagtatanim, mahalaga ang prosesong ito upang masiguro na makukuha ng mga pananim ang sustansiya ng lupa.",
											 "Ang karaniwang itinatamin ng mga tao ay mga halamang ugat. Sa mga halamang ugat, ano ang itinuturing nilang pinakamasustansiya?",
											 "Ano ang prosesong ginagawa upang magkaroon ng palatandaan ang kanilang itinanim sa mga damus (a field of root crop)?",
											 "Bukod sa serbisyo, ang buwis (tribute) na natatanggap ng datu mula sa kanyang nasasakupan ay maaaring _______.",
											 "Sino ang mga hindi kabilang sa pagbabayad ng buwis (tribute)?",
											 "Isa sa mga pangunahing ikinabubuhay ng mga tao ay ang pangingisda. Gumamit sila ng iba’t ibang kagamitan tulad ng busog at pana, paggiyod (a type of net), at ____________.",
											 "Ang mga anyong tubig ay sagana sa isda. Ang mga taong naninirahan malapit sa pampang ay kadalasang nangingisda sa karagatan samantalang ang mga nakatira sa kabundukan ay sa _________.",
											 "Ang batuk (tattoo) ay nagpapahiwatig ng katapangan ng mga lalaki at nagpapatunay na sila ay may naitulong sa mga digmaan. Saang bahagi ng katawan inuumpisahan ang paglalagay ng batuk?",
											 "Ang datu ay may kapangyarihang gumawa at magpatupad ng mga batas. Sino naman ang nagpapahayag nito sa buong barangay?",
											 "May mga mito (myth) na pinaniniwalaan ang mga tao na nagsasalaysay ng pinagmulan ng bagay tulad ng araw at buwan. Sa isang mito tungkol sa pinagmulan ng daigdig, anong hayop ang nagdulot ng pagkakabuo ng mga isla?",
											 "Isinalasay din sa mito ng pinagmulan ng daigdig ang pinagmulan ng unang lalaki at babae. Saan lumabas ang unang lalaki at babae?"};
	static string[] OneChoice = new string[] {"a.	tumataban",
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
	static string[] TwoChoice = new string[]{"b.	ayuey",
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
	static string[] ThreeChoice = new string[]{"c.	tumaranpok",
											   "c.	paglalagay ng pestisidyo",
											   "c.	gabi",
											   "c.	kinukulayan ang mga nakapaligid na bakod",
											   "c.	lahat ng nabanggit",
											   "c.	mga pamilya ng sandig sa datu (supporters of datu)",
											   "c.	bankaw (spear)",
											   "c.	ilog",
											   "c.	dibdib",
											   "c.	ang umalohokan",
											   "c.	baboy",
											   "c.	sa niyog"};

	string[] scriptDatu = new string[]{ "Puntahan mo ang kaibigan mong si Lupas. Kanina ay hinahanap ka niya.",
										"Samahan mo muna ang iyong mga kaibigan at ako ay may iniisip.",
										"Puntahan mo ang kaibigan mong si Makindo. Kanina ay hinahanap ka niya.",
										"Puntahan mo ang kaibigan mong si Piyan. Kanina ay hinahanap ka niya.",
										"Galing ito kay Silanday? Siguro ay nagtataka ka kung bakit may nagbibigay ng mga ani sa atin o kaya alagang hayop. Iyon ay tinatawag na buwis. Ibinibigay nila iyon sa atin kapalit ng pamumuno ko sa kanila. Hindi na kabilang sa mga nagbabayad ang mga kapamilya natin.",
										"Pumunta ka sa iyong ina at tulungan mo siya.",
										"Ang imbakan natin ay nasa baba ng bahay.",
                                        "Kung natulungan mo na ang iyong ina, maaari ka nang bumalik sa iyong mga kaibigan"};
	string[] scriptLupas = new string[]{ "Kabani, pupunta ako kay ama. Sumama ka sa akin upang may matutunan ka.",
										 "Alamin natin kay ama kung ano ang maaari nating maitulong.",
										 "Marahil ay inihahanda pa lang natin ang lupa.",
										 "Ngayon ay may natutunan na tayo sa pagtatanim",
										 "Sinabi sa akin ni Makindo ay pupunta siya sa ilog mamaya.",
										 "Baka nasa ilog na si Makindo.",
										 "Nahiligan ko din ang pagtatanim tulad ni ama.",
										 "Hindi ko akalaing magkakaroon ako ng kaibigang datu. Isa lang naman kasi akong tumataban. Hindi tulad mo, nagsisilbi ako sa aking panginoon ng limang araw sa isang buwan."};
	string[] scriptAmal = new string[]{ "Ang pagtatanim ay gawain naming mga oripun. Ngunit maganda ang naisip mong pagtulong kung gusto mong matuto. Sa ngayon, ang gawin mo na lang muna ay magtanggal ng mga ligaw na damo. Kailangan ay matanggal mo lahat.",
										"Kailangan ay walang matitirang damo.",
										"Mahalaga ang naitulong mong pagtatanggal ng mga ligaw na damo sa paghahanda ng lupang tataniman. Kapag hindi tinanggal ang mga iyon, sila ang kukuha ng sustansiya ng pataba na dapat ay sa mga pananim. Huwag mo iyong kakalimutan. Sa ngayon, ako na lang muna ang magtatanim.",
										"Para sa akin, masaya ang pagtatanim kahit matagal ang paghihintay bago mag-ani.",
										"Mukhang nakapagtanim na ang asawa ng aming kapitbahay na si Darok. Ang asawa niya ang  kumikilos sa kanila dahil siya ay lumpo.",
										"Napansin kong wala pang palatandaan ang kanilang pananim."};
	string[] scriptEnita = new string[]{ "Bisitahin mo ang iyong kaibigang si Lupas.",
										 "Kabani, bisitahin mo ang iyong mga kaibigan.",
										 "Bisitahin mo ang iyong kaibigang si Piyan.",
										 "Kabani, anak. Maghahanda na ako ng ating kakainin. Pumunta ka sa baba sa ating imbakan at kumuha ka ng isang gabi.",
										 "Ang gabi ay halamang ugat na bilugan, kayumanggi ang balat, at maputi ang laman.",
										 "Salamat sa pagkuha ng gabi, anak. Sagana man tayo sa mga halamang ugat, ang pinakamasustansiya sa lahat ay ang gabi. Bukod pa roon, maraming gamit ang halamang iyon – ang apay (leaves) ay ginagamit sa pagbalot ng pagkaing iihawin at ang laon (edible leaves) ay maaaring kainin."};
	string[] scriptMakindo = new string[]{ "Napakarami talagang isda sa ilog.",
										   "Kabani! Sumama ka sa akin na mangisda sa ilog. Kumuha ka muna ng sarapang kay Rarak. Pagkakuha mo ay puntahan mo ako sa ilog.",
										   "Kunin mo muna kay Rarak ang sarapang bago tayo pumunta sa ilog.",
										   "Mauna na ako sa ilog.",
										   "Ang kailangan mo lang gawin ay mag-abang tayo ng isdang lalapit. Kapag may lumapit na, saka mo tutusukin ng sarapang. Sapat na kapg nakahuli ka ng limang isda.",
										   "Natututo ka pa lang ngunit nakahuli ka na agad. Hindi ko iyon inaasahan. Ngayon ay marunong ka nang mangisda. Ako na ang bahala sa mga nahuli mong isda at dadalhin ko ito sa iyong ama.",
										   "Mabuti ka pa, anak ka ng ating datu. Lahat ng oras mo ay sa iyo lamang. Ako bilang tumaranpok, may panginoon akong pinagsisilbihan ng apat na araw kada linggo."};
	string[] scriptPiyan = new string[]{ "Magandang umaga, Kabani.",
										 "Ito ang bahay ng aking panginoon na si Silanday. Mukhang hinahanap niya ang iyong ama.",
										 "Manok lang ang inaalagaan naming hayop.",
										 "Alam mo bang ipinanganak kang mapalad, Kabani? May sarili kang tahanan. Kaming mga ayuey, nakatira kami sa bahay ng aming panginoon at sila ang nagbibigay sa amin ng damit at pagkain. Pinagsisilbihan din namin sila ng tatlong araw sa kada apat na araw."};
	string[] scriptIbusun = new string[]{ "Ako ang Umalohokan ng ating barangay.",
										  "Mukhang abala ang iyong ina at kailangan niya ng tulong.",
										  "Ang mabuting anak ay marunong sumunod sa anumang utos ng kanyang magulang.",
										  " Ako si Ibusun, ang Umalohokan ng ating barangay. Salamat sa inyong pakikiisa at kahandaang makinig. May bagong batas na ipatutupad ang ating pinunong Datu Gunsad." +
										  " Simula ngayon, kapag mayroon kayong pinaghihinalaang mangkukulam ay ipaalam agad sa ating pinuno. Kapag napatunayan ay walang ibang hatol kundi kamatayan." +
										  " Siguraduhin niyong tatandaan ang bagong batas na ito at ipakalat sa mga taong wala rito."};
	string[] scriptSilanday = new string[]{ "Ngayon ang libreng araw ni Piyan kaya makakasama mo siya.",
											"Kabani! Nasaan nga pala ang iyong ama? Magbabayad kasi ako ng buwis. Hindi ko maibabayad ang mga inani namin dahil kakaunti lamang ang mga ito. Isang manok na lang ang ibabayad ko at sa iyo ko na lang ito ipapahatid.",
											"Kumuha ka ng isang manok sa aming bakuran at ibigay mo iyon kay Datu Gunsad. Pakisabi na iyon ay kabayaran sa aking buwis. Salamat!"};
	string[] scriptBiraman = new string[]{ "Kabani, nais kong magsalaysay ng isang mito. Tungkol ito sa pinagmulan ng daigdig at ang unang lalaki at babae.",
										   "Noong unang panahon ay wala kundi langit, dagat, at isang ibong lipad ng lipad sa pagitan.",
										   "Sa katagalan ng paglipad ay napagod ang ibon. Nais nitong magpahinga ngunit nagalit ito nang walang mahanap na madadapuan.",
										   "Naisipan ng ibon na pagkagalitin ang langit at dagat. ",
										   "Sa pagkakagalit ng dalawa, ang langit ay naghuho (poured) ng maraming bato at lupa sa dagat na kinalaunan ay naging mga isla.",
										   "Sa wakas, ang ibon ay nagkaroon na ng madadapuan.",
										   "Isang araw, ang ibon ay nasa tabing-dagat.",
										   "Mula sa agos ng dagat, may napadpad na kawayan sa tapat ng ibon.",
										   "Tinuka niya ito hanggang sa mahati sa dalawa at mabukas.",
										   "Sa isang bahagi nito lumabas ang isang lalaki at sa isang bahagi naman ay isang babae."};
	string[] scriptRarak = new string[] { "Ipinagmamalaki ko na mayroon akong mga batuk (tattoo). Patunay lamang na may silbi ako kapag mayroong digmaan. Unang beses kong magkaroon ay sa paa. Kailangan ko pang galingan kung magkakaroon muli ng digmaan." };
	string[] scriptDarok = new string[]{ "Maganda ang panahon ngayon, hindi ba Kabani?",
										 "Nakapagtanim na ang asawa ko ng mga halamang ugat. Ngunit umalis siya agad at may pinuntahan. Nakalimutan niyang maglagay ng palatandaan. Hindi ko ito magagawa dahil ako ay lumpo. Maaari ba akong tulungan?",
										 "Salamat iyong tulong. Ngayon ay may palatandaan na kami na amin ang mga pananim na iyon.",
                                         "Sa loob ng bahay namin ay may kahoy. Iyon ang gamitin mo at itusok mo iyon sa lupa."};
	string[] scriptCrowd = new string[] { "Mukhang may bagong batas na nabuo si Datu Gunsad.Marahil ay ipapahayag na ito ni Ibusun.Siya ang ating Umalohokan.Anumang batas ang mabuo ng datu, hindi ito agad ipinatutupad hangga’t hindi ito naipapahayag ng Umalohokan." };

	//3D Text
	//public GameObject Lupas;
	//public GameObject nameDatu;
	//public GameObject nameEnita;

	public GameObject d_Datu;


	public int buttonclick = 0;

	public bool postflag;

    int randomQuestion;

	public AudioSource correct;
	public AudioSource wrong;
    
    public AudioSource umalSound;
    public AudioSource rarakSound;
    public AudioSource darokSound;
    public AudioSource makindoSound;
    public AudioSource biraSound;
    public AudioSource lupasSound;
    public AudioSource piyanSound;
    public AudioSource datuSound;
    public AudioSource silandaySound;
    public AudioSource enitaSound;
    public AudioSource tirugoSound;
    public AudioSource crowdSound;

    public GameObject canvas;

	public GameObject dialogueBox;
	public GameObject dialogueText;
	public GameObject nameText;
	public GameObject questPanel;
	public GameObject biramanStory;

	public static bool activeSpear;
	public static bool activeStake;

	public GameObject gabi;
	public GameObject chicken;

    public GameObject npcCrowd;

	public GameObject Fish1;
	public GameObject Fish2;
	public GameObject Fish3;
	public GameObject Fish4;
	public GameObject Fish5;

	public GameObject maki;
	public GameObject piya;
	public GameObject lupa;

	public Vector3 makiPosition;
	public Vector3 makiPosition2;
	public Vector3 piyanPosition;
	public Vector3 lupasPosition;

	public GameObject page1;
	public GameObject page2;
	public GameObject page3;
	public GameObject page4;
	public GameObject page5;
	public GameObject page6;
	public GameObject page7;
	public GameObject page8;
	public GameObject page9;

	double[] probabilityTaro = new double[2];
	double[] probabilityFish = new double[2];
	double[] probabilityTax = new double[2];
	double[] probabilityStake = new double[2];
	double[] probabilityWeed = new double[] { 0.9, 0.1 };

	bool chickenDel = false;

	public static float probWeed = 0.3f;
	static float probTax;
	static float probStake;
	static float probTaro;
	static float probFish;

	public static bool[] q = new bool[12];

	bool dlBoxEnabler = true;

	bool finishWeed = false;

	public Transform player;

	BayesServer.Network network;
	Variable weedQuest, fishQuest, taroQuest, stakeQuest, taxQuest;
	Variable weedQuest2, fishQuest2, taroQuest2, stakeQuest2, taxQuest2;
	State WTrue, WFalse;
	State WTrue2, WFalse2;
	State FTrue, FFalse;
	State FTrue2, FFalse2;
	State TTrue, TFalse;
	State TTrue2, TFalse2;
	State STrue, SFalse;
	State STrue2, SFalse2;
	State XTrue, XFalse;
	State XTrue2, XFalse2;
	Node weedNode;
	Node fishNode;
	Node taroNode;
	Node stakeNode;
	Node taxNode;
	Node weedNode2;
	Node fishNode2;
	Node taroNode2;
	Node stakeNode2;
	Node taxNode2;
	StateContext fTrueTime0;
	StateContext fFalseTime0;

    AudioSource[] umalSources;
    AudioSource[] rarakSources;
    AudioSource[] darokSources;
    AudioSource[] makindoSources;
    AudioSource[] biraSources;
    AudioSource[] lupasSources;
    AudioSource[] piyanSources;
    AudioSource[] datuSources;
    AudioSource[] silandaySources;
    AudioSource[] enitaSources;
    AudioSource[] tirugoSources;
    AudioSource[] crowdSources;

    bool stop = false;
	bool storyActive = false;

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

	int weedHolder;


	int x = 0;
	public static int scoreCount = 0;
	public static int postCount = 0;

	public static bool postTest = false;

	int caughtCount = 0;

	Rigidbody P_Rigidbody;
	public LoadScreen loadscreen;
	//for quest indicator
	public GameObject QIndicator;
	public Animator QIndicatorAnim;
	public Text QIndText;

	public static bool[] rand = new bool[12];

	bool moveLupas = false;
	bool movePiyan = false;
	bool moveMakindo = false;

    bool[] randQuest = new bool[12];

	public static bool questActive = false;
	

	void Text()
	{
		//Changing the texts of questions and choices every time the player answers
		if (x < 12)
		{
			//Debug.Log("I am the Test");

			//Debug.Log(OneChoice[x]); 

			Text txt = gameObject.GetComponent<Text>();
			//if (postflag)
			//{
				if (buttonclick == 0)
				{
                    //randomQuestion = Random.Range(0, question.Length);
                    do
                    {
                        //Debug.Log("Pumapasok dito!");
                        randomQuestion = Random.Range(0, question.Length);
                    } while (randQuest[randomQuestion]);

                    txt.text = Convert.ToString(question[randomQuestion]);
                    GameObject.Find("btn1").GetComponentInChildren<Text>().text = OneChoice[randomQuestion];
                    GameObject.Find("btn2").GetComponentInChildren<Text>().text = TwoChoice[randomQuestion];
                    GameObject.Find("btn3").GetComponentInChildren<Text>().text = ThreeChoice[randomQuestion];

                    randQuest[randomQuestion] = true;
                    
                    //if (rand[randomQuestion] == true)

                    //Debug.Log(randomQuestion + " " + randQuest[randomQuestion]);
                    //{
                    buttonclick = 1;
					//}
					//rand[randomQuestion] = false;
				}
			//}
			//else
			//{
			//	//txt.text = Convert.ToString(question[x]);
			//}
		}
		else if (x >= 12 && !postflag)
		{
			Debug.Log("Loadlevel");
			//levelLoader.FadeToLevel();
			//SceneManager.LoadScene(2);
			loadscreen.LoadLevel();
		}
		else if (x == 12)
		{
			//canvas.SetActive(true);

            GameObject.Find("PreScore").GetComponentInChildren<Text>().text = "Pre-Test " + scoreCount + "/ 12";
            GameObject.Find("PostScore").GetComponentInChildren<Text>().text = "Post-Test " + postCount + "/ 12";
            //GameObject.Find("txt1").GetComponentInChildren<Text>().text = Convert.ToString(scoreCount);
			//GameObject.Find("txt2").GetComponentInChildren<Text>().text = Convert.ToString(postCount);

			Debug.Log(scoreCount);
			Debug.Log(postCount);
		}
	}


	// Use this for initialization
	void Start()
	{

		//loadscreen.GetComponent<LoadScreen>();

		//QIndicator.SetActive(true);
		//QIndText.text = "BAGONG MISYON!";
		//QIndicatorAnim.SetBool("isPlaying", true);
		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;

		if (sceneName == "Test")
		{
            for(int z=0; z<12; z++)
            {
                randomQuestion = Random.Range(0, 12);
                randQuest[z] = false;
            }
			x = 0;
			Debug.Log("Hello this is a test");
		}

		Debug.Log(sceneName);
		if (sceneName == "Barangay")
		{
            Test.q[1] = true;
            Test.q[0] = true;

            umalSources = umalSound.GetComponents<AudioSource>();
            rarakSources = rarakSound.GetComponents<AudioSource>();
            darokSources = darokSound.GetComponents<AudioSource>();
            makindoSources = makindoSound.GetComponents<AudioSource>();
            biraSources = biraSound.GetComponents<AudioSource>();
            lupasSources = lupasSound.GetComponents<AudioSource>();
            piyanSources = piyanSound.GetComponents<AudioSource>();
            datuSources = datuSound.GetComponents<AudioSource>();
            silandaySources = silandaySound.GetComponents<AudioSource>();
            enitaSources = enitaSound.GetComponents<AudioSource>();
            tirugoSources = tirugoSound.GetComponents<AudioSource>();
            crowdSources = crowdSound.GetComponents<AudioSource>();

            gabi.SetActive(false);

			Prob();

			P_Rigidbody = this.GetComponent<Rigidbody>();
		}
	}

	// Update is called once per frame
	void Update()
	{
		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;

		if (sceneName == "Test")
		{
            Text();
		}
		else if (sceneName == "Barangay")
		{
			//Debug.Log(GameObject.FindGameObjectsWithTag("Grass").Length);
			DynamicDecisionNetwork();
			QueryNetwork();
			DDNRaycastDetection();
		}

		//Test.q[1] = true;
		//Debug.Log(scoreCount);
		//NpcName();
	}

	void Prob()
	{
		if (q[1])
		{
			probWeed = 0.4f;
		}
		else
		{
			probWeed = 0.3f;
		}

		if (q[2])
		{
			probTaro = 0.4f;
		}
		else
		{
			probTaro = 0.3f;
		}
		if (q[3])
		{
			probStake = 0.4f;
		}
		else
		{
			probStake = 0.3f;
		}
		if (q[4] || q[5])
		{
			probTax = 0.4f;
		}
		else
		{
			probTax = 0.3f;
		}
		if (q[6] || q[7])
		{
			probFish = 0.4f;
		}
		else
		{
			probFish = 0.3f;
		}
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

		WTrue2 = new State("WWTrue");
		WFalse2 = new State("WWFalse");
		weedQuest2 = new Variable("WeedW", WTrue2, WFalse2);
		weedNode2 = new Node(weedQuest2)
		{
			TemporalType = TemporalType.Temporal // this is a time series node, hence re-used for each time slice
		};
		network.Nodes.Add(weedNode2);

		//Fishing Quest
		FTrue = new State("FTrue");
		FFalse = new State("FFalse");
		fishQuest = new Variable("Fish", FTrue, FFalse);
		fishNode = new Node(fishQuest)
		{
			TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
		};
		network.Nodes.Add(fishNode);

		FTrue2 = new State("FFTrue");
		FFalse2 = new State("FFFalse");
		fishQuest2 = new Variable("FishF", FTrue2, FFalse2);
		fishNode2 = new Node(fishQuest2)
		{
			TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
		};
		network.Nodes.Add(fishNode2);

		//Taro Quest
		TTrue = new State("TTrue");
		TFalse = new State("TFalse");
		taroQuest = new Variable("Taro", TTrue, TFalse);
		taroNode = new Node(taroQuest)
		{
			TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
		};
		network.Nodes.Add(taroNode);

		TTrue2 = new State("TTTrue");
		TFalse2 = new State("TTFalse");
		taroQuest2 = new Variable("TaroT", TTrue2, TFalse2);
		taroNode2 = new Node(taroQuest2)
		{
			TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
		};
		network.Nodes.Add(taroNode2);

		//Stake Quest
		STrue = new State("STrue");
		SFalse = new State("SFalse");
		stakeQuest = new Variable("Stake", STrue, SFalse);
		stakeNode = new Node(stakeQuest)
		{
			TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
		};
		network.Nodes.Add(stakeNode);

		STrue2 = new State("SSTrue");
		SFalse2 = new State("SSFalse");
		stakeQuest2 = new Variable("StakeS", STrue2, SFalse2);
		stakeNode2 = new Node(stakeQuest2)
		{
			TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
		};
		network.Nodes.Add(stakeNode2);

		//Tax Quest
		XTrue = new State("XTrue");
		XFalse = new State("XFalse");
		taxQuest = new Variable("Tax", XTrue, XFalse);
		taxNode = new Node(taxQuest)
		{
			TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
		};
		network.Nodes.Add(taxNode);

		XTrue2 = new State("XXTrue");
		XFalse2 = new State("XXFalse");
		taxQuest2 = new Variable("TaxX", XTrue2, XFalse2);
		taxNode2 = new Node(taxQuest2)
		{
			TemporalType = TemporalType.Temporal  // this is a time series node, hence re-used for each time slice
		};
		network.Nodes.Add(taxNode2);

		// add a link from node to node
		network.Links.Add(new Link(weedNode, weedNode2, 0));

		network.Links.Add(new Link(fishNode, fishNode2, 0));

		network.Links.Add(new Link(taroNode, taroNode2, 0));

		network.Links.Add(new Link(stakeNode, stakeNode2, 0));

		network.Links.Add(new Link(taxNode, taxNode2, 0));

		//network.Links.Add(new Link(religionNode, religionNode, 1));
	}

	//Query Upon receiving data
	void QueryNetwork()
	{
		//Debug.Log("AJhdajsd");

		//Weed Quest 
		Table priorKnowledgeWeed = weedNode.NewDistribution(0).Table;

		StateContext wTrueTime0 = new StateContext(WTrue, 0);
		StateContext wFalseTime0 = new StateContext(WFalse, 0);


		priorKnowledgeWeed[wTrueTime0] = 0.3;
		priorKnowledgeWeed[wFalseTime0] = 0.7;
		// NewDistribution does not assign the new distribution, so it still must be assigned
		weedNode.Distribution = priorKnowledgeWeed;

		Table choiceStatusWeed = weedNode2.NewDistribution().Table;
		StateContext wTrue = new StateContext(WTrue2, 0);
		StateContext wFalse = new StateContext(WFalse2, 0);
		choiceStatusWeed[wTrue, wTrueTime0] = 0.9;
		choiceStatusWeed[wFalse, wTrueTime0] = 0.1;
		choiceStatusWeed[wTrue, wFalseTime0] = 0.1;
		choiceStatusWeed[wFalse, wFalseTime0] = 0.9;

		weedNode2.Distribution = choiceStatusWeed;

		//Fish Quest 
		Table priorKnowledgeFish = fishNode.NewDistribution(0).Table;

		fTrueTime0 = new StateContext(FTrue, 0);
		fFalseTime0 = new StateContext(FFalse, 0);


		priorKnowledgeFish[fTrueTime0] = 0.4;
		priorKnowledgeFish[fFalseTime0] = 0.6;
		// NewDistribution does not assign the new distribution, so it still must be assigned
		fishNode.Distribution = priorKnowledgeFish;

		Table choiceStatusFish = fishNode2.NewDistribution().Table;
		StateContext fTrue = new StateContext(FTrue2, 0);
		StateContext fFalse = new StateContext(FFalse2, 0);
		choiceStatusFish[fTrue, fTrueTime0] = 0.9;
		choiceStatusFish[fFalse, fTrueTime0] = 0.1;
		choiceStatusFish[fTrue, fFalseTime0] = 0.1;
		choiceStatusFish[fFalse, fFalseTime0] = 0.9;

		fishNode2.Distribution = choiceStatusFish;

		//Taro Quest

		StateContext tTrueTime0 = new StateContext(TTrue, 0);
		StateContext tFalseTime0 = new StateContext(TFalse, 0);

		Table priorKnowledgeTaro = taroNode.NewDistribution(0).Table;
		priorKnowledgeTaro[tTrueTime0] = 0.3;
		priorKnowledgeTaro[tFalseTime0] = 0.7;
		// NewDistribution does not assign the new distribution, so it still must be assigned
		taroNode.Distribution = priorKnowledgeTaro;

		Table choiceStatusTaro = taroNode2.NewDistribution().Table;
		StateContext tTrue = new StateContext(TTrue2, 0);
		StateContext tFalse = new StateContext(TFalse2, 0);
		choiceStatusTaro[tTrue, tTrueTime0] = 0.9;
		choiceStatusTaro[tFalse, tTrueTime0] = 0.1;
		choiceStatusTaro[tTrue, tFalseTime0] = 0.1;
		choiceStatusTaro[tFalse, tFalseTime0] = 0.9;

		taroNode2.Distribution = choiceStatusTaro;

		//Tax Quest

		StateContext xTrueTime0 = new StateContext(XTrue, 0);
		StateContext xFalseTime0 = new StateContext(XFalse, 0);

		Table priorKnowledgeTax = taxNode.NewDistribution(0).Table;
		priorKnowledgeTax[xTrueTime0] = 0.3;
		priorKnowledgeTax[xFalseTime0] = 0.7;
		// NewDistribution does not assign the new distribution, so it still must be assigned
		taxNode.Distribution = priorKnowledgeTax;

		Table choiceStatusTax = taxNode2.NewDistribution().Table;
		StateContext xTrue = new StateContext(XTrue2, 0);
		StateContext xFalse = new StateContext(XFalse2, 0);
		choiceStatusTax[xTrue, xTrueTime0] = 0.9;
		choiceStatusTax[xFalse, xTrueTime0] = 0.1;
		choiceStatusTax[xTrue, xFalseTime0] = 0.1;
		choiceStatusTax[xFalse, xFalseTime0] = 0.9;

		taxNode2.Distribution = choiceStatusTax;

		//Stake Quest

		StateContext sTrueTime0 = new StateContext(STrue, 0);
		StateContext sFalseTime0 = new StateContext(SFalse, 0);

		Table priorKnowledgeStake = stakeNode.NewDistribution(0).Table;
		priorKnowledgeStake[sTrueTime0] = 0.3;
		priorKnowledgeStake[sFalseTime0] = 0.7;
		// NewDistribution does not assign the new distribution, so it still must be assigned
		stakeNode.Distribution = priorKnowledgeStake;

		Table choiceStatusStake = stakeNode2.NewDistribution().Table;
		StateContext sTrue = new StateContext(STrue2, 0);
		StateContext sFalse = new StateContext(SFalse2, 0);
		choiceStatusStake[sTrue, sTrueTime0] = 0.9;
		choiceStatusStake[sFalse, sTrueTime0] = 0.1;
		choiceStatusStake[sTrue, sFalseTime0] = 0.1;
		choiceStatusStake[sFalse, sFalseTime0] = 0.9;

		stakeNode2.Distribution = choiceStatusStake;

		// optional check to validate network
		//beliefNet.Validate(new ValidationOptions());
		// at this point the network has been fully specified

		// we will now perform some queries on the network
		RelevanceTreeInference inference = new RelevanceTreeInference(network);
		RelevanceTreeQueryOptions queryOptions = new RelevanceTreeQueryOptions();
		RelevanceTreeQueryOutput queryOutput = new RelevanceTreeQueryOutput();

		inference.Evidence.SetState(WTrue, 0);
		//inference.Evidence.SetState(TTrue, 0);

		queryOptions.LogLikelihood = true; // only ask for this if you really need it
		var queryA = new Table(weedNode.NewDistribution(0).Table);
		inference.QueryDistributions.Add(queryA);
		inference.Query(queryOptions, queryOutput); // note that this can raise an exception (see help for details)

		//Debug.Log("LogLikelihood: " + queryOutput.LogLikelihood.Value);

		float probability = (float)queryA[wTrueTime0];

		probability = Mathf.Round(probability * 100f) / 100f;

		//Debug.Log("Inserting");
		//Debug.Log(probability);
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
	public static bool stakeFinish = false;

	//Activation of Quest
	bool weedActivated = false;
	public static bool fishingActivated = false;

	bool fishActivated = false;
	bool taxActivated = false;
	bool taroActivated = false;
	public static bool stakeActivated = false;

	bool weedTrigger1 = false, weedTrigger2 = false, weedTrigger3 = false;
	bool fishTrigger1 = false, fishTrigger2 = false;
	bool taxTrigger1 = false, taxTrigger2 = false, taxTrigger3 = false;
	bool taroTrigger1 = false, taroTrigger2 = false;
	bool stakeTrigger1 = false;

	bool hintWeed = false;
	bool hintFish = false;
	bool hintTax = false;
	bool hintStake = false;
	bool hintTaro = false;

	//QuestCounting
	int weedCount = 0;
	int fishCount = 0;
	int taroCount = 0;
	int stakeCount = 0;
	int taxCount = 0;

	int currentPage = 0;

	bool indicatorWeed = true;
	bool indicatorFish = true;
	bool indicatorTax = true;
	bool indicatorTaro = true;
	bool indicatorStake = true;


	bool inHand = false;

    bool biraIntro = true;

    bool movedL = false;
    bool movedP = false;
    bool movedM = false;

    void DDNRaycastDetection()
	{
		//Debug.Log(Input.GetButton("XButton"));
		//Debug.Log(q[1]);
		//Debug.Log("Pumapasok dito!");


		if (stop)
		{
			Time.timeScale = 0;
		}

		if (Input.GetButton("XButton"))
		{
			Time.timeScale = 1;
			stop = false;
			dialogueBox.SetActive(false);
			biramanStory.SetActive(false);
			storyActive = false;
			currentPage = 0;
            biraIntro = true;
		}



		if (taxFinish)
		{
			piya.transform.position = piyanPosition;
		}


		RaycastHit hit;
		var fwd = transform.TransformDirection(Vector3.forward);
		if (Physics.Raycast(transform.position, fwd, out hit, 100))
		{


			//Debug.Log(weedTrigger1);
			//Debug.Log(weedTrigger2);
			//Debug.Log(weedTrigger3);

			if (Input.GetButton("BButton"))
			{
				//ALL NOT ACTIVATED FUNCTION MUST HAVE A && QUESTION = False
				if (!weedActivated && q[1])
				{
					if (hit.collider.gameObject.tag == "Makindo")
					{
						Makindo(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Piyan")
					{
						Neutral(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Darok")
					{
						Darok(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Silanday")
					{
						Silanday(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Ibusun")
					{
						Neutral(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Lupas")
					{
						if (weedCount == 0)
						{
							if (q[1] && !weedTrigger1 && !weedTrigger2 && !weedTrigger3)
							{
								Debug.Log("Hint Trigger");
								probWeed = 0.6f;
								hintWeed = true;
								StartCoroutine(Hint());
							}
							else if (weedTrigger1 && !weedTrigger2)
							{
								probWeed = 0.7f;
								StartCoroutine(Hint());
							}
							else if (weedTrigger2 && !weedTrigger1)
							{
								probWeed = 0.8f;
								StartCoroutine(Hint());
							}
							LookatPlayer(hit.collider.gameObject);
							ActivateDialogue();
							nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
							dialogueText.GetComponentInChildren<Text>().text = scriptLupas[0];

                            var voice = lupasSources[5];
                            voice.Play();
                        }
						else if (weedCount == 1)
						{
							LookatPlayer(hit.collider.gameObject);
							ActivateDialogue();
							nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
							dialogueText.GetComponentInChildren<Text>().text = scriptLupas[1];

                            var voice = lupasSources[6];
                            voice.Play();
                        }
					}
					else if (hit.collider.gameObject.tag == "Enita")
					{
						if (q[1] && !weedTrigger1 && !weedTrigger2 && !weedTrigger3)
						{
							Debug.Log("Hint Trigger");
							probWeed = 0.6f;
							hintWeed = true;
							StartCoroutine(Hint());
						}
						else if (weedTrigger1 && !weedTrigger2)
						{
							probWeed = 0.7f;
							StartCoroutine(Hint());
						}
						else if (weedTrigger2 && !weedTrigger1)
						{
							probWeed = 0.8f;
							StartCoroutine(Hint());
						}
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptEnita[0];

                        var voice = enitaSources[0];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Datu")
					{
						if (q[1] && !weedTrigger1 && !weedTrigger2 && !weedTrigger3)
						{
							Debug.Log("Hint Trigger");
							probWeed = 0.6f;
							hintWeed = true;
							StartCoroutine(Hint());
						}
						else if (weedTrigger1 && !weedTrigger2)
						{
							probWeed = 0.7f;
							StartCoroutine(Hint());
						}
						else if (weedTrigger2 && !weedTrigger1)
						{
							probWeed = 0.8f;
							StartCoroutine(Hint());
						}
						LookatPlayer(hit.collider.gameObject);
						//d_Datu.SetActive(false);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptDatu[0];

                        Debug.Log("Dis is it, malgkit");

                        //var aSources =GetComponents(AudioSource); audio1 = aSources[0]; audio2 = aSources[1];

                        var voice = datuSources[0];
                        voice.Play();

                        //d1.Play();
                        //d_Datu[0].SetActive(true);
                        //SetInactiveDialogue();
                    }
					else if (hit.collider.gameObject.tag == "AmaLupas")
					{
						AmaLupas(hit.collider.gameObject);
					}
				}

				else if (weedActivated && !weedFinish)
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
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptLupas[2];

                        var voice = lupasSources[7];
                        voice.Play();
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
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptAmal[1];

                        var voice = tirugoSources[1];
                        voice.Play();
                    }
				}

				else if (weedFinish && !movedL)
				{
					if (hit.collider.gameObject.tag == "Lupas")
					{
						Debug.Log("Lupas is here");
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptLupas[3];
						Test.q[1] = false;
						moveLupas = true;
						StartCoroutine(Hold());

                        var voice = lupasSources[8];
                        voice.Play();
                    }
				}

				if (!fishingActivated && (q[6] || q[7]) && !q[1])
				{
					Debug.Log("Enter this");
					if (hit.collider.gameObject.tag == "AmaLupas")
					{
						AmaLupas(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Piyan")
					{
						Neutral(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Darok")
					{
						Darok(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Silanday")
					{
						Silanday(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Ibusun")
					{
						Neutral(hit.collider.gameObject);
					}
                    else if (hit.collider.gameObject.tag == "Enita")
                    {
                        Neutral(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "Lupas")
					{
						if ((q[6] || q[7]) && !fishTrigger1 && !fishTrigger2)
						{
							probFish = 0.6f;
							hintFish = true;
							StartCoroutine(Hint());
						}
						else if (fishTrigger1 && !fishTrigger2)
						{
							probFish = 0.8f;
							StartCoroutine(Hint());
						}
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptLupas[4];

                        var voice = lupasSources[9];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Datu")
					{
						if ((q[6] || q[7]) && !fishTrigger1 && !fishTrigger2)
						{
							probFish = 0.6f;
							hintFish = true;
							StartCoroutine(Hint());
						}
						else if (fishTrigger1 && !fishTrigger2)
						{
							probFish = 0.8f;
							StartCoroutine(Hint());
						}
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptDatu[2];

                        var voice = datuSources[1];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Makindo")
					{
						Makindo(hit.collider.gameObject);
					}
				}

				else if (fishingActivated && !fishingFinish)
				{
					Debug.Log("this is the answer");

					if (hit.collider.gameObject.tag == "Makindo")
					{
						if (maki.transform.position == makiPosition)
						{
							LookatPlayer(hit.collider.gameObject);
							ActivateDialogue();
							nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
							dialogueText.GetComponentInChildren<Text>().text = scriptMakindo[4];

                            var voice = makindoSources[9];
                            voice.Play();
                        }
						else if (fishCount == 1)
						{
							LookatPlayer(hit.collider.gameObject);
							ActivateDialogue();
							nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
							dialogueText.GetComponentInChildren<Text>().text = scriptMakindo[2];
							fishActivated = true;
							StartCoroutine(Hold());

                            var voice = makindoSources[8];
                            voice.Play();
                        }
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
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptLupas[5];
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

				else if (fishingFinish && !movedM)
				{
					if (hit.collider.gameObject.tag == "Makindo")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptMakindo[5];
						moveMakindo = true;
						StartCoroutine(Hold());

                        var voice = makindoSources[10];
                        voice.Play();
                    }
				}

				if (!taxActivated && (q[4] || q[5]) && !q[1] && !q[6] && !q[7] && !q[2] && !q[3])
				{
					if (hit.collider.gameObject.tag == "AmaLupas")
					{
						AmaLupas(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Lupas")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptLupas[6];

                        var voice = lupasSources[10];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Darok")
					{
						Darok(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Makindo")
					{
						Makindo(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Ibusun")
					{
						Neutral(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Piyan")
					{
						if ((q[4] || q[5]) && !taxTrigger1 && !taxTrigger2 && !taxTrigger3)
						{
							probTax = 0.6f;
							hintTax = true;
							StartCoroutine(Hint());
						}
						else if (taxTrigger1 && !taxTrigger2 && !taxTrigger3)
						{
							probTax = 0.7f;
							StartCoroutine(Hint());
						}
						else if (!taxTrigger2 && taxTrigger2 && !taxTrigger3)
						{
							probTax = 0.9f;
							StartCoroutine(Hint());
						}
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptPiyan[1];

                        var voice = piyanSources[6];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Enita")
					{
						if ((q[4] || q[5]) && !taxTrigger1 && !taxTrigger2 && !taxTrigger3)
						{
							probTax = 0.6f;
							hintTax = true;
							StartCoroutine(Hint());
						}
						else if (taxTrigger1 && !taxTrigger2 && !taxTrigger3)
						{
							probTax = 0.7f;
							StartCoroutine(Hint());
						}
						else if (!taxTrigger2 && taxTrigger2 && !taxTrigger3)
						{
							probTax = 0.9f;
							StartCoroutine(Hint());
						}
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptEnita[2];

                        var voice = enitaSources[2];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Datu")
					{
						if ((q[4] || q[5]) && !taxTrigger1 && !taxTrigger2 && !taxTrigger3)
						{
							probTax = 0.6f;
							hintTax = true;
							StartCoroutine(Hint());
						}
						else if (taxTrigger1 && !taxTrigger2 && !taxTrigger3)
						{
							probTax = 0.7f;
							StartCoroutine(Hint());
						}
						else if (!taxTrigger2 && taxTrigger2 && !taxTrigger3)
						{
							probTax = 0.9f;
							StartCoroutine(Hint());
						}
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptDatu[3];

                        var voice = datuSources[2];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Silanday")
					{
						Silanday(hit.collider.gameObject);
					}
				}

				else if (taxActivated && !taxFinish)
				{
					if (hit.collider.gameObject.tag == "Makindo")
					{
						Neutral(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Piyan")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptPiyan[2];

                        var voice = piyanSources[7];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Darok")
					{
						Neutral(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Silanday")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptSilanday[2];
					}
					else if (hit.collider.gameObject.tag == "Ibusun")
					{
						Neutral(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Lupas")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptLupas[6];

                        var voice = lupasSources[10];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Enita")
					{
						Neutral(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Datu")
					{
						if (inHand)
						{
							chicken.SetActive(false);
							inHand = false;
							taxFinish = true;
							Test.q[4] = false;
							Test.q[5] = false;
							questActive = false;

							LookatPlayer(hit.collider.gameObject);
							ActivateDialogue();
							nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
							dialogueText.GetComponentInChildren<Text>().text = scriptDatu[4];

                            var voice = datuSources[4];
                            voice.Play();
                        }
						else
						{
							Neutral(hit.collider.gameObject);
						}
					}
					else if (hit.collider.gameObject.tag == "AmaLupas")
					{
						Neutral(hit.collider.gameObject);
					}
				}

				if (!taroActivated && q[2] && !q[1] && !q[6] && !q[7])
				{
					if (hit.collider.gameObject.tag == "AmaLupas")
					{
						AmaLupas(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Lupas")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptLupas[6];

                        var voice = lupasSources[10];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Piyan")
					{
						Neutral(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Silanday")
					{
						Silanday(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Darok")
					{
						Darok(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Makindo")
					{
						Makindo(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Datu")
					{
						if (q[2] && !taroTrigger1 && !taroTrigger2)
						{
							probTaro = 0.6f;
							hintTaro = true;
							StartCoroutine(Hint());
						}
						else if (taroTrigger1 && !taroTrigger2)
						{
							probTaro = 0.9f;
							StartCoroutine(Hint());
						}
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptDatu[5];

                        var voice = datuSources[6];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Ibusun")
					{
						if (q[2] && !taroTrigger1 && !taroTrigger2)
						{
							probTaro = 0.6f;
							hintTaro = true;
							StartCoroutine(Hint());
						}
						else if (taroTrigger1 && !taroTrigger2)
						{
							probTaro = 0.9f;
							StartCoroutine(Hint());
						}
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptIbusun[1];

                        var voice = umalSources[1];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Enita")
					{
						Enita(hit.collider.gameObject);
					}
				}

				else if (taroActivated && !taroFinish)
				{
					if (hit.collider.gameObject.tag == "Enita")
					{
						if (inHand)
						{
							gabi.SetActive(false);
							inHand = false;
							questActive = false;
							taroFinish = true;
							Test.q[2] = false;
						}
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptEnita[4];

                        var voice = enitaSources[4];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Datu")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptDatu[6];

                        var voice = datuSources[7];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Ibusun")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptIbusun[2];

                        var voice = umalSources[2];
                        voice.Play();
                    }
					else
					{
						Neutral(hit.collider.gameObject);
					}
				}

				else if (taroFinish)
				{
					if (hit.collider.gameObject.tag == "Enita")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptEnita[5];

                        var voice = enitaSources[5];
                        voice.Play();
                    }
				}

				// && !q[1] && !q[6] && !q[7]

				if (!stakeActivated && q[3])
				{
					if (hit.collider.gameObject.tag == "Lupas")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptLupas[6];

                        var voice = lupasSources[10];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Silanday")
					{
						Silanday(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Enita")
					{
						Enita(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "Makindo")
					{
						Makindo(hit.collider.gameObject);
					}
					else if (hit.collider.gameObject.tag == "AmaLupas")
					{
						if (q[3] && !stakeTrigger1)
						{
							probStake = 0.8f;
							hintStake = true;
							StartCoroutine(Hint());
						}
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptAmal[4];

                        var voice = tirugoSources[4];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Darok")
					{
						Darok(hit.collider.gameObject);
					}
					else
					{
						Neutral(hit.collider.gameObject);
					}
				}

				else if (stakeActivated && !stakeFinish)
				{
					if (hit.collider.gameObject.tag == "AmaLupas")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptAmal[5];

                        var voice = tirugoSources[5];
                        voice.Play();
                    }
					else
					{
						Neutral(hit.collider.gameObject);
					}
				}

				else if (stakeFinish)
				{
					if (hit.collider.gameObject.tag == "Darok")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptDarok[2];

                        var voice = darokSources[3];
                        voice.Play();
                    }
				}

				////INFO GATHERING
				// Adjust

				if (!weedFinish && fishingFinish && taxFinish)
				{
					if (hit.collider.gameObject.tag == "Makindo")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = "Nasaan na kaya si Lupas?";

                        var voice = makindoSources[0];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Piyan")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = "Nasaan na kaya si Lupas?";

                        var voice = piyanSources[0];
                        voice.Play();
                    }
				}

				else if (weedFinish && !fishingFinish && taxFinish)
				{
					if (hit.collider.gameObject.tag == "Lupas")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = "Nasaan na kaya si Makindo?";

                        var voice = lupasSources[0];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Piyan")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = "Nasaan na kaya si Makindo?";

                        var voice = piyanSources[1];
                        voice.Play();
                    }
				}

				else if (weedFinish && fishingFinish && !taxFinish)
				{
					if (hit.collider.gameObject.tag == "Lupas")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = "Nasaan na kaya si Piyan?";

                        var voice = lupasSources[1];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Makindo")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = "Nasaan na kaya si Piyan?";

                        var voice = makindoSources[1];
                        voice.Play();
                    }
				}

				else if (!weedFinish && !fishingFinish && taxFinish)
				{
					if (hit.collider.gameObject.tag == "Piyan")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = "Nasaan na kaya sina Lupas at Makindo?";

                        var voice = piyanSources[2];
                        voice.Play();
                    }
				}

				else if (!weedFinish && fishingFinish && !taxFinish)
				{
					if (hit.collider.gameObject.tag == "Makindo")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = "Nasaan na kaya sina Lupas at Piyan?";

                        var voice = makindoSources[2];
                        voice.Play();
                    }
				}

				else if (weedFinish && !fishingFinish && !taxFinish)
				{
					if (hit.collider.gameObject.tag == "Lupas")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = "Nasaan na kaya sina Makindo at Piyan?";

                        var voice = lupasSources[2];
                        voice.Play();
                    }
				}

				else if (weedFinish && fishingFinish && taxFinish)
				{
					if (hit.collider.gameObject.tag == "Lupas")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptLupas[7];

                        var voice = lupasSources[4];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Makindo")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptMakindo[6];

                        var voice = makindoSources[4];
                        voice.Play();
                    }
					else if (hit.collider.gameObject.tag == "Piyan")
					{
						LookatPlayer(hit.collider.gameObject);
						ActivateDialogue();
						nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
						dialogueText.GetComponentInChildren<Text>().text = scriptPiyan[3];

                        Test.q[0] = false;

                        var voice = piyanSources[4];
                        voice.Play();
                    }
				}


				//INFO TATOO AND MYTH

				if (hit.collider.gameObject.tag == "Biraman" && biraIntro)
				{
					LookatPlayer(hit.collider.gameObject);
					dialogueBox.SetActive(true);
					nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
					dialogueText.GetComponentInChildren<Text>().text = scriptBiraman[0];
					storyActive = true;
                    questPanel.SetActive(false);

                    var voice = biraSources[0];
                    voice.Play();
                }

				if (hit.collider.gameObject.tag == "Rarak")
				{
					q[8] = false;
					LookatPlayer(hit.collider.gameObject);
					ActivateDialogue();
					nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
					dialogueText.GetComponentInChildren<Text>().text = scriptRarak[0];

                    var voiceR = rarakSources[1];
                    voiceR.Play();
                }
			}




            //Umalohokan

            if (q[9] && !q[1] && !q[2] && !q[3] && !q[4] && !q[5] && !q[6] && !q[7] && !q[8] && !q[10] && !q[11])
            {
                npcCrowd.SetActive(true);
                if (hit.collider.gameObject.tag == "Crowd" && Input.GetButton("BButton"))
                {
                    LookatPlayer(hit.collider.gameObject);
                    dialogueBox.SetActive(true);
                    nameText.GetComponentInChildren<Text>().text = hit.collider.gameObject.tag;
                    dialogueText.GetComponentInChildren<Text>().text = scriptCrowd[0];
                    StartCoroutine(Umalohokan());

                    var voice = crowdSources[0];
                    voice.Play();
                }
            }


            if (Input.GetButton("AButton"))
			{
				if (weedActivated && hit.collider.gameObject.tag == "Grass")
				{
					Destroy(hit.collider.gameObject);
				}

				if (taroActivated && hit.collider.gameObject.tag == "Taro" && !inHand)
				{
					Destroy(hit.collider.gameObject);
					gabi.SetActive(true);
					inHand = true;
				}

				if (fishingActivated && TridentController.fishSpear && hit.collider.gameObject.tag == "Basket")
				{
					activeSpear = true;
					//StartCoroutine(Hold());
					//switch (caughtCount)
					//{
					//    case 1: Fish1.SetActive(true); break;
					//    case 2: Fish2.SetActive(true); break;
					//    case 3: Fish3.SetActive(true); break;
					//    case 4: Fish4.SetActive(true); break;
					//    case 5: Fish5.SetActive(true); break;
					//}
					Fish1.SetActive(true);
					Fish2.SetActive(true);
					Fish3.SetActive(true);
					Fish4.SetActive(true);
					Fish5.SetActive(true);
					//if (caughtCount == 5)
					//{
					//    Debug.Log("Quest Fishing Done!");
					//}
					fishingFinish = true;

					Test.q[6] = false;
					Test.q[7] = false;

					questActive = false;

					Debug.Log("Quest Fishing Done!");
				}

				if (taxActivated && hit.collider.gameObject.tag == "Chicken" && !inHand)
				{
					Destroy(hit.collider.gameObject);
					chicken.SetActive(true);
					inHand = true;
				}
			}

			//d_Datu[0].SetActive(false);
		}


		if (!q[9] && !q[0] && !q[1] && !q[2] && !q[3] && !q[4] && !q[5] && !q[6] && !q[7] && !q[8] && !q[10] && !q[11])
		{
			Test.postTest = true;

            Debug.Log("EndGame");
			//SceneManager.LoadScene(1);
		}

		if (!q[1] && !q[4] && !q[5] && !q[6] && !q[7])
		{
			lupa.transform.position = lupasPosition;
			maki.transform.position = makiPosition2;
			piya.transform.position = piyanPosition;

			weedFinish = true;
			taxFinish = true;
			fishingFinish = true;

            movedL = true;
            movedM = true;
            movedP = true;
        }

        if (!weedFinish && GameObject.FindGameObjectsWithTag("Grass").Length == 0)
        {
            Debug.Log("Quest Weed Finished!");
            QIndicator.SetActive(true);
            QIndText.text = "TAPOS ANG MISYON!";
            QIndicatorAnim.SetBool("isPlaying", true);
            questActive = false;
            weedFinish = true;
        }

        //Debug.Log(currentPage);

        if (storyActive && Input.GetButtonUp("BButton"))
		{
            biraIntro = false;
            var voice = biraSources[1];
            switch (currentPage)
			{
				case 0:
					page1.SetActive(false); page2.SetActive(false); page3.SetActive(false); page4.SetActive(false); page5.SetActive(false); page6.SetActive(false);
					page7.SetActive(false); page8.SetActive(false); page8.SetActive(false); currentPage++;
					break;
				case 1:
					page1.SetActive(false); page2.SetActive(false); page3.SetActive(false); page4.SetActive(false); page5.SetActive(false); page6.SetActive(false);
					page7.SetActive(false); page8.SetActive(false); page8.SetActive(false); currentPage++;
					break;
				case 2:
                    biramanStory.SetActive(true);
                    page1.SetActive(true); page2.SetActive(false); page3.SetActive(false); page4.SetActive(false); page5.SetActive(false); page6.SetActive(false);
					page7.SetActive(false); page8.SetActive(false); page8.SetActive(false); currentPage++;
					dialogueText.GetComponentInChildren<Text>().text = scriptBiraman[currentPage - 2];
                    voice.Stop();
                    voice = biraSources[1];
                    voice.Play();
                    break;
				case 3:
					page1.SetActive(false); page2.SetActive(true); page3.SetActive(false); page4.SetActive(false); page5.SetActive(false); page6.SetActive(false);
					page7.SetActive(false); page8.SetActive(false); page8.SetActive(false); currentPage++;
					dialogueText.GetComponentInChildren<Text>().text = scriptBiraman[currentPage - 2];
                    voice.Stop();
                    voice = biraSources[2];
                    voice.Play();
                    break;
				case 4:
					page1.SetActive(false); page2.SetActive(false); page3.SetActive(true); page4.SetActive(false); page5.SetActive(false); page6.SetActive(false);
					page7.SetActive(false); page8.SetActive(false); page8.SetActive(false); currentPage++;
					dialogueText.GetComponentInChildren<Text>().text = scriptBiraman[currentPage - 2];
                    voice.Stop();
                    voice = biraSources[3];
                    voice.Play();
                    break;
				case 5:
					page1.SetActive(false); page2.SetActive(false); page3.SetActive(false); page4.SetActive(true); page5.SetActive(false); page6.SetActive(false);
					page7.SetActive(false); page8.SetActive(false); page8.SetActive(false); currentPage++;
					dialogueText.GetComponentInChildren<Text>().text = scriptBiraman[currentPage - 2];
                    voice.Stop();
                    voice = biraSources[4];
                    voice.Play();
                    break;
				case 6:
					page1.SetActive(false); page2.SetActive(false); page3.SetActive(false); page4.SetActive(false); page5.SetActive(true); page6.SetActive(false);
					page7.SetActive(false); page8.SetActive(false); page9.SetActive(false); currentPage++;
					dialogueText.GetComponentInChildren<Text>().text = scriptBiraman[currentPage - 2];
                    voice.Stop();
                    voice = biraSources[5];
                    voice.Play();
                    break;
				case 7:
					page1.SetActive(false); page2.SetActive(false); page3.SetActive(false); page4.SetActive(false); page5.SetActive(false); page6.SetActive(true);
					page7.SetActive(false); page8.SetActive(false); page9.SetActive(false); currentPage++;
					dialogueText.GetComponentInChildren<Text>().text = scriptBiraman[currentPage - 2];
                    voice.Stop();
                    voice = biraSources[6];
                    voice.Play();
                    break;
				case 8:
					page1.SetActive(false); page2.SetActive(false); page3.SetActive(false); page4.SetActive(false); page5.SetActive(false); page6.SetActive(false);
					page7.SetActive(true); page8.SetActive(false); page8.SetActive(false); currentPage++;
					dialogueText.GetComponentInChildren<Text>().text = scriptBiraman[currentPage - 2];
                    voice.Stop();
                    voice = biraSources[7];
                    voice.Play();
                    break;
				case 9:
					page1.SetActive(false); page2.SetActive(false); page3.SetActive(false); page4.SetActive(false); page5.SetActive(false); page6.SetActive(false);
					page7.SetActive(false); page8.SetActive(true); page9.SetActive(false); currentPage++;
					dialogueText.GetComponentInChildren<Text>().text = scriptBiraman[currentPage - 2];
                    voice.Stop();
                    voice = biraSources[8];
                    voice.Play();
                    break;
				case 10:
					page1.SetActive(false); page2.SetActive(false); page3.SetActive(false); page4.SetActive(false); page5.SetActive(false); page6.SetActive(false);
					page7.SetActive(false); page8.SetActive(false); page9.SetActive(true); currentPage++;
					dialogueText.GetComponentInChildren<Text>().text = scriptBiraman[currentPage - 2];
                    voice.Stop();
                    voice = biraSources[9];
                    voice.Play();
                    if (q[10] || q[11])
					{
						q[10] = false; q[11] = false;
					}
					break;
			}
			StartCoroutine(NextPage());
		}
	}

	void Neutral(GameObject hit)
	{
		if (hit.tag == "Makindo")
		{
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptMakindo[0];

            var voice = makindoSources[5];
            voice.Play();
        }
		else if (hit.tag == "Piyan")
		{
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptPiyan[0];

            var voice = piyanSources[5];
            voice.Play();
        }
		else if (hit.tag == "Darok")
		{
            if (stakeActivated)
            {
                LookatPlayer(hit);
                ActivateDialogue();
                nameText.GetComponentInChildren<Text>().text = hit.tag;
                dialogueText.GetComponentInChildren<Text>().text = scriptDarok[3];

                var voice = darokSources[2];
                voice.Play();
            }
            else
            {
                LookatPlayer(hit);
                ActivateDialogue();
                nameText.GetComponentInChildren<Text>().text = hit.tag;
                dialogueText.GetComponentInChildren<Text>().text = scriptDarok[0];

                var voice = darokSources[0];
                voice.Play();

            }
        }
		else if (hit.tag == "Silanday")
		{
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptSilanday[0];

            var voice = silandaySources[0];
            voice.Play();
        }
		else if (hit.tag == "Ibusun")
		{
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptIbusun[0];

            var voice = umalSources[0];
            voice.Play();
        }
		else if (hit.tag == "Lupas")
		{
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptLupas[3];
		}
		else if (hit.tag == "Enita")
		{
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptEnita[1];

            var voice = enitaSources[1];
            voice.Play();
        }
		else if (hit.tag == "Datu")
		{
            if (taroFinish)
            {
                LookatPlayer(hit);
                ActivateDialogue();
                nameText.GetComponentInChildren<Text>().text = hit.tag;
                dialogueText.GetComponentInChildren<Text>().text = scriptDatu[7];

                var voice = datuSources[8];
                voice.Play();
            }
            else
            {
                LookatPlayer(hit);
                ActivateDialogue();
                nameText.GetComponentInChildren<Text>().text = hit.tag;
                dialogueText.GetComponentInChildren<Text>().text = scriptDatu[1];

                var voice = datuSources[3];
                voice.Play();
            }
        }
		else if (hit.tag == "AmaLupas")
		{
            if (weedFinish)
            {
                LookatPlayer(hit);
                ActivateDialogue();
                nameText.GetComponentInChildren<Text>().text = hit.tag;
                dialogueText.GetComponentInChildren<Text>().text = scriptAmal[2];

                var voice = tirugoSources[2];
                voice.Play();
            }
            else
            {
                LookatPlayer(hit);
                ActivateDialogue();
                nameText.GetComponentInChildren<Text>().text = hit.tag;
                dialogueText.GetComponentInChildren<Text>().text = scriptAmal[3];

                var voice = tirugoSources[3];
                voice.Play();
            }
		}
	}

	void Makindo(GameObject hit)
	{
		if (indicatorFish)
			StartCoroutine(RandomProbFish(hit));
	}

	void Silanday(GameObject hit)
	{
		if (indicatorTax)
			StartCoroutine(RandomProbTax(hit));
	}

	void Darok(GameObject hit)
	{
		if (indicatorStake)
			StartCoroutine(RandomProbStake(hit));
	}

	void Enita(GameObject hit)
	{
		if (indicatorTaro)
			StartCoroutine(RandomProbTaro(hit));
	}

	void AmaLupas(GameObject hit)
	{
		if (indicatorWeed)
			StartCoroutine(RandomProbWeed(hit));
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

	IEnumerator Umalohokan()
	{
		yield return new WaitForSeconds(2f);
		dialogueBox.SetActive(true);
		nameText.GetComponentInChildren<Text>().text = "Ibusun";
		dialogueText.GetComponentInChildren<Text>().text = scriptIbusun[3];

        var voice = umalSources[4];
        voice.Play();

        Test.q[9] = false;
	}

	IEnumerator Hold()
	{
		if (weedCount == 1 && !finishWeed)
		{
			yield return new WaitForSeconds(1);
			weedActivated = true;
		}

		if (taroCount == 1 && !taroFinish)
		{
			yield return new WaitForSeconds(1);
			taroActivated = true;
		}

		if (fishActivated && !fishingFinish)
		{
			yield return new WaitForSeconds(1);
			maki.transform.position = makiPosition;
		}

		if (fishCount == 1 && !fishingFinish)
		{
			Debug.Log("FishActivate!");
			yield return new WaitForSeconds(1);
			fishingActivated = true;
			Debug.Log(fishingActivated);
			Debug.Log(fishCount);
		}

		if (moveLupas && finishWeed)
		{
			lupa.transform.position = lupasPosition;
			moveLupas = false;
		}

		if (moveMakindo && fishingFinish)
		{
			maki.transform.position = makiPosition2;
			moveMakindo = false;
		}

		if (stakeCount == 1 && !stakeFinish)
		{
			yield return new WaitForSeconds(1);
			stakeActivated = true;
		}

		if (taxCount == 1 && !taxFinish)
		{
			yield return new WaitForSeconds(1);
			taxActivated = true;
		}

		if (TridentController.fishSpear && !fishingFinish)
		{
			caughtCount++;
			yield return new WaitForSeconds(1);
		}
	}

	IEnumerator NextPage()
	{
		storyActive = false;
		yield return new WaitForSeconds(2f);
		storyActive = true;
	}

	IEnumerator Hint()
	{
		//Weed
		if (!weedTrigger1 && hintWeed)
		{
			yield return new WaitForSeconds(2f);
			weedTrigger1 = true;
		}
		else if (!weedTrigger2)
		{
			yield return new WaitForSeconds(2f);
			weedTrigger1 = false;
			weedTrigger2 = true;
		}
		else if (!weedTrigger3)
		{
			yield return new WaitForSeconds(2f);
			weedTrigger2 = false;
			weedTrigger3 = true;
		}
		//Fish
		if (!fishTrigger1 && hintFish)
		{
			yield return new WaitForSeconds(2f);
			fishTrigger1 = true;
		}
		else if (!fishTrigger2)
		{
			yield return new WaitForSeconds(2f);
			fishTrigger1 = false;
			fishTrigger2 = true;
		}
		//Tax
		if (!taxTrigger1 && hintTax)
		{
			yield return new WaitForSeconds(2f);
			taxTrigger1 = true;
		}
		else if (!taxTrigger2)
		{
			yield return new WaitForSeconds(2f);
			taxTrigger1 = false;
			taxTrigger2 = true;
		}
		else if (!taxTrigger3)
		{
			yield return new WaitForSeconds(2f);
			taxTrigger2 = false;
			taxTrigger3 = true;
		}
		//Taro
		if (!taroTrigger1 && hintTaro)
		{
			yield return new WaitForSeconds(2f);
			taroTrigger1 = true;
		}
		else if (!taroTrigger2)
		{
			yield return new WaitForSeconds(2f);
			taroTrigger1 = false;
			taroTrigger2 = true;
		}
		//Stake
		if (!stakeTrigger1 && hintStake)
		{
			yield return new WaitForSeconds(2f);
			stakeTrigger1 = true;
		}
	}

	IEnumerator RandomProbWeed(GameObject hit)
	{
		indicatorWeed = false;
		yield return new WaitForSeconds(0.5f);
		float pb = probWeed * 100f;
		int ipb = Convert.ToInt32(pb);
		int random = GetRandomValue(
			new RandomSelection(0, ipb, pb / 100f),
			new RandomSelection(ipb, 101, (100 - pb) / 100f)
		);
		Debug.Log(random + " " + pb + " " + probWeed);
		if (random <= pb)
		{
			QIndicator.SetActive(true);
			QIndText.text = "BAGONG MISYON!";
			QIndicatorAnim.SetBool("isPlaying", true);
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptAmal[0];
			questActive = true;
			indicatorWeed = true;
			weedCount++;
			StartCoroutine(Hold());

            var voice = tirugoSources[2];
            voice.Play();
        }
		else
		{
			indicatorWeed = true;
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptAmal[3];

            var voice = tirugoSources[3];
            voice.Play();
        }
	}

	IEnumerator RandomProbFish(GameObject hit)
	{
		indicatorFish = false;
		yield return new WaitForSeconds(0.5f);
		float pb = probFish * 100f;
		int ipb = Convert.ToInt32(pb);
		int random = GetRandomValue(
			new RandomSelection(0, ipb, pb / 100f),
			new RandomSelection(ipb, 101, (100 - pb) / 100f)
		);
		Debug.Log(random + " " + pb + " " + probFish);
		if (random <= pb)
		{
			QIndicator.SetActive(true);
			QIndText.text = "BAGONG MISYON!";
			QIndicatorAnim.SetBool("isPlaying", true);
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptMakindo[1];
			indicatorFish = true;
			questActive = true;
			fishCount++;
			StartCoroutine(Hold());

            var voice = makindoSources[6];
            voice.Play();
        }
		else
		{
			indicatorFish = true;
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptMakindo[0];
		}
	}

	IEnumerator RandomProbTax(GameObject hit)
	{
		indicatorTax = false;
		yield return new WaitForSeconds(0.5f);
		float pb = probTax * 100f;
		int ipb = Convert.ToInt32(pb);
		int random = GetRandomValue(
			new RandomSelection(0, ipb, pb / 100f),
			new RandomSelection(ipb, 101, (100 - pb) / 100f)
		);
		if (random <= pb)
		{
			QIndicator.SetActive(true);
			QIndText.text = "BAGONG MISYON!";
			QIndicatorAnim.SetBool("isPlaying", true);
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptSilanday[1];
			indicatorTax = true;
			questActive = true;
			taxCount++;
			StartCoroutine(Hold());

            var voice = silandaySources[1];
            voice.Play();
        }
		else
		{
			indicatorTax = true;
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptSilanday[0];
		}
	}

	IEnumerator RandomProbTaro(GameObject hit)
	{
		indicatorTaro = false;
		yield return new WaitForSeconds(0.5f);
		float pb = probTaro * 100f;
		int ipb = Convert.ToInt32(pb);
		int random = GetRandomValue(
			new RandomSelection(0, ipb, pb / 100f),
			new RandomSelection(ipb, 101, (100 - pb) / 100f)
		);
		if (random <= pb)
		{
			QIndicator.SetActive(true);
			QIndText.text = "BAGONG MISYON!";
			QIndicatorAnim.SetBool("isPlaying", true);
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptEnita[3];
			indicatorTaro = true;
			questActive = true;
			taroCount++;
			StartCoroutine(Hold());

            var voice = enitaSources[3];
            voice.Play();
        }
		else
		{
			indicatorTaro = true;
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptEnita[1];

            var voice = enitaSources[1];
            voice.Play();
        }
	}

	IEnumerator RandomProbStake(GameObject hit)
	{
		indicatorStake = false;
		yield return new WaitForSeconds(0.5f);
		float pb = probStake * 100f;
		int ipb = Convert.ToInt32(pb);
		int random = GetRandomValue(
			new RandomSelection(0, ipb, pb / 100f),
			new RandomSelection(ipb, 101, (100 - pb) / 100f)
		);
		if (random <= pb)
		{
			QIndicator.SetActive(true);
			QIndText.text = "BAGONG MISYON!";
			QIndicatorAnim.SetBool("isPlaying", true);
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptDarok[1];
			indicatorStake = true;
			questActive = true;
			stakeCount++;
			StartCoroutine(Hold());

            var voice = darokSources[1];
            voice.Play();
        }
		else
		{
			indicatorStake = true;
			LookatPlayer(hit);
			ActivateDialogue();
			nameText.GetComponentInChildren<Text>().text = hit.tag;
			dialogueText.GetComponentInChildren<Text>().text = scriptDarok[0];
		}
	}
	//Button OnCliCK

	public void ChoiceOne()
	{
		if (!postflag)
		{
			if (randomQuestion == 5 || randomQuestion == 6 || randomQuestion == 10)
			{
				correct.Play();
				Debug.Log("TAMA");
				Test.scoreCount++;
				Test.q[randomQuestion] = false;
				x++;
			}
			else
			{
				wrong.Play();
				Debug.Log("MALI");
				Test.q[randomQuestion] = true;
				x++;
			}
            GameObject.Find("ScoreCount").GetComponentInChildren<Text>().text = scoreCount + "/12";
        }
		else if (postflag && x < 12)
        {
			if (randomQuestion == 5 || randomQuestion == 6 || randomQuestion == 10)
			{
                correct.Play();
                Test.postCount++;
				x++;
                Debug.Log(postCount);
            }
			else
			{
                wrong.Play();
                x++;
			}
            GameObject.Find("ScoreCount").GetComponentInChildren<Text>().text = postCount + "/12";
        }
		//rand[randomQuestion] = false;
		buttonclick = 0;
		//score();
	}

	public void ChoiceTwo()
	{
		if (!postflag)
		{
			if (randomQuestion == 0 || randomQuestion == 1 || randomQuestion == 3 || randomQuestion == 8 || randomQuestion == 11)
			{
				correct.Play();
				Debug.Log("TAMA");
				Test.scoreCount++;
				Test.q[randomQuestion] = false;
				x++;
			}
			else
			{
				wrong.Play();
				Debug.Log("MALI");
				Test.q[randomQuestion] = true;
				//this lies the effect if not answered
				x++;
			}
            GameObject.Find("ScoreCount").GetComponentInChildren<Text>().text = scoreCount + "/12";
        }
		else if (postflag && x < 12)
		{
			if (randomQuestion == 0 || randomQuestion == 1 || randomQuestion == 3 || randomQuestion == 8 || randomQuestion == 11)
			{
                correct.Play();
                Test.postCount++;
				x++;
                Debug.Log(postCount);
			}
			else
			{
                wrong.Play();
                x++;
			}
            GameObject.Find("ScoreCount").GetComponentInChildren<Text>().text = postCount + "/12";
        }
		//rand[randomQuestion] = false;
		buttonclick = 0;
		//score();
	}

	public void ChoiceThree()
	{
		if (!postflag)
		{
			if (randomQuestion == 2 || randomQuestion == 4 || randomQuestion == 7 || randomQuestion == 9)
			{
				correct.Play();
				Debug.Log("TAMA");
				Test.scoreCount++;
				Test.q[randomQuestion] = false;
				x++;
			}
			else
			{
				wrong.Play();
				Debug.Log("MALI");
				Test.q[randomQuestion] = true;
				//this lies the effect if not answered
				x++;
			}
            GameObject.Find("ScoreCount").GetComponentInChildren<Text>().text = scoreCount + "/12";
        }
		else if (postflag && x<12)
        {
			if (randomQuestion == 2 || randomQuestion == 4 || randomQuestion == 7 || randomQuestion == 9)
			{
                correct.Play();
                Test.postCount++;
				x++;
                Debug.Log(postCount);
            }
			else
			{
                wrong.Play();
                x++;
			}
            GameObject.Find("ScoreCount").GetComponentInChildren<Text>().text = postCount + "/12";
        }
		//rand[randomQuestion] = false;
		buttonclick = 0;
		//score();
	}

}
