// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Nether.Analytics.EventProcessor.Host
{
    internal class Location_1_0_Google_GameEventHandler
    {
        private NetherEventHubOutputManager _eventHubOutputManager;
        private NetherBlobOutputManager _blobOutputManager;

        public Location_1_0_Google_GameEventHandler(NetherEventHubOutputManager eventHubOutputManager, NetherBlobOutputManager blobOutputManager)
        {
            _eventHubOutputManager = eventHubOutputManager;
            _blobOutputManager = blobOutputManager;
        }
    }
}