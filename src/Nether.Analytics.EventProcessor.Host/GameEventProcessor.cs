// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Nether.Analytics.EventProcessor.Host
{
    internal class GameEventProcessor
    {
        public GameEventProcessor()
        {
        }

        public NetherEventHubListner GameEventListner { get; internal set; }
        public NetherEventParser GameEventParser { get; internal set; }
        public NetherEventRouter GameEventRouter { get; internal set; }

        internal void ProcessAndBlock()
        {
            throw new NotImplementedException();
        }
    }
}