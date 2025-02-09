using CustomerBackend.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Company
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("company_id")]
    public long CompanyId { get; set; }

    [Required]
    [Column("company_name")]
    [StringLength(100)]
    public string CompanyName { get; set; }

    [Required]
    [Column("tax_id")]
    [StringLength(20)]
    public string TaxId { get; set; }

    [Column("address")]
    [StringLength(200)]
    public string Address { get; set; }

    [Column("phone")]
    [StringLength(20)]
    public string Phone { get; set; }

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [Column("created_date")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [Column("updated_date")]
    public DateTime? UpdatedDate { get; set; }

    [Required]
    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    public virtual ICollection<Customer> Customers { get; set; }
}