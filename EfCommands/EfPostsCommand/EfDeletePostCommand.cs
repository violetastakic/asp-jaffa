using Aplication.Commands.PostCommands;
using Aplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfPostsCommand
{
    public class EfDeletePostCommand : BaseEfCommand, IDeletePostCommand

    {
        

        public EfDeletePostCommand(DataContext context):base(context)
        {
          
        }

        public void Execute(int request)
        {
            var post = context.Posts.Find(request);
            if (post == null) throw new EntityNotFoundException("Post");
            if(post.IsDeleted == true) throw new EntityNotFoundException("Post");


            post.IsDeleted = true;
            post.UserId = null;
            post.PictureId = null;

            context.SaveChanges();
        }
    }
}
