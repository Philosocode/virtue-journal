using Microsoft.AspNetCore.Mvc;

namespace VirtueJournal.Shared.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static int GetCurrentUserId(this ControllerBase controller)
        {
            return int.Parse(controller.User.Identity.Name);
        }
    }
}