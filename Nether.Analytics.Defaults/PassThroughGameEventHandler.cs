// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Nether.Analytics
{
    public class PassThroughMessageHandler : IMessageHandler
    {
        private object[] _outputManagers;

        public PassThroughMessageHandler(params object[] outputManagers)
        {
            _outputManagers = outputManagers;
        }

        public GameHandlerResult ProcessMessage(EventHubListenerMessage message)
        {
            throw new NotImplementedException();
        }
    }
}