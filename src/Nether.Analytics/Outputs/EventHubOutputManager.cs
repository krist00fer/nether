// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Nether.Analytics
{
    public class EventHubOutputManager : OutputManager
    {
        private string _outputEventHubConnectionString;

        public EventHubOutputManager(string outputEventHubConnectionString)
        {
            _outputEventHubConnectionString = outputEventHubConnectionString;
        }
    }
}