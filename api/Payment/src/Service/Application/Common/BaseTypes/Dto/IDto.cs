namespace Application.Common.BaseTypes.Dto;

public interface IDto : IDto<string>
{
}

public interface IDto<TId> : IBaseDto
    where TId : class
{
    TId Id { get; set; }
    DateTime CreatedDateTime { get; set; }
    DateTime UpdatedDateTime { get; set; }
}