// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// KEEP

using System;

namespace Nether.Analytics
{
    public interface IOutputManager<ParsedMessageType> where ParsedMessageType : IKnownMessageType
    {
        void OutputMessage(ParsedMessageType msg);
    }
}