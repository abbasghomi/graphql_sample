using BackendWebApi.Entities;

namespace BackendWebApi.GraphQL;

public class ScoreType : ObjectType<Score>
{
    protected override void Configure(IObjectTypeDescriptor<Score> descriptor)
    {
        descriptor.Field(s => s.Id).Type<IdType>();
        descriptor.Field(s => s.Course).Type<StringType>();
        descriptor.Field(s => s.Value).Type<IntType>();
        descriptor.Field(s => s.UserId).Ignore();
        descriptor.Field(s => s.User).Type<UserType>();
    }
}