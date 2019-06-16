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
    public class EfEditPostCommand : BaseEfCommand, IEditPostCommand
    {
       

        public EfEditPostCommand(DataContext context):base(context)
        {
           
        }

        public void Execute(EditPostDto request)
        {
            var post = context.Posts.Find(request.Id);

            if (post == null) throw new EntityNotFoundException("Post");

            if (post.IsDeleted == true) throw new EntityNotFoundException("Post");


            if (request.Title != null)
            {
                if (post.Title != request.Title)
                {
                    if (context.Posts.Any(p => p.Title == request.Title)) throw new EntityAlreadyExistsException("Post");
                }
            }

            if (request.Subtitle != null)
            {
                if (post.Subtitle != request.Subtitle)
                {
                    if (context.Posts.Any(p => p.Subtitle == request.Subtitle)) throw new EntityAlreadyExistsException("Post");
                }
            }

            if (request.Description != null)
            {
                if (post.Description != request.Description)
                {
                    if (context.Posts.Any(p => p.Description == request.Description)) throw new EntityAlreadyExistsException("Post");
                }
            }

            if (request.UserId != 0)
            {
                if (post.UserId != request.UserId)
                {
                    if (!context.Users.Any(u => u.Id == request.UserId)) throw new EntityNotFoundException("User");
                }
            }

            if (request.PicturePath != null)
            {

                    if (context.Pictures.Any(p => p.Path == request.PicturePath)) throw new EntityAlreadyExistsException("Picture");





            }

            //izmena PICTURE
            var picture = new Domain.Picture
            {
                Path = request.PicturePath,
                Alt = request.Alt

            };
            context.Pictures.Add(picture);
            context.SaveChanges();//generate PictureId
            var Pictureid = picture.Id;



            //izmena POSTA
            var posts = new Domain.Post
            {
                Title = request.Title,
                Subtitle = request.Subtitle,
                Description = request.Description,
                UserId = request.UserId,
                PictureId = Pictureid,
            };
            posts.ModifiedAt = DateTime.Now;
            context.Posts.Add(post);
            context.SaveChanges();//generate PostId
            var postId = posts.Id;



            //DODAVANJE U POSTCATEGORY

            foreach (var id in request.CategoryId)
            {
                var PostCategory = new Domain.PostCategory
                {
                    PostId = postId,
                    CategoryId = id

                };

                context.Add(PostCategory);
                context.SaveChanges();
            }



        }
    }
}
