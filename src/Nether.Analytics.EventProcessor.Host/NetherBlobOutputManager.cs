// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Nether.Analytics.EventProcessor.Host
{
    internal class NetherBlobOutputManager : OutputManager
    {
        private string _outputblobStorageConnectionString;

        public NetherBlobOutputManager(string outputblobStorageConnectionString)
        {
            _outputblobStorageConnectionString = outputblobStorageConnectionString;
        }
    }
}