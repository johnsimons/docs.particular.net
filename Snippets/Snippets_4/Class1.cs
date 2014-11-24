namespace Snippets_4
{
    using NServiceBus;

    class Class1
    {
        #region selfhostingconfig 4.0
        static void Main()
        {
            Configure.Serialization.Xml();

            Configure.Transactions.Enable();

            Configure.With()
            .Log4Net()
            .DefaultBuilder()
            .UseTransport<Msmq>()
            .PurgeOnStartup(false)
            .InMemorySubscriptionStorage()
            .UnicastBus()
            .RunHandlersUnderIncomingPrincipal(false)
            .LoadMessageHandlers()
            .UseRavenTimeoutPersister()
            .CreateBus()
            .Start(() => Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());
        }
        #endregion
    }
}
