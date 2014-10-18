using System;
using System.Collections.Generic;
using System.Reflection;
using TelegramApi.TLCore.Extensions;

namespace TelegramApi.TLCore
{
    public abstract class TLObject
    {
        public IEnumerable<PropertyInfo> GetTLProperties()
        {
            return GetType().GetTLProperties();
        }

        public UInt32 GetClassId()
        {
            return GetType().GetClassId();
        }
    }
}