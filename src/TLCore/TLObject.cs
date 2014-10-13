﻿using System.Collections.Generic;
using System.Reflection;
using TelegramApi.TLCore.Extensions;
using TelegramApi.TLCore.Serialization;

namespace TelegramApi.TLCore
{
    public abstract class TLObject
    {
        public IEnumerable<PropertyInfo> GetTLProperties()
        {
            return GetType().GetTLProperties();
        }

        public int GetClassId()
        {
            return GetType().GetClassId();
        }
    }
}