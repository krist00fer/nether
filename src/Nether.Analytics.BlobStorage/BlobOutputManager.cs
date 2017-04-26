﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// KEEP

using System;
using Nether.Analytics.Parsers;
using System.Threading.Tasks;

namespace Nether.Analytics
{
    public class BlobOutputManager : IOutputManager<DictionaryBasedMessage>
    {
        private string _outputblobStorageConnectionString;

        public BlobOutputManager(string outputblobStorageConnectionString)
        {
            _outputblobStorageConnectionString = outputblobStorageConnectionString;
        }

        public Task OutputMessageAsync(DictionaryBasedMessage msg)
        {
            throw new NotImplementedException();
        }
    }
}