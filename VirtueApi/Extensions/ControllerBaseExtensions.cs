using Microsoft.AspNetCore.Mvc;

namespace VirtueApi.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static int GetCurrentUserId(this ControllerBase controller)
        {
            return int.Parse(controller.User.Identity.Name);
        }
    }
}