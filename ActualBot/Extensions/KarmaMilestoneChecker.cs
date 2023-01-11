using ActualBot.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualBot.Extensions
{
    public static class KarmaMilestoneChecker
    {
        public static string Check(int karma)
        {
            switch (karma)
            {
                case (int)KarmaMilestones.Good:
                    return "Ничего себе, уже 25 кармы!";
                case (int)KarmaMilestones.Great:
                    return "Отлично, 50 кармы! Похоже, ты хороший человек";
                case (int)KarmaMilestones.Wow:
                    return "Ого! 100 кармы! Ты лучший друг для всех вокруг";
                case (int)KarmaMilestones.Amazing:
                    return "Безумие. 250 кармы! Далай Лама завидует тебе.";
                case (int)KarmaMilestones.Godlike:
                    return "В это невозмножно поверить. 500 кармы! Ты самый лучший человек в мире.";
                case (int)KarmaMilestones.Bad:
                    return "У тебя уже -25 кармы. Пора задуматься о своём поведении.";
                case (int)KarmaMilestones.Awful:
                    return "У тебя -50 кармы. Тучи сгущаются.";
                case (int)KarmaMilestones.Wrong:
                    return "-100. Полиция кармы уже выехала.";
                case (int)KarmaMilestones.Atrocious:
                    return "Серьзеные bad boy vibes с -250 кармы";
                case (int)KarmaMilestones.Devil:
                    return "Ты дьявол во плоти! -500 кармы.";
                default:
                    return null;
            }
        }
    }
}
