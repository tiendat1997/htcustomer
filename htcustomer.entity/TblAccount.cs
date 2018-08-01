namespace htcustomer.entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblAccount")]
    public partial class TblAccount
    {
        [Key]
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(150)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
