using Aplication.Commands.CommentCommand;
using Aplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfCommentsCommand
{
    public class EfDeleteCommentsCommand : BaseEfCommand, IDeleteCommentsCommand
    {
        
        public EfDeleteCommentsCommand(DataContext context):base(context)
        {
           
        }

        public void Execute(int request)
        {
            var comment = context.Comments.Find(request);

            if (comment == null) throw new EntityNotFoundException("Comment");
           if(comment.IsDeleted == true) throw new EntityNotFoundException("Comment");


            comment.IsDeleted = true;
            comment.UserId = null;
            comment.PostId = null;
        }
    }
}
