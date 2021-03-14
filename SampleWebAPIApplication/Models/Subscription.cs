using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using SampleWebAPIApplication.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleWebAPIApplication.Models
{
    public class Subscription
    {
        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<TodoItemEntity>> OnTodoItemGet([Service] ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, TodoItemEntity>("ReturnedItems", cancellationToken);
        }

        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<TodoItemEntity>> OnTodoItemCreate([Service] ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, TodoItemEntity>("TodoItemCreated", cancellationToken);
        }
    }
}
