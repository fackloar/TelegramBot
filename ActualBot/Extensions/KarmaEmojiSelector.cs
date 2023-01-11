using ActualBot.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ActualBot.Extensions
{
    public static class KarmaEmojiSelector
    {
        public static string EmojiString(int karma)
        {
            string karmaEmoji;
            switch (karma)
            {
                case <= (int)KarmaMilestones.Devil:
                    karmaEmoji = char.ConvertFromUtf32(0x1F608);
                    return karmaEmoji;
                case <= (int)KarmaMilestones.Atrocious and >= (int)KarmaMilestones.Devil:
                    karmaEmoji = char.ConvertFromUtf32(0x1F47A);
                    return karmaEmoji;
                case <= (int)KarmaMilestones.Wrong and >= (int)KarmaMilestones.Atrocious:
                    karmaEmoji = char.ConvertFromUtf32(0x1F621);
                    return karmaEmoji;
                case <= (int)KarmaMilestones.Awful and >= (int)KarmaMilestones.Wrong:
                    karmaEmoji = char.ConvertFromUtf32(0x1F616);
                    return karmaEmoji;
                case <= (int)KarmaMilestones.Bad and >= (int)KarmaMilestones.Awful:
                    karmaEmoji = char.ConvertFromUtf32(0x1F613);
                    return karmaEmoji;
                case < 0 and >= (int)KarmaMilestones.Bad:
                    karmaEmoji = char.ConvertFromUtf32(0x1F614);
                    return karmaEmoji;
                case 0:
                    karmaEmoji = char.ConvertFromUtf32(0x1F611);
                    return karmaEmoji;
                case > 0 and <= (int)KarmaMilestones.Good:
                    karmaEmoji = char.ConvertFromUtf32(0x1F604);
                    return karmaEmoji;
                case >= (int)KarmaMilestones.Good and <= (int)KarmaMilestones.Great:
                    karmaEmoji = char.ConvertFromUtf32(0x1F60C);
                    return karmaEmoji;
                case >= (int)KarmaMilestones.Great and <= (int)KarmaMilestones.Wow:
                    karmaEmoji = char.ConvertFromUtf32(0x1F60E);
                    return karmaEmoji;
                case >= (int)KarmaMilestones.Wow and <= (int)KarmaMilestones.Amazing:
                    karmaEmoji = char.ConvertFromUtf32(0x1F600);
                    return karmaEmoji;
                case >= (int)KarmaMilestones.Amazing and <= (int)KarmaMilestones.Godlike:
                    karmaEmoji = char.ConvertFromUtf32(0x1F47C);
                    return karmaEmoji;
                case >= (int)KarmaMilestones.Godlike:
                    karmaEmoji = char.ConvertFromUtf32(0x1F451);
                    return karmaEmoji;
                default:
                    return null;
            }
        }
    }
}
