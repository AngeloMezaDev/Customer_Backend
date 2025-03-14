﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBackend.Application.DTOs
{
    public class CustomerDTO
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsActive { get; set; }

        // Propiedades adicionales
        public string CompanyName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
