using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Comments;
using api.Models;

namespace api.Mapper
{
    public static class CommentMapper
    {
        public static CommentDto TocommentDto(this Comment commentModel)
        {
            return new CommentDto{
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn =commentModel.CreatedOn.Date,
                StockId = commentModel.StockId
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentDto commentModel,int stockId)
        {
            return new Comment{
                Title = commentModel.Title,
                Content = commentModel.Content,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdateDto(this UpdateCommentRequestDto commentModel)
        {
            return new Comment{
                Title = commentModel.Title,
                Content = commentModel.Content
            };
        }
    }
}