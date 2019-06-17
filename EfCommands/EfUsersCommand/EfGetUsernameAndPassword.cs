using Aplication;
using Aplication.Commands.UserCommands;
using Aplication.Dto;
using Aplication.Interfaces;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfUsersCommand
{
    public class EfGetUsernameAndPassword : BaseEfCommand, GetUsernameAndPassword
    {
 
        public EfGetUsernameAndPassword(DataContext context):base(context)
        {
            this.context = context;
        }



        void ICommand<LoggedUser>.Execute(LoggedUser request)
        {
            var loggeduser = context.Users.Find(request);


        }
    }
}
