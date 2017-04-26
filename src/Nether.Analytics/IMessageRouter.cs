// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Nether.Analytics
{
    public interface IMessageRouter<ParsedMessageType> where ParsedMessageType : IParsedMessage
    {
        //TODO: Make method Async
        void RouteMessage(ParsedMessageType message);
    }
}