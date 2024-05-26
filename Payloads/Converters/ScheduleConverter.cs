using FluentEmail.Core;
using movie_project.Entities;
using movie_project.Payloads.DTOs;

namespace movie_project.Payloads.Converters
{
    public class ScheduleConverter
    {
        //public ScheduleDTO ToDTO(Schedule schedule)
        //{
        //    return new ScheduleDTO
        //    {
        //        EndAt = schedule.EndAt,
        //        IsActive = schedule.IsActive,
        //        RoomID = schedule.RoomID,
        //        StartAt = schedule.StartAt
        //    };
        //}

        public IQueryable<ScheduleDTO> ToScheduleDTOList(IQueryable<Schedule> schedules)
        {
            return schedules.Select(s => new ScheduleDTO
            {
                EndAt = s.EndAt,
                IsActive = s.IsActive,
                RoomID = s.RoomID,
                StartAt = s.StartAt
            });
        }
    }
}
