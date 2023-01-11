using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualBot.Info
{
    public class CatStickers
    {
        private List<string> _catStickersList = new List<string>
        {
            "CAACAgIAAxkBAAEGoTZjh_nzdC3bVYIfg5avIB8ghx70IwACNxUAAneYoUuKCzy_q8x4eCsE",
            "CAACAgIAAxkBAAEHM5xjvf-n1BIK2moikgv_vPqsVoRAHwACrxwAAha0UUkuw5cNrzAnwi0E",
            "CAACAgIAAxkBAAEHM51jvf-ne_iILJ7B1GTyMGEES9NwRQACuhQAApY7cUi8yJ25-cHqJS0E",
            "CAACAgIAAxkBAAEHM55jvf-ni97YMfnacN2sovGY-HlFxAACCgADkp8eEYoo_BiL_vEXLQQ",
            "CAACAgIAAxkBAAEHM59jvf-nsGFXDMHsKRTP_2ZaBUQyKwACCk0AAgeZwQvsmLu_stXpUy0E",
            "CAACAgIAAxkBAAEHM6Bjvf-n6moajUKm9ZhJ_PugF0BragACDAEAAhZ8aAPEXuoz0922Fy0E",
            "CAACAgIAAxkBAAEHM6Fjvf-n4IV9TcbT9pRwBp1YmsD9ngACjQADg6PsFQ5wFZQQCM0RLQQ",
            "CAACAgIAAxkBAAEHM6Jjvf-nzYXROB6--7pGU1xCG04AAdsAAowAA4Oj7BVWNGmi0HmfNi0E",
            "CAACAgQAAxkBAAEHM6Njvf-neIUDDeDlEiMHB9UgfN2DwgACSQ8AAqbxcR5mS9271MsMTi0E",
            "CAACAgIAAxkBAAEHM6Rjvf-nccyGfk5cMyvWiqBvbGgKfwACgQADNyA6CjNydNTcNCiaLQQ",
            "CAACAgIAAxkBAAEHM65jvf_G0kadF8fr5iUm8AcDUZmnRAACkAADg6PsFY8LD-EgKpdvLQQ",
            "CAACAgIAAxkBAAEHM7Bjvf_PzKWlQGSZ8_fkt1JZaMawzgACQAEAAhZ8aAPOt9pjb9XRXS0E",
            "CAACAgIAAxkBAAEHM7Jjvf_YuzubymiZfXXVa6OGICNjfAACJgEAAhZ8aAMUxPgKnyop7i0E",
            "CAACAgIAAxkBAAEHM7Rjvf_ls16NTyeqsTvDZRPNZDpL-QACbwEAAhZ8aAOK9nH8d3JcRi0E",
            "CAACAgIAAxkBAAEHM7Zjvf_w1wFKUsgplHlwUoGwjLCR7gACxRQAAuh9oEtj8RFL-uNJxC0E",
            "CAACAgIAAxkBAAEHM7hjvf_3zb7WnswFmyTdyRkwMLJ36wACvxQAAiVc6Etnjm-s-wVsXC0E",
            "CAACAgIAAxkBAAEHM7pjvf_75WijZoZVvDZJ3hDssg5nYgACxRQAAjDF8Us30NZyZQpRXC0E",
            "CAACAgIAAxkBAAEHM7xjvgABBYH18z1pyvPvFRKC_Ix_a6gAApsSAAJHrKFLIYBsEQABdjD2LQQ",
            "CAACAgIAAxkBAAEHM75jvgABExZYlqmaJPsYQmAkp37J2fMAAtIZAAL5D_BKHPxZFX__negtBA",
            "CAACAgIAAxkBAAEHM8BjvgABJ2CZKQRsFqPNHWZBpqdRWpkAAu0RAAJBKslLFLv1AUFMG-EtBA",
            "CAACAgIAAxkBAAEHM9ZjvgeG931n2R1-BEsIU9_UfWDubAACOhQAAofbyUu3hLZoJflrVC0E",
            "CAACAgIAAxkBAAEHM9hjvgeWTeAny1zDcJgeBc1OV8BCbwACvxMAAmfD0Et20NMvC8Nf3S0E",
            "CAACAgIAAxkBAAEHM9pjvgeiHsTDGkvhVyk7j4436yo6zgACvhMAApi06UtReff9es6yUS0E",
            "CAACAgQAAxkBAAEHM9xjvgfAINh5yjKeDZLPthcxFWt01wACDgsAAoEFeFLEkGMLJfkY9S0E",
            "CAACAgQAAxkBAAEHM95jvgfsReoZ5_9gm_Pf83CLN8ZjDQAC2BAAAtbOiFAsZ0Nv4uDUIi0E",
            "CAACAgQAAxkBAAEHM-BjvgfxoXRjC5MxUQy1X497_YF60wAC7AkAAi8seFIEoOJdaKiFjC0E",
            "CAACAgQAAxkBAAEHM-Jjvgf2IqnoluKWZ3j2uN_cnZIQ2gACFQsAAs9JkVNxcaEY4b0gHC0E",
            "CAACAgIAAxkBAAEHM-Rjvggml-i9sxSlloSLyYRxeefXNgACtxAAAs01gUsvjza-mLFSki0E",
            "CAACAgIAAxkBAAEHM-Zjvggv8MC9M4xZpTz_0JjhIDCkOQACEhsAAtujyEg5qB9hVnhSDy0E",
            "CAACAgIAAxkBAAEHM-hjvgg1aFnwZae8cD6n2A6Xc9dWYAACVRkAAmAhYUvQk-wBc7FppC0E",
            "CAACAgIAAxkBAAEHM-pjvgg_J6Z7y6lT3lzBeoPGAAEoF_IAAkYPAAI4yHBJbEzgb3fy11ItBA"
        };

        public List<string> CatStickersList { get { return _catStickersList; } }
    }
}
