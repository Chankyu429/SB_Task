# 7월 21일 수정

```csharp
using System;

namespace TextBasedGame
{
    public abstract class Player
    {
        public string Name { get; }
        public string Job { get; protected set; }
        public int Level { get; protected set; }
        public float Hp { get; set; }
        public float Mp { get; set; }
        public float AttackPower { get; set; }
        public float Defnce { get; set; }

        public Player(string name, float hp, float mp, float power, float defnce)
        {
            Name = name;
            Hp = hp;
            Mp = mp;
            AttackPower = power;
            Defnce = defnce;
            Level = 1;
            Job = "없음";
        }

        public abstract void Attack();

        public void ShowStatus()
        {
            Console.WriteLine("=========================================");
            Console.WriteLine($"{Name}의 상태창");
            Console.WriteLine("=========================================");
            if (Job != "없음")
            {
                Console.WriteLine($"직업 : {Job}, Lv.{Level}");
            }
            Console.WriteLine($"HP : {Hp}, MP : {Mp}, 공격력 : {AttackPower}, 방어력 : {Defnce}");
            Console.WriteLine("=========================================");
        }
    }

    public class Warrior : Player
    {
        public Warrior(string name, float hp, float mp, float power, float defnce) : base(name, hp, mp, power, defnce) { Job = "전사"; }
        public override void Attack() { Console.WriteLine("검으로 공격!!"); }
    }

    public class Mage : Player
    {
        public Mage(string name, float hp, float mp, float power, float defnce) : base(name, hp, mp, power, defnce) { Job = "마법사"; }
        public override void Attack() { Console.WriteLine("마법으로 공격!!"); }
    }

    public class Thief : Player
    {
        public Thief(string name, float hp, float mp, float power, float defnce) : base(name, hp, mp, power, defnce) { Job = "도적"; }
        public override void Attack() { Console.WriteLine("단검으로 공격!!"); }
    }

    public class Archer : Player
    {
        public Archer(string name, float hp, float mp, float power, float defnce) : base(name, hp, mp, power, defnce) { Job = "궁수"; }
        public override void Attack() { Console.WriteLine("활로 공격!!"); }
    }

    public class Novice : Player
    {
        public Novice(string name, float hp, float mp, float power, float defnce) : base(name, hp, mp, power, defnce) { Job = "없음"; }
        public override void Attack() { Console.WriteLine("맨손으로 공격!!"); }
    }

    public class Monster
    {
        public string Name { get; set; }
        public float Hp { get; set; }
        public float Power { get; set; }
        public float Defnce { get; set; }
        public string DropItemName { get; set; }
        public float DropItemRatio { get; set; }

        public Monster(string name, float hp, float power, float defnce, string dropItemName, float dropItemRatio)
        {
            Name = name;
            Hp = hp;
            Power = power;
            Defnce = defnce;
            DropItemName = dropItemName;
            DropItemRatio = dropItemRatio;
        }

        public void Attack(Player player)
        {
            float damage = Power - player.Defnce;
            if (damage < 0) damage = 0; //데미지가 음수가 되는 것을 방지

            player.Hp -= damage;
            Console.WriteLine($"{Name}이(가) {player.Name}에게 {damage} 데미지!");

            if (player.Hp <= 0)
            {
                player.Hp = 0;
                Console.WriteLine($"{player.Name} 사망!!");
            }
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
            float finalHp = 0, finalMp = 0, finalAtk = 0, finalDefnce = 0;

            while (true)
            {
                Console.Write("플레이어 이름 : ");
                inputName = Console.ReadLine()!;
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
                string input = Console.ReadLine()!;
                string[] parts = input.Split(',');
                if (parts.Length < 2) { Console.WriteLine("올바른 형식으로 입력해주세요. 예) 100, 50"); continue; }

                if (float.TryParse(parts[0].Trim(), out float inputHp) && float.TryParse(parts[1].Trim(), out float inputMp))
                {
                    if (inputHp < 60 || inputMp < 40) { Console.WriteLine("최소 HP는 60, 최소 MP는 40입니다. 다시 입력해주세요."); continue; }
                    finalHp = inputHp; finalMp = inputMp;
                    break;
                }
                else Console.WriteLine("숫자 형식으로 입력해주세요.");
            }

            while (true)
            {
                Console.Write("공격력, 방어력 : ");
                string input = Console.ReadLine()!;
                string[] parts = input.Split(',');
                if (parts.Length < 2) { Console.WriteLine("올바른 형식으로 입력해주세요. 예) 20, 5"); continue; }

                if (float.TryParse(parts[0].Trim(), out float inputAtk) && float.TryParse(parts[1].Trim(), out float inputDefnce))
                {
                    if (inputAtk < 15 || inputDefnce < 5) { Console.WriteLine("최소 공격력은 15, 최소 방어력은 5입니다. 다시 입력해주세요."); continue; }
                    finalAtk = inputAtk; finalDefnce = inputDefnce;
                    break;
                }
                else Console.WriteLine("숫자 형식으로 입력해주세요.");
            }

            Player player = new Novice(inputName, finalHp, finalMp, finalAtk, finalDefnce);
            player.ShowStatus();

            //상태 관리 및 강화 메뉴
            int hpPotions = 3, mpPotions = 3, atkCoupons = 2, defCoupons = 3;
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

                string choice = Console.ReadLine()!;
                switch (choice)
                {
                    case "1":
                        if (hpPotions > 0) { player.Hp += 20; hpPotions--; Console.WriteLine($"** HP가 20 증가했습니다. (남은 포션 {hpPotions}개)"); }
                        else Console.WriteLine("** 포션이 부족합니다.");
                        break;
                    case "2":
                        if (mpPotions > 0) { player.Mp += 20; mpPotions--; Console.WriteLine($"** MP가 20 증가했습니다. (남은 포션 {mpPotions}개)"); }
                        else Console.WriteLine("** 포션이 부족합니다.");
                        break;
                    case "3":
                        if (atkCoupons > 0) { player.AttackPower *= 2; atkCoupons--; Console.WriteLine($"** 공격력이 2배 증가했습니다. (남은 쿠폰 {atkCoupons}개)"); }
                        else Console.WriteLine("** 쿠폰이 부족합니다.");
                        break;
                    case "4":
                        if (defCoupons > 0) { player.Defnce *= 2; defCoupons--; Console.WriteLine($"** 방어력이 2배 증가했습니다. (남은 쿠폰 {defCoupons}개)"); }
                        else Console.WriteLine("** 쿠폰이 부족합니다.");
                        break;
                    case "5": player.ShowStatus(); break;
                    case "0":
                        bGameStart = true;
                        Console.WriteLine("\n*****************************************");
                        Console.WriteLine("Game Start!!!!");
                        Console.WriteLine("*****************************************");
                        break;
                    default: Console.WriteLine("올바른 번호를 선택해주세요."); break;
                }
            }

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

                string jobChoice = Console.ReadLine()!;

                if (jobChoice == "1")
                    player = new Warrior(player.Name, player.Hp, player.Mp, player.AttackPower, player.Defnce);
                else if (jobChoice == "2")
                    player = new Mage(player.Name, player.Hp, player.Mp, player.AttackPower, player.Defnce);
                else if (jobChoice == "3")
                    player = new Thief(player.Name, player.Hp, player.Mp, player.AttackPower, player.Defnce);
                else if (jobChoice == "4")
                    player = new Archer(player.Name, player.Hp, player.Mp, player.AttackPower, player.Defnce);
                else
                {
                    Console.WriteLine("올바른 번호를 선택해주세요.");
                    continue;
                }

                Console.WriteLine($"* [{player.Job}]로 전직했습니다.");
                break;
            }

            //전직 완료 후 상태창
            player.ShowStatus();

            //전투 준비 단계 (몬스터 스폰 테스트)
            Console.WriteLine("\n<전투 시작>");

            Monster oak = new Monster("오크", 50, 20, 5, "HP Up 쿠폰", 0.5f);

            Console.WriteLine($"* 전방에 [{oak.Name}](이)가 나타났습니다! (HP: {oak.Hp})");

            oak.Attack(player);

            player.ShowStatus();
        }
    }
}
```
