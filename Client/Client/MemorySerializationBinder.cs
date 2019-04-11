using System;
using System.Runtime.Serialization;

namespace Memory
{
    sealed class MemorySerializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            return Type.GetType(typeName, true);
        }
    }
}
