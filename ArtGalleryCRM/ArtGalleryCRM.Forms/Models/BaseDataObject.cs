using System;
using CommonHelpers.Common;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Telerik.XamarinForms.Common.DataAnnotations;

namespace ArtGalleryCRM.Forms.Models
{
    public class BaseDataObject : BindableBase
    {
        public BaseDataObject()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Id for item
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        [Ignore]
        internal string Id { get; set; }

        /// <summary>
        /// Azure created at time stamp
        /// </summary>
        [JsonProperty(PropertyName = "createdAt")]
        [Ignore]
        internal DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Azure UpdateAt timestamp for online/offline sync
        /// </summary>
        [UpdatedAt]
        [Ignore]
        internal DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Azure version for online/offline sync
        /// </summary>
        [Version]
        [Ignore]
        internal string AzureVersion { get; set; }
    }
}
