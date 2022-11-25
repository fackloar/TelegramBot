using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Bot;

namespace ActualBot
{
    internal class ScheduleJob : IJob
    {
        private CommandOperator _commandOperator;

        public ScheduleJob(CommandOperator commandOperator)
        {
            _commandOperator = commandOperator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _commandOperator.OfTheDay();
        }
    }
}
