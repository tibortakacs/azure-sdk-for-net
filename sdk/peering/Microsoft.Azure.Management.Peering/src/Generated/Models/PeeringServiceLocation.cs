// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.Peering.Models
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// The peering service location.
    /// </summary>
    [Rest.Serialization.JsonTransformation]
    public partial class PeeringServiceLocation : Resource
    {
        /// <summary>
        /// Initializes a new instance of the PeeringServiceLocation class.
        /// </summary>
        public PeeringServiceLocation()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the PeeringServiceLocation class.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <param name="id">The ID of the resource.</param>
        /// <param name="type">The type of the resource.</param>
        /// <param name="country">Country of the customer</param>
        /// <param name="state">State of the customer</param>
        /// <param name="azureRegion">Azure region for the location</param>
        public PeeringServiceLocation(string name = default(string), string id = default(string), string type = default(string), string country = default(string), string state = default(string), string azureRegion = default(string))
            : base(name, id, type)
        {
            Country = country;
            State = state;
            AzureRegion = azureRegion;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets country of the customer
        /// </summary>
        [JsonProperty(PropertyName = "properties.country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets state of the customer
        /// </summary>
        [JsonProperty(PropertyName = "properties.state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets azure region for the location
        /// </summary>
        [JsonProperty(PropertyName = "properties.azureRegion")]
        public string AzureRegion { get; set; }

    }
}
