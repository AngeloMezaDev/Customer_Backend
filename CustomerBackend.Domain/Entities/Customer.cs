using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBackend.Domain.Entities
{
    public class Customer : BaseEntity
    {
        [Required(ErrorMessage = "El ID de la compañía es obligatorio")]
        [ForeignKey("Company")]
        public long CompanyId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 100 caracteres")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "El formato del teléfono no es válido")]
        [StringLength(20, ErrorMessage = "El teléfono no puede exceder los 20 caracteres")]
        public string Phone { get; set; }

        [StringLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres")]
        public string Address { get; set; }

        // Propiedad de navegación para EF Core
        public virtual Company Company { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
