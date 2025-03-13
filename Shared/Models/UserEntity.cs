using Shared.Models.Currency;
using Shared.Models.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }

        public string Email { get; set; } = string.Empty;

        public string? PictureUrl { get; set; } = string.Empty;

        public string? Role { get; set; } = string.Empty;


        // ParentUserId for sub-users (employees)
        public int? ParentUserId { get; set; }

        // Navigation property to the parent user
        [JsonIgnore]
        public UserEntity? ParentUser { get; set; }

        // Navigation property to sub-users (employees)
        [JsonIgnore]
        public ICollection<UserEntity> SubUsers { get; set; } = new List<UserEntity>();

        //// Navigation property to CustomerAccounts
        //[JsonIgnore]
        //public ICollection<CustomerAccount> CustomerAccounts { get; set; } = new List<CustomerAccount>();

        //[JsonIgnore]
        //public ICollection<CurrencyEntity> Currency { get; set; } = new List<CurrencyEntity>();
        //[JsonIgnore]
        //public ICollection<CurrencyExchangeRate> CurrencyExchangeRates { get; set; } = new List<CurrencyExchangeRate>();
        //[JsonIgnore]
        //public UserPreference UserPreference { get; set; } // Navigation property

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? LastModifiedDate { get; set; }

        public DateTime? ValidUntil { get; set; }

        public string? ConnectionString { get; set; }

        public string? ServerAddress { get; set; }

        public string? Databasename { get; set; }

    }

}
