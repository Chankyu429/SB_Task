# 진행 상황

```csharp
using System;

//부모 class
class Player
{
    public string name;
    public float hp;
    public float mp;
    public float attack;
    public float defence;

    public string job;
    public int level;

    public Player(string name, float hp, float mp, float attack, float defence)
    {
        this.name = name;
        this.hp = hp;
        this.mp = mp;
        this.attack = attack;
        this.defence = defence;
        this.job = "초보자";
        this.level = 1;
    }

    public virtual void Attack()
    {
        Console.WriteLine("기본 공격!");
    }

    //상태창 + 꾸밈
    public void ShowStatus()
    {
        Console.WriteLine("\n◈━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━◈");
        Console.WriteLine("             [ " + name + "의 상태창 ]");
        Console.WriteLine("-----------------------------------------");

        if (job != "초보자")
        {
            Console.WriteLine("  ▶ 직업 : " + job + " (Lv." + level + ")");
        }

        Console.WriteLine("  ▶ HP : " + hp + " / MP : " + mp);
        Console.WriteLine("  ▶ 공격력 : " + attack + " / 방어력 : " + defence);
        Console.WriteLine("◈━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━◈\n");
    }
}

class Novice : Player
{
    public Novice(string name, float hp, float mp, float attack, float defence)
        : base(name, hp, mp, attack, defence)
    {

    }

    public override void Attack()
    {
        Console.WriteLine("맨손 공격!");
    }
}

//직업별 자식 class (상속 및 오버라이딩 사용함)
class Warrior : Player
{
    public Warrior(string name, float hp, float mp, float attack, float defence)
        : base(name, hp, mp, attack, defence)
    {
        this.job = "전사";
    }

    public override void Attack()
    {
        Console.WriteLine("전사의 강력한 베기!");
    }
}

class Mage : Player
{
    public Mage(string name, float hp, float mp, float attack, float defence)
        : base(name, hp, mp, attack, defence)
    {
        this.job = "마법사";
    }

    public override void Attack()
    {
        Console.WriteLine("마법사의 파이어볼!");
    }
}

class Thief : Player
{
    public Thief(string name, float hp, float mp, float attack, float defence)
        : base(name, hp, mp, attack, defence)
    {
        this.job = "도적";
    }

    public override void Attack()
    {
        Console.WriteLine("도적의 암살 공격!");
    }
}

class Archer : Player
{
    public Archer(string name, float hp, float mp, float attack, float defence)
        : base(name, hp, mp, attack, defence)
    {
        this.job = "궁수";
    }

    public override void Attack()
    {
        Console.WriteLine("궁수의 정밀한 화살 쏘기!");
    }
}

// 몬스터 부모 class
class Monster
{
    public string name;
    public float hp;
    public float power;
    public float defence;
    public string dropItemName;
    public float dropItemRatio;

    public Monster(string name, float hp, float power, float defence, string dropItemName, float dropItemRatio)
    {
        this.name = name;
        this.hp = hp;
        this.power = power;
        this.defence = defence;
        this.dropItemName = dropItemName;
        this.dropItemRatio = dropItemRatio;
    }

    //몬스터가 플레이어를 공격
    public void AttackPlayer(Player player)
    {
        float damage = this.power - player.defence;
        if (damage < 1) damage = 1; //최소 데미지

        player.hp = player.hp - damage;
        Console.WriteLine(name + "의 공격! " + player.name + "에게 " + damage + " 데미지!");
    }
}

//오크 class
class Oak : Monster
{
    public Oak() : base("오크", 30, 15, 2, "HP Up 쿠폰", 50.0f)
    {
    }
}

//Main Program class
class Program
{
    static void Main()
    {
        //이모지 깨짐 방지
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string inputName = "";
        float inputHp = 0, inputMp = 0, inputAttack = 0, inputDefence = 0;

        string select = "";

        //시작화면
        Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
        Console.WriteLine("■                                         ■");
        Console.WriteLine("■           캐릭터 생성을 시작합니다          ■");
        Console.WriteLine("■                                         ■");
        Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");

        while (true)
        {
            Console.Write(" ▶ 플레이어 이름 : ");
            inputName = Console.ReadLine()!;

            if (inputName.Length <= 3)
            {
                Console.WriteLine("  [경고] 이름이 너무 짧아요. 다시 입력해 주세요.\n");
                continue;
            }
            break;
        }

        while (true)
        {
            Console.Write(" ▶ HP, MP (예: 100,50) : ");
            string[] hpMpInput = Console.ReadLine()!.Split(',');

            inputHp = float.Parse(hpMpInput[0]);
            inputMp = float.Parse(hpMpInput[1]);

            if (inputHp < 60 || inputMp < 40)
            {
                Console.WriteLine("  [경고] 최소 HP는 60, 최소 MP는 40입니다. 다시 입력해주세요.\n");
                continue;
            }
            break;
        }

        while (true)
        {
            Console.Write(" ▶ 공격력, 방어력 (예: 20,4) : ");
            string[] atkDefInput = Console.ReadLine()!.Split(',');

            inputAttack = float.Parse(atkDefInput[0]);
            inputDefence = float.Parse(atkDefInput[1]);

            if (inputAttack < 15 || inputDefence < 5)
            {
                Console.WriteLine("  [경고] 최소 공격력은 15, 최소 방어력은 5입니다. 다시 입력해주세요.\n");
                continue;
            }
            break;
        }

        Player player = new Novice(inputName, inputHp, inputMp, inputAttack, inputDefence);
        player.ShowStatus();

        int hpPotion = 3;
        int mpPotion = 3;
        int atkCoupon = 2;
        int defCoupon = 3;
        bool bGameStart = false;

        Console.WriteLine(" 🎁 [시스템] HP 포션 " + hpPotion + "개, MP 포션 " + mpPotion + "개를 지급했습니다.");
        Console.WriteLine(" 🎁 [시스템] 공격력 Up 쿠폰 " + atkCoupon + "개, 방어력 Up 쿠폰 " + defCoupon + "개를 지급했습니다.\n");

        while (bGameStart == false)
        {
            //메뉴창 꾸며봄
            Console.WriteLine("◈━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━◈");
            Console.WriteLine("             < " + player.name + " 강화 메뉴 >");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("  [1] HP Up ");
            Console.WriteLine("  [2] MP Up");
            Console.WriteLine("  [3] 공격력 2배");
            Console.WriteLine("  [4] 방어력 2배");
            Console.WriteLine("  [5] 능력치 보기");
            Console.WriteLine("  [0] 게임 시작");
            Console.WriteLine("◈━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━◈");
            Console.Write(" ☞ 번호를 입력하세요 : ");

            select = Console.ReadLine()!;

            if (select == "1")
            {
                if (hpPotion > 0)
                {
                    player.hp = player.hp + 20;
                    hpPotion = hpPotion - 1;
                    Console.WriteLine("\n ✨ HP가 20 증가했습니다! (남은 포션 " + hpPotion + "개)\n");
                }
                else
                {
                    Console.WriteLine("\n ❌ 포션이 부족합니다.\n");
                }
            }
            else if (select == "2")
            {
                if (mpPotion > 0)
                {
                    player.mp = player.mp + 20;
                    mpPotion = mpPotion - 1;
                    Console.WriteLine("\n ✨ MP가 20 증가했습니다! (남은 포션 " + mpPotion + "개)\n");
                }
                else
                {
                    Console.WriteLine("\n ❌ 포션이 부족합니다.\n");
                }
            }
            else if (select == "3")
            {
                if (atkCoupon > 0)
                {
                    player.attack = player.attack * 2;
                    atkCoupon = atkCoupon - 1;
                    Console.WriteLine("\n ⚔️ 공격력이 2배 증가했습니다! (남은 쿠폰 " + atkCoupon + "개)\n");
                }
                else
                {
                    Console.WriteLine("\n ❌ 쿠폰이 부족합니다.\n");
                }
            }
            else if (select == "4")
            {
                if (defCoupon > 0)
                {
                    player.defence = player.defence * 2;
                    defCoupon = defCoupon - 1;
                    Console.WriteLine("\n 🛡️ 방어력이 2배 증가했습니다! (남은 쿠폰 " + defCoupon + "개)\n");
                }
                else
                {
                    Console.WriteLine("\n ❌ 쿠폰이 부족합니다.\n");
                }
            }
            else if (select == "5")
            {
                player.ShowStatus();
            }
            else if (select == "0")
            {
                bGameStart = true;
            }
            else
            {
                Console.WriteLine("\n ⚠️ 잘못된 입력입니다. 다시 선택해주세요.\n");
            }
        }

        //특수문자(?) 사이트가서 찾아옴
        Console.WriteLine("\n★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★");
        Console.WriteLine("★                                     ★");
        Console.WriteLine("★          GAME START!!!!             ★");
        Console.WriteLine("★                                     ★");
        Console.WriteLine("★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★\n");

        Console.WriteLine("◈━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━◈");
        Console.WriteLine("          < " + player.name + " 직업 선택 >");
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine("  [1] 전사");
        Console.WriteLine("  [2] 마법사");
        Console.WriteLine("  [3] 도적");
        Console.WriteLine("  [4] 궁수");
        Console.WriteLine("◈━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━◈");
        Console.Write(" ☞ 번호를 입력하세요 : ");

        select = Console.ReadLine()!;
        Console.WriteLine();

        //Win + . 누르면 이모지 추가 
        if (select == "1")
        {
            player = new Warrior(player.name, player.hp, player.mp, player.attack, player.defence);
            Console.WriteLine(" ⚔️ [" + player.job + "]로 전직했습니다!");
        }
        else if (select == "2")
        {
            player = new Mage(player.name, player.hp, player.mp, player.attack, player.defence);
            Console.WriteLine(" 🔮 [" + player.job + "]로 전직했습니다!");
        }
        else if (select == "3")
        {
            player = new Thief(player.name, player.hp, player.mp, player.attack, player.defence);
            Console.WriteLine(" 🗡️ [" + player.job + "]로 전직했습니다!");
        }
        else if (select == "4")
        {
            player = new Archer(player.name, player.hp, player.mp, player.attack, player.defence);
            Console.WriteLine(" 🏹 [" + player.job + "]로 전직했습니다!");
        }
        else
        {
            Console.WriteLine(" ⚠️ 잘못된 선택입니다. 기본 상태를 유지합니다.");
        }

        player.ShowStatus();

        //몬스터 생성 및 전투
        Monster monster = new Oak();
        int inventoryHpCoupon = 0; //보상으로 얻을 쿠폰 개수 카운트

        Console.WriteLine("\n<전투 시작>");

        while (true)
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("<" + player.name + "> 턴 시작");
            Console.WriteLine("=========================================");

            // 플레이어 공격
            player.Attack();
            float playerDamage = player.attack - monster.defence;
            if (playerDamage < 1) playerDamage = 1;

            float prevMonsterHp = monster.hp;
            monster.hp = monster.hp - playerDamage;
            if (monster.hp < 0) monster.hp = 0;

            Console.WriteLine(monster.name + "에게 " + playerDamage + " 데미지!");
            Console.WriteLine(monster.name + " HP : " + prevMonsterHp + " -> " + monster.hp);

            //몬스터 사망 시
            if (monster.hp <= 0)
            {
                Console.WriteLine(monster.name + " 사망!!");
                Console.WriteLine("=========================================");
                Console.WriteLine("\n** 전투 승리!");
                inventoryHpCoupon++;
                Console.WriteLine(" => \"" + monster.dropItemName + "\" 획득");
                Console.WriteLine(" => (" + monster.dropItemName + " +1 => 남은 쿠폰 " + inventoryHpCoupon + "개)");
                Console.WriteLine(" => 다음 단계에서 인벤토리에 저장");
                break;
            }

            //몬스터 반격 턴
            Console.WriteLine("\n-----------------------------------------");
            monster.AttackPlayer(player);
            Console.WriteLine(player.name + " 남은 HP : " + player.hp);
            Console.WriteLine("-----------------------------------------");

            //플레이어 사망 시
            if (player.hp <= 0)
            {
                Console.WriteLine("\n❌ 플레이어가 사망했습니다... 게임 패배!");
                break;
            }

            Console.WriteLine("\n다음 턴으로 넘어가려면 엔터를 누르세요...");
            Console.ReadLine();
        }
    }
}
```
