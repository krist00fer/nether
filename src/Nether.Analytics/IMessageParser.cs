﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// KEEP

using System.Threading.Tasks;

namespace Nether.Analytics.Parsers
{
    public interface IMessageParser<RawMessageFormat, ParsedMessageFormat>
    {
        Task<ParsedMessageFormat> ParseAsync(RawMessageFormat message);
    }
}