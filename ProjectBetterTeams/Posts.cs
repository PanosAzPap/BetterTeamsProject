namespace ProjectBetterTeams
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Posts
    {

        #region Posts Table Fields

        [Key]
        [Column(Order = 0)]
        public int PostID { get; set; }

        
        [Column(Order = 1)]
        [StringLength(20)]
        public string UsernameSender { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(250)]
        public string Post { get; set; }

        public virtual Users Users { get; set; }
        #endregion 
    }
}
