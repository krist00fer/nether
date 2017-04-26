// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// KEEP

using System;
using Nether.Analytics.Parsers;

namespace Nether.Analytics
{
    public class EventHubOutputManager : IOutputManager<GenericMessage>
    {
        private string _outputEventHubConnectionString;

        public EventHubOutputManager(string outputEventHubConnectionString)
        {
            _outputEventHubConnectionString = outputEventHubConnectionString;
        }

        public void OutputMessage(GenericMessage msg)
        {
            throw new NotImplementedException();
        }
    }
}