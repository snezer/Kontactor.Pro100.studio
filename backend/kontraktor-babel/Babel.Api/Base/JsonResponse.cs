using Microsoft.AspNetCore.Mvc;

namespace Babel.Api.Base
{
    public static class JsonResponse
    {
        public static JsonResult New<T>(T value)
        {
            return new JsonResult(value);
        }
    }
}
