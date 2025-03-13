using Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.Models
{
    public class CustomerAccount
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(100)]
        public string? Lastname { get; set; }

        public int AccountNumber { get; set; }

        public decimal BorrowAmount { get; set; }

        public string? PassportNumber { get; set; }

        public byte[]? Image { get; set; } = null;
        
        public string? IDCardNumber { get; set; }


        [RegularExpression("^(\\+93|0)\\d{9}$")]
        public string? Mobile { get; set; }

        [AllowedValues(nameof(eAccountType.Regular),
            nameof(eAccountType.Treasury),
            nameof(eAccountType.Incremental),
            nameof(eAccountType.CurrencyExchange),
            nameof(eAccountType.Decremental))]
        public eAccountType? AccountType { get; set; }

        // Add UserId to link the customer to a specific user
        public int UserId { get; set; }

        // Navigation property to the User class
        //[JsonIgnore]
        //public UserEntity User { get; set; }


        // RowVersion property for concurrency control
        [Timestamp]
        public byte[] RowVersion { get; set; } = new byte[0];

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? LastModifiedDate { get; set; }
        [JsonIgnore]
        public ICollection<CustomerTransactionHistory> CustomerTransactionHistories { get; set; } = new List<CustomerTransactionHistory>();
        [JsonIgnore]
        public ICollection<CustomerBalance> CustomerBalances { get; set; } = new List<CustomerBalance>();

    }


}
