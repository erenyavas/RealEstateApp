using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.DTOs
{
    public class DynamicFeatureDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string DataType { get; set; } // int, string, selectbox

        public int? MinValue { get; set; } // int t�r�ndeki �zellikler i�in

        public int? MaxValue { get; set; } // int t�r�ndeki �zellikler i�in

        public List<string>? Options { get; set; } // selectbox t�r�ndeki �zellikler i�in se�enekler
    }
}
