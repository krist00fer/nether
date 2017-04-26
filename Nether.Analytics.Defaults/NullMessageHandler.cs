﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Nether.Analytics
{
    public class NullMessageHandler<T> : IMessageHandler<T> where T : IParsedMessage
    {
        private object[] _outputManagers;

        public NullMessageHandler(params object[] outputManagers)
        {
            _outputManagers = outputManagers;
        }

        public MessageHandlerResluts ProcessMessage(T message)
        {
            // Don't do anything since this is a NullMessageHandler

            return MessageHandlerResluts.Success;
        }
    }
}