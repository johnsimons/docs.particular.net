namespace Snippets_4
{
    using NServiceBus;

    class Class1
    {
        #region selfhostingconfig 3.0
        static void Main()
        {
            Configure.With()
                    .Log4Net()
                    .DefaultBuilder()
                    .XmlSerializer()
                    .MsmqTransport()
                    .IsTransactional(true)
                    .PurgeOnStartup(false)
                    .InMemorySubscriptionStorage()
                    .UnicastBus()
                    .ImpersonateSender(false)
                    .LoadMessageHandlers()
                    .UseRavenTimeoutPersister()
                    .CreateBus()
                    .Start(() => Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());
        }
        #endregion
    }
}
