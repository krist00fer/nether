// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Nether.Analytics
{
    public class BlobOutputManager : OutputManager
    {
        private string _outputblobStorageConnectionString;

        public BlobOutputManager(string outputblobStorageConnectionString)
        {
            _outputblobStorageConnectionString = outputblobStorageConnectionString;
        }
    }
}