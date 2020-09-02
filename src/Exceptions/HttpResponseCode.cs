﻿namespace API.Exceptions
{
    public enum HttpResponseCode
    {
        Ok = 200,
        Created = 201,
        NoContent = 204,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        NotAcceptable = 406,
        Conflict = 409,
        UnsupportedMediaType = 415,
        ImATeapot = 418,
        InternalServerError = 500,
        NotImplemented = 501
    }
}