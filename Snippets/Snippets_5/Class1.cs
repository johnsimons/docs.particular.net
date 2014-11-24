namespace Snippets_4
{
    using NServiceBus;

    class Class1
    {
        #region selfhostingconfig 5.0
        static void Main()
        {
            var config = new BusConfiguration();
            config.UsePersistence<InMemoryPersistence>();

            using (var bus = Bus.Create(config))
            {
                bus.Start();
            }
        }
        #endregion
    }
}
