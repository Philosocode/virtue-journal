using System;
using VirtueApi.Entities;

namespace VirtueApi.Shared
{
    public static class VirtueMappers
    {
        /// <summary>
        /// Convert a VirtueCreateDTO to a Virtue
        /// </summary>
        /// <param name="virtueDTO">The VirtueDTO</param>
        /// <returns>New Virtue</returns>
        /// <see href="https://stackoverflow.com/questions/52000933/how-to-manually-mapping-dto-without-using-automapper">StackOverflow Link</see>
        public static Virtue VirtueFromCreateDTO(VirtueCreateDTO data)
        {
            if (data != null)
            {
                return new Virtue()
                {
                    Color = data.Color,
                    Description = data.Description,
                    Icon = data.Icon,
                    Name = data.Name,
                    CreatedAt = DateTime.Now
                };
            }

            return null;
        }

        public static Virtue VirtueFromEditDTO(Virtue oldVirtue, VirtueEditDTO updates)
        {
            if (updates != null && oldVirtue != null)
            {
                return new Virtue()
                {
                    Id = oldVirtue.Id,
                    Color = updates.Color ?? oldVirtue.Color,
                    Description = updates.Description ?? oldVirtue.Description,
                    Icon = updates.Icon ?? oldVirtue.Icon,
                    Name = updates.Name ?? oldVirtue.Name,
                    CreatedAt = updates.CreatedAt ?? oldVirtue.CreatedAt
                };
            }

            return null;
        }
    }
}