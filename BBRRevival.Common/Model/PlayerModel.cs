using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Common.Model
{
    public class PlayerModel
    {
        [Key]
        public Guid Id { get; set; } = new();

        public Guid ClientConfigID { get; set; }
    }
}
