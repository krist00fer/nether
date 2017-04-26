// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Nether.Analytics.EventProcessor.Host
{
    internal class NetherEventHubListner
    {
        private string _ingestEventHubConnectionString;

        public NetherEventHubListner(string ingestEventHubConnectionString)
        {
            _ingestEventHubConnectionString = ingestEventHubConnectionString;
        }
    }
}