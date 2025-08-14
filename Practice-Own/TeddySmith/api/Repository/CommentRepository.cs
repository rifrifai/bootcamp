using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    // repository for database call
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null) return null;

            existingComment.IsDeleted = true;
            await _context.SaveChangesAsync();
            return existingComment;

            // var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            // if (commentModel == null) return null;
            // _context.Comments.Remove(commentModel);
            // await _context.SaveChangesAsync();
            // return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            var result = await _context.Comments.Where(c => c.IsDeleted == false).ToListAsync();
            return result;
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var result = await _context.Comments.Where(e => e.IsDeleted == false).FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null) return null;

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();
            return existingComment;
        }
    }
}