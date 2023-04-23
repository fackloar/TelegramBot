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
        public const string NotRegistered = "Что-то тут не так... Похоже, ты уже регистрировался.";
        public const string AlreadyOn = "Ой! Игра уже включена...";
        public const string Help = "Привет! Чтобы зарегистрироваться в игре, напиши /register.\n" +
                                "Когда все желающие пройдут регистрацию, можно запустить игру набрав /gameOn\n" +
                                "Чтобы я напомнил, кто сегодня победил - /winner\n" +
                                "Также я умею считать карму. По умолчанию счетчик кармы у каждого выключен\n" +
                                "Чтобы включить счетчик, напиши /karmaSwitch\n" +
                                "Для того, чтобы поднять карму человеку, ответь на его сообщение +\n" +
                                "Для того, чтобы понизить карму, ответь на его сообщение -\n" +
                                "Если ты вдруг захочешь выключить свой счетчик, ты можешь это сделать. Просто напиши /karmaSwitch ещё раз\n" +
                                "Учти, что в таком случае, если у тебя была положительная карма, то она обнулится. А если отрицательная, то останется как есть.\n";
        public const string GamesOff = "Никто не выиграет, пока не начнёте играть... Для запуска игры напишите /gameOn";
        public const string DontCheat = "Извини, но самому себе карму поменять не получится.";
        public const string NoChat = "Похоже, я еще не запомнил ваш чат. Напишите /start.";
        public const string NoUser = "Похоже, я еще не запомнил такого пользователя. Напиши /register.";
        public const string Error = "Что-то тут не так...";
        public const string KarmaOff = "У этого пользователя выключен счетчик кармы ;)";
        public const string DontCheatKarmaSwitch = "Извини, но нельзя трогать чужую карму, если твой счетчик отключен ;)";
    }
}
