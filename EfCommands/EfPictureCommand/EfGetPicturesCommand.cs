using Aplication.Commands.PictureCommand;
using Aplication.Dto;
using Aplication.PageResponse;
using Aplication.Search;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfPictureCommand
{
    public class EfGetPicturesCommand : BaseEfCommand, IGetPicturesCommand

    {
       

        public EfGetPicturesCommand(DataContext context):base(context)
        {
            
        }

        public PageResponse<PictureDto> Execute(PictureSearch request)
        {
            var pictures = context.Pictures.AsQueryable();

            if (request.Alt != null)
            {
                pictures = pictures.Where(p => p.Alt.ToLower().Contains(request.Alt.ToLower()));
            }

            if (request.Path != null)
            {
                pictures = pictures.Where(p => p.Path.ToLower().Contains(request.Path.ToLower()));
            }
            var totalCount = pictures.Count();

            pictures = pictures.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);


            var response = new PageResponse<PictureDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = pictures.Select(p => new PictureDto
                {
                    Id = p.Id,
                    Alt = p.Alt,
                    Path = p.Path

                })
            };
            return response;
        }
    }
}
