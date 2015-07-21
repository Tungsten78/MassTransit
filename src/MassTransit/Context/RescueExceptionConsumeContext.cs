﻿// Copyright 2007-2015 Chris Patterson, Dru Sellers, Travis Smith, et. al.
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
namespace MassTransit.Context
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Events;
    using Pipeline;


    public class RescueExceptionConsumeContext<TMessage> :
        ExceptionConsumeContext<TMessage>
        where TMessage : class
    {
        readonly ConsumeContext<TMessage> _context;
        readonly Exception _exception;
        ExceptionInfo _exceptionInfo;

        public RescueExceptionConsumeContext(ConsumeContext<TMessage> context, Exception exception)
        {
            _context = context;
            _exception = exception;
        }

        Exception ExceptionConsumeContext.Exception
        {
            get { return _exception; }
        }

        ExceptionInfo ExceptionConsumeContext.ExceptionInfo
        {
            get
            {
                if (_exceptionInfo != null)
                    return _exceptionInfo;

                _exceptionInfo = new FaultExceptionInfo(_exception);

                return _exceptionInfo;
            }
        }

        Task ConsumeContext.CompleteTask
        {
            get { return _context.CompleteTask; }
        }

        IEnumerable<string> ConsumeContext.SupportedMessageTypes
        {
            get { return _context.SupportedMessageTypes; }
        }

        Task IPublishEndpoint.Publish<T>(T message, CancellationToken cancellationToken)
        {
            return _context.Publish(message, cancellationToken);
        }

        Task IPublishEndpoint.Publish<T>(T message, IPipe<PublishContext<T>> publishPipe, CancellationToken cancellationToken)
        {
            return _context.Publish(message, publishPipe, cancellationToken);
        }

        Task IPublishEndpoint.Publish<T>(T message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken)
        {
            return _context.Publish(message, publishPipe, cancellationToken);
        }

        Task IPublishEndpoint.Publish(object message, CancellationToken cancellationToken)
        {
            return _context.Publish(message, cancellationToken);
        }

        Task IPublishEndpoint.Publish(object message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken)
        {
            return _context.Publish(message, publishPipe, cancellationToken);
        }

        Task IPublishEndpoint.Publish(object message, Type messageType, CancellationToken cancellationToken)
        {
            return _context.Publish(message, messageType, cancellationToken);
        }

        Task IPublishEndpoint.Publish(object message, Type messageType, IPipe<PublishContext> publishPipe,
            CancellationToken cancellationToken)
        {
            return _context.Publish(message, messageType, publishPipe, cancellationToken);
        }

        Task IPublishEndpoint.Publish<T>(object values, CancellationToken cancellationToken)
        {
            return _context.Publish<T>(values, cancellationToken);
        }

        Task IPublishEndpoint.Publish<T>(object values, IPipe<PublishContext<T>> publishPipe, CancellationToken cancellationToken)
        {
            return _context.Publish(values, publishPipe, cancellationToken);
        }

        Task IPublishEndpoint.Publish<T>(object values, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken)
        {
            return _context.Publish<T>(values, publishPipe, cancellationToken);
        }

        bool PipeContext.HasPayloadType(Type contextType)
        {
            return _context.HasPayloadType(contextType);
        }

        bool PipeContext.TryGetPayload<TPayload>(out TPayload payload)
        {
            return _context.TryGetPayload(out payload);
        }

        TPayload PipeContext.GetOrAddPayload<TPayload>(PayloadFactory<TPayload> payloadFactory)
        {
            return _context.GetOrAddPayload(payloadFactory);
        }

        Guid? MessageContext.MessageId
        {
            get { return _context.MessageId; }
        }

        Guid? MessageContext.RequestId
        {
            get { return _context.RequestId; }
        }

        Guid? MessageContext.CorrelationId
        {
            get { return _context.CorrelationId; }
        }

        DateTime? MessageContext.ExpirationTime
        {
            get { return _context.ExpirationTime; }
        }

        Uri MessageContext.SourceAddress
        {
            get { return _context.SourceAddress; }
        }

        Uri MessageContext.DestinationAddress
        {
            get { return _context.DestinationAddress; }
        }

        Uri MessageContext.ResponseAddress
        {
            get { return _context.ResponseAddress; }
        }

        Uri MessageContext.FaultAddress
        {
            get { return _context.FaultAddress; }
        }

        Headers MessageContext.Headers
        {
            get { return _context.Headers; }
        }

        CancellationToken PipeContext.CancellationToken
        {
            get { return _context.CancellationToken; }
        }

        ReceiveContext ConsumeContext.ReceiveContext
        {
            get { return _context.ReceiveContext; }
        }

        bool ConsumeContext.HasMessageType(Type messageType)
        {
            return _context.HasMessageType(messageType);
        }

        bool ConsumeContext.TryGetMessage<T>(out ConsumeContext<T> consumeContext)
        {
            return _context.TryGetMessage(out consumeContext);
        }

        Task ConsumeContext.RespondAsync<T>(T message)
        {
            return _context.RespondAsync(message);
        }

        Task ConsumeContext.RespondAsync<T>(T message, IPipe<SendContext<T>> sendPipe)
        {
            return _context.RespondAsync(message, sendPipe);
        }

        void ConsumeContext.Respond<T>(T message)
        {
            _context.Respond(message);
        }

        Task<ISendEndpoint> ISendEndpointProvider.GetSendEndpoint(Uri address)
        {
            return _context.GetSendEndpoint(address);
        }

        Task ConsumeContext.NotifyConsumed<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType)
        {
            return _context.NotifyConsumed(context, duration, consumerType);
        }

        Task ConsumeContext.NotifyFaulted<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception)
        {
            return _context.NotifyFaulted(context, duration, consumerType, exception);
        }

        TMessage ConsumeContext<TMessage>.Message
        {
            get { return _context.Message; }
        }

        Task ConsumeContext<TMessage>.NotifyConsumed(TimeSpan duration, string consumerType)
        {
            return _context.NotifyConsumed(duration, consumerType);
        }

        Task ConsumeContext<TMessage>.NotifyFaulted(TimeSpan duration, string consumerType, Exception exception)
        {
            return _context.NotifyFaulted(this, duration, consumerType, exception);
        }

        ConnectHandle IPublishObserverConnector.ConnectPublishObserver(IPublishObserver observer)
        {
            return _context.ConnectPublishObserver(observer);
        }
    }
}