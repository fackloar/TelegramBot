using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualBot.Info
{
    public static class Messages
    {
        public const string Hello = "Привет! Чтобы увидеть список команд, введи /help.";
        public const string Registered = "Отлично! Ты зарегестрирован в игре \"Котик дня\"";
        public const string AlreadyOn = "Ой! Игра уже включена...";
        public const string Help = "Привет! Чтобы зарегистрироваться в игре, напиши /register.\n" +
                                "Когда все желающие пройдут регистрацию, можно запустить игру набрав /gameOn\n" +
                                "Чтобы я напомнил, кто сегодня победил - /winner\n" +
                                "Для того, чтобы вы сыграли в секретного Санту, и я выбрал, кто кому должен будет что-то подарить - /secretSanta\n" +
                                "ВНИМАНИЕ: Чтобы я правильно распределил секретных Сант, каждый участник должен написать мне в лс /start! Иначе не сработает:(";
        public const string GamesOff = "Никто не выиграет, пока не начнёте играть... Для запуска игры напишите /gameOn";
        public const string SecretSantaSticker = "CAACAgIAAxkBAAEaT7tjf-2UlQR748jqPTARgQABpHXVwKgAAmYSAAIrEOhJ3To7U-US8LYrBA";
        public const string OfTheDaySticker = "CAACAgIAAxkBAAEGoTZjh_nzdC3bVYIfg5avIB8ghx70IwACNxUAAneYoUuKCzy_q8x4eCsE";
        public static string? Winner { get; set; }
    }
}
