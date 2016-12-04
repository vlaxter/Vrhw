using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vrhw.Repository.Sql.Entities
{
    public class Diff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Left { get; set; }
        public string Right { get; set; }
    }
}