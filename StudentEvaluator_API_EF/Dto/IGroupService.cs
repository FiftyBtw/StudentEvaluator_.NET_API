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

        public Task<GroupDto?> GetGroupByIds(long id);
        public Task<GroupDto?> PostGroup(GroupDto book);

        public Task<GroupDto?> Putgroup(long id, GroupDto book);

        public Task<bool> DeleteGroup(long id);
    }
}
