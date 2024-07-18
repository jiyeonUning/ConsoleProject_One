namespace gh_textRPG
{
    internal class Program
    {
        enum Scene
        {
            내방_시작, 아래층1, 아래층2,
            책상, 엔딩불효녀,
            인벤토리, 아이템재봉가위, 아이템재봉키트,
            동의함, 엔딩미식가,
            바깥
        }

        enum Item { 재봉가위, 재봉키트 }

        enum EndingScene { 불효녀, 미식가 }

        struct GameData
        {
            public bool running;
            public Scene scene;  // 여기까지 집
            public Scene prevScene; // 인벤토리 이동 전 이전 화면 저장용


            public EndingScene ending;

            public Item item;  // 아이템 저장고
            public string itemName; // 아이템이름

            public bool running2;
            public bool[,] map;
            public ConsoleKey inputKey;
            public Point playerPos;    // 플레이어 위치
            public Point goalPos;      // 골 위치
        }

        static GameData data;

        static void Main(string[] args)
        {
            Start();

            while (data.running)
            {
                Run();
            }
            End();
        }  //  게임루프

        static void Start()
        {
            data = new GameData();
            data.running = true;

            Console.Clear();
            Console.WriteLine("                  =====================================");
            Console.WriteLine("                  =                                   =");

            Console.Write("                  =        ~ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("빨간");
            Console.ResetColor();
            Console.Write(" 망토");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" 이야기");
            Console.ResetColor();
            Console.WriteLine(" ~       =");

            Console.WriteLine("                  =                                   =");
            Console.WriteLine("                  =====================================");
            Console.WriteLine();
            Console.WriteLine("=========================================================================");
            Console.WriteLine("=                                                                       =");
            Console.WriteLine("=                       당신은 '빨간 망토' 입니다!                      =");
            Console.WriteLine("=  배드엔딩을 보지 않고 무사히 할머니 댁에 도착하게 되면 클리어입니다.  =");
            Console.WriteLine("=                              행운을 빌어요.                           =");
            Console.WriteLine("=                                                                       =");
            Console.WriteLine("=========================================================================");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("               계 속 하 려 면   아 무 키 나   누 르 세 요                ");
            Console.ReadKey();
        }  // 시작 화면

        static void End()
        { data.running = false; } // 게임 종료

        static void Run()  //  장면 출력
        {
            Console.Clear();
            switch (data.scene)
            {
                case Scene.내방_시작:  // 오프닝
                    MyRoom();
                    break;

                case Scene.아래층1:  //  내려간다
                    Under1();
                    break;
                case Scene.아래층2:  //  내려가지 않는다 (방에서의 단독행동으로 이어짐)
                    Under2();
                    break;

                case Scene.엔딩불효녀:  // 침대 탐색 선택 시.
                    data.ending = EndingScene.불효녀;
                    End();
                    return; // = 여기 둠으로써 바로 switch를 끝내주는 것. (피드백 받음)
                case Scene.책상:  // 책상 탐색 외 침대 탐색 시 엔딩, 그냥 내려갈 시 위 아래층1씬 출력
                    Table();
                    break;

                case Scene.동의함:  //  동의함.
                    Agree();
                    break;
                case Scene.엔딩미식가:  // 동의하지 않음 선택 시 엔딩.
                    data.ending = EndingScene.미식가;
                    End();
                    return;
                case Scene.바깥:        // 이동형으로 바뀌기 전 출력할 스크립트.
                    Outside();
                    break;

                case Scene.인벤토리:  // 아이템 획특 시 출력할 스크립트.
                    PrintItem();
                    break;
                case Scene.아이템재봉가위:
                    Scissors();
                    break;
                case Scene.아이템재봉키트:
                    Sew();
                    break;
            }
        }

        static void InvenIn()  //  인벤토리 들어가기
        {
            Console.Clear();
            Console.WriteLine("가지고 있는 물건을 확인합니다...");
            Wait(2);
            data.prevScene = data.scene;
            data.scene = Scene.인벤토리;
        }

        static void InvenOut()  // 인벤토리 나오기
        {
            Console.Clear();
            Console.WriteLine("물건을 정리합니다...");
            Wait(2);
            data.scene = data.prevScene;
        }

        static void PrintItem()  //  아이템 목록 출력
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==============================================");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("                빨 간");
            Console.ResetColor();
            Console.WriteLine("  망 토");

            Console.WriteLine();
            Console.WriteLine($"          : 소지하고 있는 아이템 :");
            Console.WriteLine($" {data.itemName,-6}");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("===============================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("돌 아 가 려 면   9 번  키 를   눌 러 주 세 요 .");

            string input = Console.ReadLine();

            switch (input)
            {
                case "9":
                    InvenOut();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못 입력하셨습니다! 9번을 골라주세요. 스크립트를 다시 출력합니다...");
                    Wait(2);
                    PrintItem();
                    break;
            }
        }

        static void Wait(float seconds)
        {
            Thread.Sleep((int)(seconds * 1000)); ;
        }  // 로딩 구현


        #region 하우스
        static void MyRoom()  // 어머니가 빨간망토를 부르는 장면
        {   //  Render
            Console.WriteLine("================================================================================================");
            Console.WriteLine();
            Console.WriteLine("\"빨간 망토야, 어서 내려와보렴!\"");
            Console.WriteLine("이른 아침부터, 무슨일인지 어머니가 아래층에서 부터 당신을 부르고 있습니다. 무슨 일일까요?");
            Console.WriteLine("바로 내려가시겠습니까?");
            Console.WriteLine();
            Console.WriteLine("================================================================================================");
            Console.WriteLine();
            Console.WriteLine("1. 내려간다");
            Console.WriteLine("2. 내려가지 않는다.");
            Console.WriteLine("0. 가지고 있는 물건들을 확인한다.");
            Console.Write("선택 : ");

            string input = Console.ReadLine();  //  Input

            switch (input)  //  Update
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("당신은 어머니의 부름에 따라 내려가보기로 하였습니다...");
                    Wait(2);
                    data.scene = Scene.아래층1;
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("당신은 어머니의 부름을 무시하기로 했습니다...");
                    Wait(2);
                    data.scene = Scene.아래층2;
                    break;
                case "0":
                    InvenIn();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못 입력하셨습니다! 보기의 선택지에서 골라주세요. 스크립트를 다시 출력합니다...");
                    Wait(2);
                    data.scene = Scene.내방_시작;
                    break;
            }
        }

        static void Under1()  // 어머니가 부르는 소리에 1. 내려간다
        {
            Console.WriteLine("================================================================================================");
            Console.WriteLine();
            Console.WriteLine("\"빨간 망토야! 좋은 아침이로구나.\" 당신의 어머니가 언제나처럼 온화한 미소로 당신을 반깁니다.");
            Console.WriteLine("하지만 그것은 곧 걱정스런 얼굴로 바뀌더니, 잠깐 머뭇거리시던 어머니는 어렵사리 입을 여셨습니다.");
            Console.WriteLine();
            Console.WriteLine("\"너도 알다시피, 지금 할머니께서 많이 편찮으시잖니. 혼자 사시다보니 더욱 걱정이 되는구나.\"");
            Console.WriteLine("\"엄마가 가기에는 요즘 장사일이 너무 바빠서...\"");
            Console.WriteLine();
            Console.WriteLine("\"빨간 망토야, 이 포도주와 빵이 든 바구니를 들고 할머니 댁에 잠시 다녀오지 않겠니?\"");
            Console.WriteLine("\"네가 말동무를 해주면 할머니께서도 분명 기뻐하실거야.\"");
            Console.WriteLine();
            Console.WriteLine("===============================================================================================");
            Console.WriteLine();
            Console.WriteLine("어떻게 하시겠습니까?");
            Console.WriteLine("1. 어머니의 말을 따른다.");
            Console.WriteLine("2. 어머니의 말을 따르지 않는다.");
            Console.WriteLine("0. 가지고 있는 물건들을 확인한다.");

            Console.Write("선택 : ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("어머니의 말에 고개를 끄덕입니다...");
                    Wait(2);
                    data.scene = Scene.동의함;
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("어머니의 말에 고개를 젓습니다...");
                    Wait(1);
                    Gourmet();
                    End();
                    break;
                case "0":
                    InvenIn();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못 입력하셨습니다! 보기의 선택지에서 골라주세요. 스크립트를 다시 출력합니다...");
                    Wait(2);
                    data.scene = Scene.아래층1;
                    break;
            }
        }

        static void Under2()  // 어머니가 부르는 소리에 2. 내려가지 않는다
        {
            Console.WriteLine("=================================================================");
            Console.WriteLine();
            Console.WriteLine("잠깐 안 내려간다고 화를 내시진 않을겁니다. 설마요.");
            Console.WriteLine("당신은 2층에 위치해있는, 당신의 아기자기한 방을 바라봅니다.");
            Console.WriteLine();
            Console.WriteLine("=================================================================");
            Console.WriteLine();
            Console.WriteLine("무엇을 하시겠습니까?");
            Console.WriteLine("1. 침대를 살핀다.");
            Console.WriteLine("2. 책상 위를 살핀다.");
            Console.WriteLine("3. 그냥 내려간다.");
            Console.WriteLine("0. 가지고 있는 물건들을 확인한다.");
            Console.Write("선택 : ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("당신은 침대를 살핍니다...");
                    Wait(1);
                    NotDutifulDaughter();
                    End();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("당신은 책상 위를 살핍니다...");
                    Wait(2);
                    data.scene = Scene.책상;
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("당신은 어머니의 부름에 따라 내려가보기로 하였습니다...");
                    Wait(2);
                    data.scene = Scene.아래층1;
                    break;
                case "0":
                    InvenIn();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못 입력하셨습니다! 보기의 선택지에서 골라주세요. 스크립트를 다시 출력합니다...");
                    Wait(2);
                    data.scene = Scene.아래층2;
                    break;
            }
        }

        static void NotDutifulDaughter()  // 1. 침대 위를 살핀다... 배드엔딩1. 잠자는 숲속의 불효녀
        {
            Console.WriteLine("=========================================================================================");
            Console.WriteLine();
            Console.WriteLine("따듯한 햇살, 지저귀는 새들의 소리. 다시 단잠에 빠지기에 최적의 환경이네요.");
            Console.WriteLine("졸음을 참지 못한 당신은, 머리를 감싼 거추장스런 두건을 벗고 침대 위로 올라갑니다.");
            Console.WriteLine("아래서 당신을 애타게 부르는 소리는 포근한 햇볕과 잠자리에 뭍혀져만 갑니다...");
            Console.WriteLine();
            Console.WriteLine("=========================================================================================");
            Console.WriteLine("엔터키를 입력하세요.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("=                                                =");
            Console.WriteLine("=      BAD ENDING 1, 잠자는 숲속의 불효녀        =");
            Console.WriteLine("=                                                =");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("처 음 부 터   다 시   도 전 해 보 세 요 .");

            data.ending = EndingScene.불효녀;
        }

        static void Table()  // 2. 책상 위를 살핀다.
        {
            Console.WriteLine("====================================================================================================");
            Console.WriteLine();
            Console.WriteLine("당신의 취미는 재봉입니다. 당신이 쓰고 있는 빨간색의 귀여운 두건 또한 당신의 작품이지요.");
            Console.WriteLine("재봉틀, 바늘꽂이, 가위 등.... 당신의 주 작업공간인 책상 위에는 여러 재봉 도구들이 가득하네요.");
            Console.WriteLine();
            Console.WriteLine("====================================================================================================");
            Console.WriteLine();
            Console.WriteLine("물건을 챙기시겠습니까? 둘 중 하나만 가져갈 수 있습니다.");
            Console.WriteLine("1. 가위를 챙긴다.");
            Console.WriteLine("2. 실과 바늘을 챙긴다.");
            Console.WriteLine("3. 아무것도 챙기지 않는다. (=그냥 내려간다.)");
            Console.WriteLine("0. 가지고 있는 물건들을 확인한다.");
            Console.Write("선택 : ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("가위를 챙깁니다...");
                    Wait(2);
                    data.scene = Scene.아이템재봉가위;
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("바늘과 실을 챙깁니다...");
                    Wait(2);
                    data.scene = Scene.아이템재봉키트;
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("당신은 어머니의 부름에 따라 내려가보기로 하였습니다...");
                    Wait(2);
                    data.scene = Scene.아래층1;
                    break;
                case "0":
                    InvenIn();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못 입력하셨습니다! 보기의 선택지에서 골라주세요. 스크립트를 다시 출력합니다...");
                    Wait(2);
                    data.scene = Scene.책상;
                    break;
            }
        }

        static void Scissors()  // 가위 획득
        {
            Console.WriteLine("====================================================================================================");
            Console.WriteLine();
            Console.WriteLine("무엇이든 사각사각 자를 수 있을 것만 같은 재봉 가위입니다. 산지 얼마 되지 않아 날이 매섭습니다.");
            Console.WriteLine("끝이 뾰족하니 찔리지 않도록 주의합시다. 당신은 그것을 손수건에 잘 감싸 주머니에 쏘옥 넣어둡니다.");
            Console.WriteLine("언젠가는 쓸만할 때가 올지도 모르니까요.");
            Console.WriteLine();
            Console.WriteLine("====================================================================================================");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("재봉가위 +");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("1. 가위를 챙기고 아래로 내려간다.");
            Console.WriteLine("0. 가지고 있는 물건들을 확인한다.");
            Console.Write("선택 : ");

            data.itemName = "재봉 가위";

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("가위를 챙기고 아래로 내려갑니다...");
                    Wait(2);
                    data.scene = Scene.아래층1;
                    break;
                case "0":
                    InvenIn();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못 입력하셨습니다! 1번을 골라주세요. 스크립트를 다시 출력합니다...");
                    Wait(2);
                    data.scene = Scene.아이템재봉가위;
                    break;
            }

        }

        static void Sew() // 재봉키트 획득
        {
            Console.WriteLine("====================================================================================================");
            Console.WriteLine();
            Console.WriteLine("아무리 큰 구멍이라도 이 실과 바늘 앞에서는 어쩔 방도가 없을 것이 분명합니다.");
            Console.WriteLine("비록 당신 손만한 크기의 조그만 재봉키트이지만... 아무튼 주머니에 소중히 넣어둡니다.");
            Console.WriteLine("천과 천 사이를 꿰맬 때 유용히 사용할 수 있을 것 같습니다. 실이 모자라지 않는 한에는 말이죠.");
            Console.WriteLine();
            Console.WriteLine("====================================================================================================");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("미니 재봉 키트 (가위 미포함) +");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("1. 재봉키트를 챙기고 아래로 내려간다.");
            Console.WriteLine("0. 가지고 있는 물건들을 확인한다.");
            Console.Write("선택 : ");

            data.itemName = "미니 재봉 키트";

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("재봉키트를 챙기고 아래로 내려갑니다...");
                    Wait(2);
                    data.scene = Scene.아래층1;
                    break;
                case "0":
                    InvenIn();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못 입력하셨습니다! 1번을 골라주세요. 스크립트를 다시 출력합니다...");
                    Wait(2);
                    data.scene = Scene.아이템재봉키트;
                    break;
            }
        }

        static void Agree()  // 어머니의 말을 1. 따른다
        {
            Console.WriteLine("===========================================================================================================");
            Console.WriteLine();
            Console.WriteLine("\"정말 고맙구나! 할머니께서도 분명 기뻐하실거야.\" 어머니가 행복하게 웃었습니다.");
            Console.WriteLine();
            Console.WriteLine("\"이미 알고 있겠지만, 할머니 댁은 숲속으로 향하는 오솔길을 쭉 따라가면 나온단다.\"");
            Console.WriteLine("\"명심하려무나. 다른 길로 새지 말고, 곧장 할머니 댁으로 가도록 하렴. 모르는 사람은 따라가면 안된다!\"");
            Console.WriteLine();
            Console.WriteLine("어머니가 당부하자, 당신은 걱정할 것 없다는 듯 어깨를 으쓱합니다.");
            Console.WriteLine("그 늠름한 모습을 보던 어머니는 한숨을 한번 푸욱 쉬시고는, 당신에게 묵직한 바구니를 건네주었습니다.");
            Console.WriteLine();
            Console.WriteLine("\"잘 다녀오려무나.\"");
            Console.WriteLine();
            Console.WriteLine("===========================================================================================================");
            Console.WriteLine();
            Console.WriteLine("집을 나섭니까?");
            Console.WriteLine("1. 다녀오겠습니다!");
            Console.WriteLine("0. 가지고 있는 물건들을 확인한다.");
            Console.Write("선택 : ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("당신은 집 밖으로 나섭니다...");
                    Wait(2);
                    data.scene = Scene.바깥;
                    break;
                case "0":
                    InvenIn();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못 입력하셨습니다! 1번을 골라주세요. 스크립트를 다시 출력합니다...");
                    Wait(2);
                    data.scene = Scene.동의함;
                    break;
            }
        }

        static void Gourmet()  //  2. 어머니의 말을 따르지 않는다...  배드엔딩2. 고독한 미식가
        {
            Console.WriteLine("=========================================================================================");
            Console.WriteLine();
            Console.WriteLine("당신은 어머니의 부탁을 거절하였습니다. 10살한테 뭘 시키는거예요?");
            Console.WriteLine("어머니도 이해한다는 듯이 고개를 끄덕였습니다. \"그래, 네겐 아직 버거울 부탁이긴 했지.\"");
            Console.WriteLine();
            Console.WriteLine("\"그럼 이 바구니를 할머니께 가져다드릴 사람을 따로 찾아봐야겠구나...\"");
            Console.WriteLine("\"이른 아침부터 깨워 미안하구나. 배고프지? 아침 금방 차려줄게! 기다리렴.\"");
            Wait(1);
            Console.WriteLine("그렇게 말한 어머니는 금방 아침식사를 내어주셨습니다.");
            Console.WriteLine("갓 구운 베이컨, 계란. 그리고 신선한 우유와 빵... 푸짐하네요!");
            Console.WriteLine();
            Console.WriteLine("\"그럼 엄마는 옆집 청년에게 부탁해보러 다녀오마. 먹고 있으려무나.\"");
            Console.WriteLine("비록 혼자서 즐기는 식사지만, 당신은 집에서 즐겁게 아침식사를 할 수 있었습니다.");
            Console.WriteLine();
            Console.WriteLine("=========================================================================================");
            Console.WriteLine("엔터키를 입력하세요.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("===========================================");
            Console.WriteLine("=                                         =");
            Console.WriteLine("=      BAD ENDING 2, 고독한 미식가        =");
            Console.WriteLine("=                                         =");
            Console.WriteLine("===========================================");
            Console.WriteLine();
            Console.WriteLine("처 음 부 터   다 시   도 전 해 보 세 요 .");

            data.ending = EndingScene.미식가;
        }

        static void Outside()
        {
            Console.WriteLine("=========================================================================================");
            Console.WriteLine();
            Console.WriteLine("이렇게 이른 아침부터 심부름을 나온 것은 처음이라 그런가, 당신은 조금 긴장됩니다.");
            Console.WriteLine("마침 기분좋게 서늘하 바람이 불고 있네요. 좋은 여정의 시작입니다!");
            Console.WriteLine();
            Console.WriteLine("=========================================================================================");
            Console.WriteLine();
            Console.WriteLine("1. 앞으로 나아간다.");
            Console.WriteLine("0. 가지고 있는 물건들을 확인한다.");
            Console.Write("선택 : ");

            string input = Console.ReadLine();

            if (input == "1")
            {
                Town();
                while (data.running)
                {
                    Render();
                    Input();
                    Update();
                }
                ENDING();
            }

            switch (input)
            {
                case "0":
                    InvenIn();
                    break;
                default:
                    data.scene = Scene.바깥;
                    break;
            }
        }

        public struct Point
        { public int x; public int y; }  //  플레이어와 골의 위치좌표

        static void Town()
        {
            Console.CursorVisible = false;  //  커서 없애는 용도

            data = new GameData();

            data.running = true;
            data.map = new bool[,]
                {
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                { false, true, true, false, true, true, true, true, true, true, true, true, false, true, false },
                { false, true, true, false, false, false, false, true, true, false, true, true, false, true, false },
                { false, true, true, true, true, true, false, true, true, false, true, true, false, true, false },
                { false, false, false, false, true, true, false, true, true, false, true, true, false, true, false },
                { false, true, true, false, true, true, false, true, true, false, true, true, false, true, false },
                { false, false, true, true, true, true, true, true, true, false, true, true, true, true, false },
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false }
                };

            data.playerPos = new Point() {x = 1, y = 5 };
            data.goalPos = new Point() { x = 13, y = 1 };

            Console.Clear();
            Console.WriteLine("=============================================");
            Console.WriteLine("  집 에 서   숲 까 지   이 동 해 보 세 요 !  ");
            Console.WriteLine("=============================================");
            Console.WriteLine();
            Console.WriteLine("      아 무  키 나  눌 러   시 작 하 기      ");
            Console.ReadKey();
        }


        static void ENDING()
        {
            Console.Clear();
            Console.WriteLine("======================================");
            Console.WriteLine("=                                    =");
            Console.WriteLine("=            여 기 까 지  !          =");
            Console.WriteLine("=                                    =");
            Console.WriteLine("======================================");
            Console.WriteLine();
            Console.WriteLine("   뒷내용은 아직 개발 중입니다 . . .");
            End();
        }

        static void Render()
        {
            Console.Clear();

            PrintMap();  // 맵을 생성
            PrintPlayer();  //  플레이어를 위치에 생성
            PrintGoal();  //  골을 위치에 생성
        }


        static void Update()
        {
            Move();
            Clear(); // 엔딩
        }

        static void PrintMap()
        {
            for (int y = 0; y < data.map.GetLength(0); y++)
            {
                for (int x = 0; x < data.map.GetLength(1); x++)
                {
                    if (data.map[y, x]) //  =  2차원배열을 통해
                    {
                        Console.Write(" ");  //  =  움직일 수 있는 지역
                    }
                    else  //  =  움직일 수 없는 지역
                    {
                        Console.Write("#");
                    }
                }
                Console.WriteLine();
            }
        }

        static void Clear()
        {
            if (data.playerPos.x == data.goalPos.x &&  // 플레이어의 위치값과
                data.playerPos.y == data.goalPos.y)    // 골의 위치값이 같으면
            {
                data.running = false;                  // 게임이 끝남
            }
        }

        static void PrintPlayer()
        {   //  Console.SetCursorPosition = 커서의 위치
            //                             (여기에서, 여기만큼); 이라는 뜻
            Console.SetCursorPosition(data.playerPos.x, data.playerPos.y);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("@"); //  빨간망토의 머리
            Console.ResetColor();
        }

        static void PrintGoal()
        {
            Console.SetCursorPosition(data.goalPos.x, data.goalPos.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("<");  // 골인지점 표시
            Console.ResetColor();
        }

        static void Input()
        {
            data.inputKey = Console.ReadKey(true).Key;  // 방향키 설정.
        }

        static void Move()  // 방향키 적용
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
            }
        }

        static void MoveUp()
        {
            Point next = new Point() { x = data.playerPos.x, y = data.playerPos.y - 1 };
            if (data.map[next.y, next.x])
            {
                data.playerPos = next;  // = if ~를 해줌으로써 벽인 부분을 벽으로 인식시켜주는 역할
            }
        }

        static void MoveDown()
        {
            Point next = new Point() { x = data.playerPos.x, y = data.playerPos.y + 1 };
            if (data.map[next.y, next.x])
            {
                data.playerPos = next;
            }
        }

        static void MoveLeft()
        {
            Point next = new Point() { x = data.playerPos.x - 1, y = data.playerPos.y };
            if (data.map[next.y, next.x])
            {
                data.playerPos = next;
            }
        }

        static void MoveRight()
        {
            Point next = new Point() { x = data.playerPos.x + 1, y = data.playerPos.y };
            if (data.map[next.y, next.x])
            {
                data.playerPos = next;
            }
        }
    }
}
#endregion

