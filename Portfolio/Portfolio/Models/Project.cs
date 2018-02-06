using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Environment;

namespace Portfolio.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30),Required]
        public string Name { get; set; }

        [StringLength(20), Required]
        public string Type { get; set; }

        [StringLength(200), Required]
        public string Description { get; set; }

        [Display(Name="Date Completed")]
        public DateTime DateCompleted { get; set; }

        public string ImageUrl { get; set; }
        public byte[] Image { get; set; }
        public string ImageContentType { get; set; }

        public string Technologies { get; set; }
        public IEnumerable<string> TechnologiesList => (Technologies ?? string.Empty).Split(NewLine);

        public string GetInlineImage()
        {
            if (Image == null || ImageContentType == null) return null;

            string base64Image = Convert.ToBase64String(Image);

            return $"data:{ImageContentType};base64,{base64Image}";
        }

        public void SetImage(IFormFile file)
        {
            if (file == null) return;
            ImageContentType = file.ContentType;
            
            using(MemoryStream stream = new MemoryStream())
            {
                file.CopyTo(stream);
                Image = stream.ToArray();
            }
        }
    }
}
