﻿// Copyright (c) Toni Solarin-Sodara
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Runtime.Serialization;
using Coverlet.Core.Instrumentation;

namespace Coverlet.Core
{
    // Followed safe serializer guide, will emit xml format
    // https://docs.microsoft.com/en-us/visualstudio/code-quality/ca2300-do-not-use-insecure-deserializer-binaryformatter?view=vs-2019
    // https://docs.microsoft.com/en-us/visualstudio/code-quality/ca2301-do-not-call-binaryformatter-deserialize-without-first-setting-binaryformatter-binder?view=vs-2019
    [DataContract]
    internal class CoveragePrepareResult
    {
        [DataMember]
        public string Identifier { get; set; }
        [DataMember]
        public string ModuleOrDirectory { get; set; }
        [DataMember]
        public string MergeWith { get; set; }
        [DataMember]
        public bool UseSourceLink { get; set; }
        [DataMember]
        public InstrumenterResult[] Results { get; set; }
        [DataMember]
        public CoverageParameters Parameters { get; set; }

        public static CoveragePrepareResult Deserialize(Stream serializedInstrumentState)
        {
            return (CoveragePrepareResult)new DataContractSerializer(typeof(CoveragePrepareResult)).ReadObject(serializedInstrumentState);
        }

        public static Stream Serialize(CoveragePrepareResult instrumentState)
        {
            var ms = new MemoryStream();
            new DataContractSerializer(typeof(CoveragePrepareResult)).WriteObject(ms, instrumentState);
            ms.Position = 0;
            return ms;
        }
    }
}
