using Aplication.Commands.CommentCommand;
using Aplication.Dto;
using Aplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfCommentsCommand
{
   public class EfAddCommentsCommand:BaseEfCommand,IAddCommentsCommand
    {
        

        public EfAddCommentsCommand(DataContext context):base(context)
        {
        }

        public void Execute(CommentDto request)
        {

            if (!context.Users.Any(u => u.Id == request.UserId)) throw new EntityNotFoundException("User");
            if(!context.Posts.Any(p => p.Id == request.PostId)) throw new EntityNotFoundException("Post");


            context.Comments.Add(new Domain.Comment
            {
                Content = request.Content,
                UserId = request.UserId,
                PostId = request.PostId
            });
            context.SaveChanges();
        }
    }
}
