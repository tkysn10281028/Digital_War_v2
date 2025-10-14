using DigitalWar.Project.Common.Enums;

namespace DigitalWar.Project.Explore.Domain.Status
{
    public class StatusObject
    {
        public Objects Type;

        public StatusObject(Objects type)
        {
            this.Type = type;
        }
    }
}
