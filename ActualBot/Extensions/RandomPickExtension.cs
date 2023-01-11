using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualBot.Extensions
{
    public static class RandomPickExtension
    {
        public static T PickRandom<T>(List<T> list)
        {
            var random = new Random();
            var choiceOfAlgo = random.Next(2);
            if (choiceOfAlgo == 0)
            {
                var choiceOfElement = random.Next(list.Count());
                var chosenElement = list[choiceOfElement];
                return chosenElement;
            }
            else
            {
                var sortedList = list.OrderBy(x => random.Next());
                var chosenElement = sortedList.First();
                return chosenElement;
            }
        }
    }
}
