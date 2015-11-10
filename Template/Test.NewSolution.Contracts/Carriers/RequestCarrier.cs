using System;
using System.Runtime.Serialization;

namespace Test.NewSolution.Contracts.Carriers
{
    /// <summary>
    /// Base request.
    /// </summary>
    [DataContract]
    public class Request
    {

    }

    /// <summary>
    /// Base request carrier.
    /// </summary>
    [DataContract]
    public class RequestCarrier
    {
    }

    /// <summary>
    /// Base request carrier.
    /// </summary>
    [DataContract]
    public class RequestCarrier<TRequest>: RequestCarrier where TRequest : Request
    {
        [DataMember(Name="request")]
        public TRequest Request {get;set;}
    }
}

