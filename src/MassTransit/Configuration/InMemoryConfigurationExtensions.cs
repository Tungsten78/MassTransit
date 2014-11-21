﻿// Copyright 2007-2014 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit
{
    using System;
    using Configurators;
    using EndpointConfigurators;


    public static class InMemoryConfigurationExtensions
    {
        /// <summary>
        /// Select an in-memory transport for the service bus
        /// </summary>
        public static IInMemoryServiceBusFactoryConfigurator InMemory(this IServiceBusFactorySelector selector)
        {
            var configurator = new InMemoryServiceBusFactoryConfigurator(selector);

            selector.SetServiceBusFactory(configurator);

            return configurator;
        }

        /// <summary>
        /// Specify a receive endpoint for the bus, with the specified queue name
        /// </summary>
        /// <param name="configurator"></param>
        /// <param name="queueName">The queue name for the receiving endpoint</param>
        /// <param name="configure">The configuration callback</param>
        public static void ReceiveEndpoint(this IInMemoryServiceBusFactoryConfigurator configurator, string queueName,
            Action<IReceiveEndpointConfigurator> configure)
        {
            var endpointConfigurator = new InMemoryReceiveEndpointConfigurator(queueName);

            configure(endpointConfigurator);

            configurator.AddServiceBusFactoryBuilderConfigurator(endpointConfigurator);
        }
    }
}