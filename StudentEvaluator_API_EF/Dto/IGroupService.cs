using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    public interface IGroupService
    {
        public Task<PageReponseDto<GroupDto>> GetGroups(int index,int count);

        public Task<GroupDto?> GetGroupByIds(int gyear, int gnumber);
        public Task<GroupDto?> PostGroup(GroupDto book);

        public Task<GroupDto?> Putgroup(int gyear, int gnumber, GroupDto book);

        public Task<bool> DeleteGroup(int gyear, int gnumber);
    }
}
