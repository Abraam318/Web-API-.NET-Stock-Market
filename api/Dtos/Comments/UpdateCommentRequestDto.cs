using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comments
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5,ErrorMessage ="Title must be 5 characters")]
        [MaxLength(20,ErrorMessage ="Title can't be more that 20 characters")]
        public string Title {get; set;} = String.Empty;
        
        [Required]
        [MinLength(5,ErrorMessage ="Contant must be 5 characters")]
        [MaxLength(20,ErrorMessage ="Contant can't be more that 20 characters")]
        public string Content {get; set;} = String.Empty;
    }
}