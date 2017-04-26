// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// KEEP

using System;
using Nether.Analytics.Parsers;

namespace Nether.Analytics
{
    public class EventHubOutputManager : IOutputManager<SimpleMessage>
    {
        private string _outputEventHubConnectionString;

        public EventHubOutputManager(string outputEventHubConnectionString)
        {
            _outputEventHubConnectionString = outputEventHubConnectionString;
        }

        public void OutputMessage(SimpleMessage msg)
        {
            throw new NotImplementedException();
        }
    }
}