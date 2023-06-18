namespace InvBank.Backend.Contracts.Authentication;

public record LoginRequest
(
    string Email,
    string Password
);