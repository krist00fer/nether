// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace Nether.Analytics
{
    public class GameEventPipeline
    {
        private GameEventHandler[] _gameEventHandlers;
        private OutputManager[] _outputManagers;

        public GameEventPipeline(string messageType, GameEventHandler[] gameEventHandlers, OutputManager[] outputManagers)
        {
            MessageType = messageType;
            _gameEventHandlers = gameEventHandlers;
            _outputManagers = outputManagers;
        }

        public void ProcessMessage(GameMessage msg)
        {
            foreach (var handler in _gameEventHandlers)
            {
                var result = handler.ProcessMessage(msg);
                if (result.StopProcessing)
                {
                    return;
                }
            }
            foreach (var outputManager in _outputManagers)
            {
                outputManager.OutputMessage(msg);
            }
        }

        public string MessageType { get; private set; }
    }
}