﻿using LibraryManager.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        return Ok(request);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        return Ok(request);
    }
}