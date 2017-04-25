// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;

namespace Nether.Analytics
{
    public class GameEventPipelineBuilder
    {
        private string _eventName;
        private List<GameEventHandler> _handlers = new List<GameEventHandler>();
        private OutputManager[] _outputManagers;

        public GameEventPipelineBuilder(string eventName)
        {
            _eventName = eventName;
        }

        public GameEventPipelineBuilder AddHandler(GameEventHandler eventHandler)
        {
            _handlers.Add(eventHandler);

            return this;
        }

        public GameEventPipeline Build(List<GameEventHandler> globalHandlers)
        {
            return new GameEventPipeline(_eventName, globalHandlers.Concat(_handlers).ToArray(), _outputManagers);
        }

        public void OutputTo(params OutputManager[] outputManagers)
        {
            _outputManagers = outputManagers;
        }
    }
}