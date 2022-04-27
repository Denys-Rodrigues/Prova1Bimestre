using System.ComponentModel.DataAnnotations;

namespace Prova1Bimestre.Models
{
    public class Cliente
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
