
using Api.Email;
using Api.Helpers;
using Aplication;
using Aplication.Commands.CategoryCommand;
using Aplication.Commands.CommentCommand;
using Aplication.Commands.PictureCommand;
using Aplication.Commands.PostCommands;
using Aplication.Commands.RoleCommands;
using Aplication.Commands.UserCommands;
using Aplication.Dto;
using Aplication.Interfaces;
using EfCommands.EfCategoriesCommand;
using EfCommands.EfCommentsCommand;
using EfCommands.EfPictureCommand;
using EfCommands.EfPostsCommand;
using EfCommands.EfRolesCommand;
using EfCommands.EfUsersCommand;
using EfDataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<DataContext>();

            //roles
            services.AddTransient<IGetRolesCommand, EfGetRolesCommand>();
            services.AddTransient<IGetRoleCommand, EfGetRoleCommand>();
            services.AddTransient<IAddRoleCommand, EfAddRoleCommand>();
            services.AddTransient<IEditRoleCommand, EfEditRoleCommand>();
            services.AddTransient<IDeleteRoleCommand, EfDeleteRoleCommand>();


            //users

            services.AddTransient<IGetUsersCommand, EfGetUsersCommand>();
            services.AddTransient<IAddUserCommand, EfAddUserCommand>();
            services.AddTransient<IEditUserCommand, EfEditUserCommand>();
            services.AddTransient<IGetUserCommand, EfGetUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<ILoginUserCommand, EfLoginUserCommand>();

            //categories
            services.AddTransient<IGetCategoriesCommand, EfGetCategoriesCommand>();
            services.AddTransient<IGetCategoryCommand, EfGetCategoryCommand>();
            services.AddTransient<IAddCategoryCommand, EfAddCategoryCommand>();
            services.AddTransient<IEditCategoryCommand, EfEditCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();

            //comments
            services.AddTransient<IGetCommentsCommand, EfGetCommentsCommand>();
            services.AddTransient<IAddCommentsCommand, EfAddCommentsCommand>();
            services.AddTransient<IDeleteCommentsCommand, EfDeleteCommentsCommand>();



            //POSTS
            services.AddTransient<IGetPostsCommand, EfGetPostsCommand>();
            services.AddTransient<IGetPostCommand, EfGetPostCommand>();
            services.AddTransient<IAddPostCommand, EfAddPostCommand>();
            services.AddTransient<IDeletePostCommand, EfDeletePostCommand>();
            services.AddTransient<IEditPostCommand, EfEditPostCommand>();


            //PICTURES
            services.AddTransient<IAddPictureCommand, EfAddPictureCommand>();
            services.AddTransient<IGetPicturesCommand, EfGetPicturesCommand>();
            services.AddTransient<IGetPictureCommand, EfGetPictureCommand>();
            services.AddTransient<IEditPictureCommand, EfEditPictureCommand>();
            services.AddTransient<IDeletePictureCommand, EfDeletePictureCommand>();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();


            //EMAIL
            var section = Configuration.GetSection("Email");

            var sender =
                new SmtpEmailSender(section["host"], System.Int32.Parse(section["port"]), section["fromAddress"], section["password"]);

            services.AddSingleton<IEmailSender>(sender);

            //registration
            services.AddTransient<IRegistrateUserCommand, EfRegistrateUserCommand>();



            //Swagger



            // Register the Swagger generator, defining 1 or more Swagger documents
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                   
                    Title = "Jaffa",
                    Description = "Jaffa blog post",
                   
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            //LOGIN

            var key = Configuration.GetSection("Encryption")["key"];
            var enc = new Encryption(key);
            services.AddSingleton(enc);


           

            services.AddTransient(s =>
            {
                var http = s.GetRequiredService<IHttpContextAccessor>();
                var value = http.HttpContext.Request.Headers["Authorization"].ToString();
                var encription = s.GetRequiredService<Encryption>();


                try {
                    var decodedString = encription.DecryptString(value);
                    decodedString = decodedString.Substring(0, decodedString.LastIndexOf("}") + 1);

                    var user=  JsonConvert.DeserializeObject<LoggedUserDto>(decodedString);
                    user.IsLogged = true;
                    return user;
                }
                catch(Exception)
                {
                    return new LoggedUserDto {
                        IsLogged = false
                    };
                }
               

               
            });

            




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseStaticFiles();


            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }
    }
}
