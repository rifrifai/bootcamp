using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace api.Validators
{
    public class CommentValidator : Profile
    {
        public CommentValidator()
        {
            CreateMap<Dtos.Comment.CreateCommentDto, Models.Comment>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));

            CreateMap<Dtos.Comment.UpdateCommentRequestDto, Models.Comment>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));
        }


        public bool ValidateCreateComment(Dtos.Comment.CreateCommentDto commentDto)
        {
            if (commentDto == null) return false;
            if (string.IsNullOrWhiteSpace(commentDto.Content)) return false;
            // if (commentDto <= 0) return false;

            return true;
        }

        public bool ValidateUpdateComment(Dtos.Comment.UpdateCommentRequestDto updateDto)
        {
            if (updateDto == null) return false;
            if (string.IsNullOrWhiteSpace(updateDto.Content)) return false;

            return true;
        }
    }
}