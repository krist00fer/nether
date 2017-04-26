﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace Nether.Analytics
{
    public abstract class GameEventHandler
    {
        public abstract GameHandlerResult ProcessMessage(GameMessage message);
    }
}