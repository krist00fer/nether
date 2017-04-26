// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// KEEP

namespace Nether.Analytics
{
    public interface IMessageHandler<ParsedMessageType> where ParsedMessageType : IMessageType
    {
        MessageHandlerResluts ProcessMessage(ParsedMessageType message);
    }
}