using Quartz;
using TelegramBot.Bot;

namespace ActualBot
{
    public class ScheduleJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.MergedJobDataMap;

            var commandOperator = (CommandOperator)dataMap["commandOperator"];

            await commandOperator.EverydayJob();

        }
    }
}
