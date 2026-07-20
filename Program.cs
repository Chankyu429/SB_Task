using System;

namespace TextBasedGame
{
    public class Player
    {
        public string Name { get; }
        public string Job { get; set; }
        public int Level { get; set; }
        public float Hp { get; set; }
        public float Mp { get; set; }
        public float Attack { get; set; }
        public float Deffence { get; set; }

        public Player(string name, float hp, float mp, float power, float defence)
        {
            Name = name;
            Hp = hp;
            Mp = mp;
            Attack = power;
            Deffence = defence;
            Job = "없음"; 
            Level = 1;    
        }

        public void ShowStatus()
        {
            Console.WriteLine("=========================================");
            Console.WriteLine($"{Name}의 상태창");
            Console.WriteLine("=========================================");
            if (Job != "없음")
            {
                Console.WriteLine($"직업 : {Job}, Lv.{Level}");
            }
            Console.WriteLine($"HP : {Hp}, MP : {Mp}, 공격력 : {Attack}, 방어력 : {Deffence}");
            Console.WriteLine("=========================================");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("플레이어 데이터 입력");
            Console.WriteLine("=========================================");

            string inputName = "";
            float finalHp = 0;
            float finalMp = 0;
            float finalAtk = 0;
            float finalDef = 0;

            while (true)
            {
                Console.Write("플레이어 이름 : ");
                inputName = Console.ReadLine();

                if (inputName.Length <= 3)
                {
                    Console.WriteLine("플레이어 이름이 너무 짧아요. 다시 입력해 주세요. (4글자 이상)");
                    continue;
                }
                break;
            }

            while (true)
            {
                Console.Write("HP, MP : ");
                string input = Console.ReadLine();

                string[] parts = input.Split(',');
                if (parts.Length < 2)
                {
                    Console.WriteLine("올바른 형식으로 입력해주세요. 예) 100, 50");
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

                    finalHp = inputHp;
                    finalMp = inputMp;
                    break;
                }
                else
                {
                    Console.WriteLine("숫자 형식으로 입력해주세요.");
                }
            }

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

                    finalAtk = inputAtk;
                    finalDef = inputDef;
                    break;
                }
                else
                {
                    Console.WriteLine("숫자 형식으로 입력해주세요.");
                }
            }

            Player player = new Player(inputName, finalHp, finalMp, finalAtk, finalDef);

            player.ShowStatus();

            int hpPotions = 3;
            int mpPotions = 3;
            int atkCoupons = 2;
            int defCoupons = 3;

            Console.WriteLine($"* HP 포션 {hpPotions}개, MP 포션 {mpPotions}개를 지급했습니다.");
            Console.WriteLine($"* 공격력 Up 쿠폰 {atkCoupons}개, 방어력 Up 쿠폰 {defCoupons}개를 지급했습니다.");

            bool bGameStart = false;

            while (!bGameStart)
            {
                Console.WriteLine("\n=========================================");
                Console.WriteLine($"< {player.Name} 강화 >");
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
                            player.Hp += 20;
                            hpPotions--;
                            Console.WriteLine($"** HP가 20 증가했습니다. (HP 포션 -1 => 남은 포션 {hpPotions}개)");
                        }
                        else Console.WriteLine("** 포션이 부족합니다.");
                        break;

                    case "2":
                        if (mpPotions > 0)
                        {
                            player.Mp += 20;
                            mpPotions--;
                            Console.WriteLine($"** MP가 20 증가했습니다. (MP 포션 -1 => 남은 포션 {mpPotions}개)");
                        }
                        else Console.WriteLine("** 포션이 부족합니다.");
                        break;

                    case "3":
                        if (atkCoupons > 0)
                        {
                            player.Attack *= 2;
                            atkCoupons--;
                            Console.WriteLine($"** 공격력이 2배 증가했습니다. (공격력 Up 쿠폰 -1 => 남은 쿠폰 {atkCoupons}개)");
                        }
                        else Console.WriteLine("** 쿠폰이 부족합니다.");
                        break;

                    case "4":
                        if (defCoupons > 0)
                        {
                            player.Deffence *= 2;
                            defCoupons--;
                            Console.WriteLine($"** 방어력이 2배 증가했습니다. (방어력 Up 쿠폰 -1 => 남은 쿠폰 {defCoupons}개)");
                        }
                        else Console.WriteLine("** 쿠폰이 부족합니다.");
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

            //새 항목 직업 선택 메뉴
            while (true)
            {
                Console.WriteLine("\n<직업 선택>");
                Console.WriteLine("=========================================");
                Console.WriteLine($"< {player.Name} 직업 >");
                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 마법사");
                Console.WriteLine("3. 도적");
                Console.WriteLine("4. 궁수");
                Console.WriteLine("=========================================");
                Console.Write("메뉴를 선택하세요 : ");

                string jobChoice = Console.ReadLine();
                string selectedJob = "";

                if (jobChoice == "1") selectedJob = "전사";
                else if (jobChoice == "2") selectedJob = "마법사";
                else if (jobChoice == "3") selectedJob = "도적";
                else if (jobChoice == "4") selectedJob = "궁수";
                else
                {
                    Console.WriteLine("올바른 번호를 선택해주세요.");
                    continue;
                }

                player.Job = selectedJob;
                Console.WriteLine($"* [{selectedJob}]로 전직했습니다.");
                break;
            }

            //전직 완료 후 최종 상태창 출력
            player.ShowStatus();
        }//Main
    }
}
