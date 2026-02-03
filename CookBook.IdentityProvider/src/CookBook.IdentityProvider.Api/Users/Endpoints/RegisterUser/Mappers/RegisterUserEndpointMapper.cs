using CookBook.IdentityProvider.Api.Users.Endpoints.RegisterUser.Contracts;
using CookBook.IdentityProvider.Domain.Users.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.IdentityProvider.Api.Users.Endpoints.RegisterUser.Mappers;

[Mapper(
    EnumMappingStrategy = EnumMappingStrategy.ByName,
    EnumMappingIgnoreCase = true)]
internal static partial class RegisterUserEndpointMapper
{
    public static RegisterUserRequest ToModel(
        this RegisterUserRequestDto source);
}
