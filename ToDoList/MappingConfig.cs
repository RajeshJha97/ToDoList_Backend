using AutoMapper;
using ToDoList.Models.Accounts;
using ToDoList.Models.DTO;
using ToDoList.Models.TaskList;

namespace ToDoList
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<SignUp,SignUpDTO>().ReverseMap();
            CreateMap<TaskCreateDTO,TaskList>().ReverseMap();
            CreateMap<TaskUpdateDTO, TaskList>().ReverseMap();
        }
    }
}
