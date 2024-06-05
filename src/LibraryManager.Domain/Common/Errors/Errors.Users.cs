﻿using ErrorOr;

namespace LibraryManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Email is already in use.");

        public static Error UserNotFound => Error.NotFound(
            code: "User.NotFound",
            description: "The user was not found.");
    }
}
