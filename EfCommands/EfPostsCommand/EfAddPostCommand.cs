using Aplication.Commands.PostCommands;
using Aplication.Dto;
using Aplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfPostsCommand
{
    public class EfAddPostCommand : BaseEfCommand, IAddPostCommand
    {
        
        public EfAddPostCommand(DataContext context):base(context)
        {
            
        }

        public void Execute(AddPostDto request)
        {
            if (context.Posts.Any(p => p.Title == request.Title)) throw new EntityAlreadyExistsException("Post");
            if (context.Posts.Any(p => p.Subtitle == request.Subtitle)) throw new EntityAlreadyExistsException("Post");
            if (context.Posts.Any(p => p.Description == request.Description)) throw new EntityAlreadyExistsException("Post");


            if (!context.Users.Any(u => u.Id == request.UserId)) throw new EntityNotFoundException("User");
           



            //INSERT PICTURE
            var picture = new Domain.Picture
            {
                Path = request.PicturePath,
                Alt = request.Alt
            };
            context.Pictures.Add(picture);
            context.SaveChanges();//generate PictureId
            var Pictureid = picture.Id;


            //DODAVANJE POSTA
            var post = new Domain.Post
            {
                Title = request.Title,
                Subtitle = request.Subtitle,
                Description = request.Description,
                UserId = request.UserId,
                PictureId = Pictureid,       
            };
            context.Posts.Add(post);
            context.SaveChanges();//generate PostId
            var PostId = post.Id;



            //DODAVANJE U POSTCATEGORY

            foreach (var id in request.CategoryId)
            {
                var PostCategory = new Domain.PostCategory
                {
                    PostId = PostId,
                    CategoryId = id

                };
                context.Add(PostCategory);
            }
            
            context.SaveChanges();

        }
    }
}
