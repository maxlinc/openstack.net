﻿using System.Linq;
using Newtonsoft.Json.Linq;

namespace net.openstack.Core.Domain.Mapping
{
    /// <summary>
    /// Provides a mapping from <see cref="JObject"/> to <see cref="Network"/>.
    /// </summary>
    /// <remarks>
    /// The base implementation is unidirectional, and does not provide a mapping from <see cref="Network"/>
    /// to <see cref="JObject"/>.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    internal class NetworkResponseJsonMapper : IJsonObjectMapper<Network>
    {
        /// <summary>
        /// A default instance of <see cref="NetworkResponseJsonMapper"/>.
        /// </summary>
        private static readonly NetworkResponseJsonMapper _default = new NetworkResponseJsonMapper();

        /// <summary>
        /// Gets a default implementation of <see cref="NetworkResponseJsonMapper"/>.
        /// </summary>
        public static NetworkResponseJsonMapper Default
        {
            get
            {
                return _default;
            }
        }

        /// <inheritdoc/>
        public Network Map(JObject @from)
        {
            if (from == null)
                return null;

            var network = from.Properties().First();
            return new Network
                {
                    Id = network.Name,
                    Addresses = network.Value.Select(o => o.ToObject<AddressDetails>()).ToArray()
                };
        }

        /// <inheritdoc/>
        public JObject Map(Network to)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public Network Map(string rawJson)
        {
            if (string.IsNullOrWhiteSpace(rawJson))
                return null;

            return Map(JObject.Parse(rawJson));
        }
    }
}
