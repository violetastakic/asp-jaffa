using EfDataAccess;


namespace EfCommands
{
    public abstract class BaseEfCommand
    {
        protected DataContext context;

        public BaseEfCommand(DataContext context)
        {
            this.context = context;
        }
    }
}
