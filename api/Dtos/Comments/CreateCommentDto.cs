using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comments
{
    public class CreateCommentDto
    {
        public string Title {get; set;} = String.Empty;
        public string Content {get; set;} = String.Empty;    }
}