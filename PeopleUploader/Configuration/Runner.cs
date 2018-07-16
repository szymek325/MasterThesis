using Microsoft.Extensions.Logging;

namespace PeopleUploader.Configuration
{
    public class Runner : IRunner
    {
        private readonly ILogger<Runner> logger;

        public Runner(ILogger<Runner> logger)
        {
            this.logger = logger;
        }

        public void DoAction(string name)
        {
            logger.LogDebug(20, "Doing hard work! {Action}", name);
        }
    }
}