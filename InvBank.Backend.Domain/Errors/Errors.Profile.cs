using ErrorOr;

namespace InvBank.Backend.Domain.Errors;

public static partial class Errors
{
    public static class Profile
    {
        public static Error ProfileNofFound => Error.NotFound(
            "Profile.ProfileNofFound",
            "Não foi encontrado nenhum perfil com o email correspodente");
    }
}