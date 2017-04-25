// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Nether.Analytics
{
    public class GameEventRouter
    {
        private Dictionary<string, GameEventPipeline> _eventPipelines;
        private GameEventPipeline _unhandledEventPipeline;

        public GameEventRouter(Dictionary<string, GameEventPipeline> eventPipelines, GameEventPipeline unhandledEventPipeline)
        {
            _eventPipelines = eventPipelines;
            _unhandledEventPipeline = unhandledEventPipeline;
        }

        public void RouteMessage(GameMessage msg)
        {
            if (_eventPipelines.TryGetValue(msg.MessageType, out var pipeline))
            {
                pipeline.ProcessMessage(msg);
            }
            else
            {
                _unhandledEventPipeline?.ProcessMessage(msg);
            }
        }
    }
}