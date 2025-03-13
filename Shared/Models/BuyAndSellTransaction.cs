using Shared.Enums;
using Shared.Models.Currency;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Models
{
    public class BuyAndSellTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        //[JsonIgnore]
        //public UserEntity UserEntity { get; set; }

        public int CurrencyExchangeAccountId { get; set; }
        [JsonIgnore]
        public CustomerAccount CurrencyExchangeAccount { get; set; }
        public int BuyTransactionId { get; set; }
        [JsonIgnore]
        public CustomerTransactionHistory BuyTransaction { get; set; }

        public int SellTransactionId { get; set; }
        [JsonIgnore]
        public CustomerTransactionHistory SellTransaction { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }

        [Required]
        public int SourceCurrencyId { get; set; }
        [JsonIgnore]
        public CurrencyEntity SourceCurrencyEntity { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Rate { get; set; }

        public int TargetCurrencyId { get; set; }
        [JsonIgnore]
        public CurrencyEntity TargetCurrencyEntity { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal ConvertedAmount { get; set; }

        public TransactionType TransactionType { get; set; }

        public CurrencyBuyAndSellType BuyAndSellType { get; set; } = 0;

        public int? CustomerId { get; set; }

        [JsonIgnore]
        public CustomerAccount CustomerAccount { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        // RowVersion for concurrency control
        [Timestamp]
        public byte[] RowVersion { get; set; } = new byte[0];
    }
}
