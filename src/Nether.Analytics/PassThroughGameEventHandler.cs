// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Nether.Analytics
{
    public class PassThroughGameEventHandler
    {
        private object[] _outputManagers;

        public PassThroughGameEventHandler(params object[] outputManagers)
        {
            _outputManagers = outputManagers;
        }
    }
}