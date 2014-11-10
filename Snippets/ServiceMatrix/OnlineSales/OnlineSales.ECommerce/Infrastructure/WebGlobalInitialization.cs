﻿//------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by ServiceMatrix.
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Autofac;
using Autofac.Integration.Mvc;
using NServiceBus;
using NServiceBus.Persistence;
using System.Reflection;
using System.Web.Mvc;
using System.Diagnostics;

namespace OnlineSales.ECommerce.Infrastructure
{
    public static class WebGlobalInitialization
    {
        public static IBus InitializeNServiceBus()
        {
		    var builder = new ContainerBuilder();
			builder.RegisterControllers(Assembly.GetExecutingAssembly())
                .PropertiesAutowired(); //this is needed to get property injection working
			
			// Register the sender components 
			builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                  .Where(t => t.Name.EndsWith("Sender"))
				  .AsImplementedInterfaces()
				  .AsSelf()
				  .PropertiesAutowired();
			var container = builder.Build();

			// Configure the bus
			var config = new BusConfiguration();
			config.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(container));
			
			if (Debugger.IsAttached)
            {	
				// For production use, please select a durable persistence.
				// To use RavenDB, install-package NServiceBus.RavenDB and then use configuration.UsePersistence<RavenDBPersistence>();
				// To use SQLServer, install-package NServiceBus.NHibernate and then use configuration.UsePersistence<NHibernatePersistence>();	
				config.UsePersistence<InMemoryPersistence>();
				
				// In Production, make sure the necessary queues for this endpoint are installed before running the website
				// While calling this code will create the necessary queues required, this step should
                // ideally be done just one time as opposed to every execution of this endpoint.
				config.EnableInstallers();
			}

			var bus = Bus.Create(config);
            bus.Start();            

            //tell MVC to resolve via autofac
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return bus;
        }
    }
}