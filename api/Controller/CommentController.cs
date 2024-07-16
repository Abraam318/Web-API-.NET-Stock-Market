using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Comments;
using api.Interfaces;
using api.Mapper;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [Route("api/[controller]")] 
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var comment = await _commentRepo.GetAllCommentsAsync();
            var commentDto = comment.Select(s=>s.TocommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id){
            var comment = await _commentRepo.GetByIdASync(id);
            if(comment == null){
                return NotFound();
            }
            return Ok(comment.TocommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute]int stockId,[FromBody]CreateCommentDto commentDto){
            if(!await _stockRepo.StockExist(stockId)){
                return BadRequest("Stock doesn't exist");
            }

            var commentModel = commentDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id},commentModel.TocommentDto());
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> Update([FromRoute]int commentId,[FromBody] UpdateCommentRequestDto updateDto)
        {
            var comment = await _commentRepo.UpdateAsync(commentId, updateDto.ToCommentFromUpdateDto());
            if(comment == null){
                return NotFound("Comment not found");
            }
            return Ok(comment.TocommentDto());
        }

        [HttpDelete("{CommentId}")]
        public async Task<IActionResult> Delete([FromRoute]int CommentId)
        {
            var commentModel = await _commentRepo.DeleteASync(CommentId);
            if(commentModel==null)
            {
                NotFound("Comment not found");
            }
            return Ok(commentModel);
        }
    }
}