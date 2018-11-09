namespace ProjectBetterTeams
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Messages
    {
        [Key]
        [Column(Order = 0)]
        public int MessageID { get; set; }

        
        [Column(Order = 1)]
        [StringLength(20)]
        public string UsernameSender { get; set; }

        [Required]
        [StringLength(20)]
        public string Receiver { get; set; }

        [Required]
        [StringLength(250)]
        public string Message { get; set; }

        public DateTime DateTime { get; set; }

        public virtual Users Users { get; set; }
    }
}
