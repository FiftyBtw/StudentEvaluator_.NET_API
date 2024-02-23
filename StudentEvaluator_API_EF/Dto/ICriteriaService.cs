namespace API_Dto;

public interface ICriteriaService
{
    public Task<PageReponseDto<TextCriteriaDto>> GetTextCriterions(int? index, int? count);

    public Task<TextCriteriaDto> GetTextCriterionByIds(long id);
}