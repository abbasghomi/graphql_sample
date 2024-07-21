using BackendWebApi.Entities;

namespace BackendWebApi.GraphQL;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(u => u.Id).Type<IdType>();
        descriptor.Field(u => u.Name).Type<StringType>();
        descriptor.Field(u => u.Email).Type<StringType>();
        descriptor.Field(u => u.DisplayName).Type<StringType>();
        descriptor.Field(u => u.Scores).Type<ListType<ScoreType>>();
    }
}