using System;
using TextGame;

namespace TextGame
{
    public class Player
    {
        public string name;
        public float hp;
        public float mp;
        public float attack;
        public float defence;

        public void ShowStatus()
        {
            Console.WriteLine("=========================================");
            Console.WriteLine($"{name}의 상태창");
            Console.WriteLine("=========================================");
            Console.WriteLine($"HP : {hp}, MP : {mp}, 공격력 : {attack}, 방어력 : {defence}");
        }
    } //Player
} //TextGame

class Program
{
    static void Main(string[] args)
    {
        Player player = new Player();

        Console.WriteLine("=========================================");
        Console.WriteLine($"플레이어 데이터 입력");
        Console.WriteLine("=========================================");

        // ----------------------------------------------------
        // [1단계 & 2단계] 이름 입력 및 검증 (3글자 초과 필수)
        // ----------------------------------------------------
        while(true)
        {
            Console.Write("플레이어 이름 : ");
            string inputName = Console.ReadLine();

            if (inputName.Length <= 3)
            {
                Console.WriteLine("플레이어 이름이 너무 짧아요 다시 입력해주세요. (4글자 이상)");
                continue;
            }

            player.name = inputName;
            break;
        }

        // ----------------------------------------------------
        // [1단계 & 2단계] HP, MP 입력 및 검증 (HP >= 60, MP >= 40)
        // ----------------------------------------------------

        while(true)
        {
            Console.Write("HP, MP : ");
            string input = Console.ReadLine();

            // ","를 기준으로 문자열 분할 후 공백 제거

            string[] parts = input.Split(',');
            if(parts.Length < 2)
            {
                Console.WriteLine("올바른 형식으로 입력해주세요. (예 : 100, 50)");
                continue;
            }

            if (float.TryParse(parts[0].Trim(), out float inputHp) &&
                float.TryParse(parts[1].Trim(), out float inputMp))
            {
                if (inputHp < 60 || inputMp < 40)
                {
                    Console.WriteLine("최소 HP는 60, 최소 MP는 40입니다. 다시 입력해주세요.");
                    continue;
                }

                player.hp = inputHp;
                player.mp = inputMp;
                break;
            }
            else
            {
                Console.WriteLine("숫자 형식으로 입력해주세요.");
            }
        }

        // ----------------------------------------------------
        // [1단계 & 2단계] 공격력, 방어력 입력 및 검증 (공격력 >= 15, 방어력 >= 5)
        // ----------------------------------------------------

        while (true)
        {
            Console.Write("공격력, 방어력 : ");
            string input = Console.ReadLine();

            string[] parts = input.Split(',');
            if (parts.Length < 2)
            {
                Console.WriteLine("올바른 형식으로 입력해주세요. 예) 20, 5");
                continue;
            }

            if (float.TryParse(parts[0].Trim(), out float inputAtk) &&
                float.TryParse(parts[1].Trim(), out float inputDef))
            {
                if (inputAtk < 15 || inputDef < 5)
                {
                    Console.WriteLine("최소 공격력은 15, 최소 방어력은 5입니다. 다시 입력해주세요.");
                    continue;
                }

                player.attack = inputAtk;
                player.defence = inputDef;
                break;
            }
            else
            {
                Console.WriteLine("숫자 형식으로 입력해주세요.");
            }
        }

        // 초기 상태창 출력
        player.ShowStatus();

        // ----------------------------------------------------
        // [3단계] 상태 관리 및 강화 메뉴
        // ----------------------------------------------------
        // 초기 지급 아이템 개수
        int hpPotions = 3;
        int mpPotions = 3;
        int atkCoupons = 2;
        int defCoupons = 3;

        Console.WriteLine($"* HP 포션 {hpPotions}개, MP 포션 {mpPotions}개를 지급했습니다.");
        Console.WriteLine($"* 공격력 Up 쿠폰 {atkCoupons}개, 방어력 Up 쿠폰 {defCoupons}개를 지급했습니다.");

        bool bGameStart = false;

        while(!bGameStart)
        {
            Console.WriteLine("\n=========================================");
            Console.WriteLine($"< {player.name} 강화 >");
            Console.WriteLine("1. HP Up ");
            Console.WriteLine("2. MP Up");
            Console.WriteLine("3. 공격력 2배");
            Console.WriteLine("4. 방어력 2배");
            Console.WriteLine("5. 능력치 보기");
            Console.WriteLine("0. 게임 시작");
            Console.WriteLine("=========================================");
            Console.Write("메뉴를 선택하세요 : ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    if (hpPotions > 0)
                    {
                        player.hp += 20;
                        hpPotions--;
                        Console.WriteLine($"** HP가 20 증가했습니다. (HP 포션 -1 => 남은 포션 {hpPotions}개)");
                    }
                    else
                    {
                        Console.WriteLine("** 포션이 부족합니다.");
                    }
                    break;

                case "2":
                    if (mpPotions > 0)
                    {
                        player.mp += 10;
                        mpPotions--;
                        Console.WriteLine($"** MP가 10 증가했습니다. (MP 포션 -1 => 남은 포션 {mpPotions}개)");
                    }
                    else
                    {
                        Console.WriteLine("** 포션이 부족합니다.");
                    }
                    break;

                case "3":
                    if (atkCoupons > 0)
                    {
                        player.attack *= 2;
                        atkCoupons--;
                        Console.WriteLine($"** 공격력이 2배 증가했습니다. (공격력 Up 쿠폰 -1 => 남은 쿠폰 {atkCoupons}개)");
                    }
                    else
                    {
                        Console.WriteLine("** 쿠폰이 부족합니다.");
                    }
                    break;

                case "4":
                    if (defCoupons > 0)
                    {
                        player.defence *= 2;
                        defCoupons--;
                        Console.WriteLine($"** 방어력이 2배 증가했습니다. (방어력 Up 쿠폰 -1 => 남은 쿠폰 {defCoupons}개)");
                    }
                    else
                    {
                        Console.WriteLine("** 쿠폰이 부족합니다.");
                    }
                    break;

                case "5":
                    player.ShowStatus();
                    break;

                case "0":
                    bGameStart = true;
                    Console.WriteLine("\n*****************************************");
                    Console.WriteLine("Game Start!!!!");
                    Console.WriteLine("*****************************************");
                    break;

                default:
                    Console.WriteLine("올바른 번호를 선택해주세요.");
                    break;
            }
        }
    }
} //Main