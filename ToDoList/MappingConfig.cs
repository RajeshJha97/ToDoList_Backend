using AutoMapper;
using ToDoList.DTO;
using ToDoList.Models.Accounts;

namespace ToDoList
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<SignUp,SignUpDTO>().ReverseMap();
        }
    }
}
