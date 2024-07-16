using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comments;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteASync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x=>x.Id == id);
            if(comment == null)
            {
                return null;
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdASync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(s=>s.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int stockId, Comment comment)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(x=>x.Id == stockId);

            if(existingComment == null){
                return null;
            } 

            existingComment.Content = comment.Content;
            existingComment.Title = comment.Title;

            await _context.SaveChangesAsync();
            return existingComment;
        } 
    }
}