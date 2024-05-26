﻿using movie_project.Payloads.DTOs;
using movie_project.Payloads.Requests.ScheduleRequests;

namespace movie_project.Payloads.Requests.MovieRequests
{
    public class AddMovieRequest
    {
        public int MovieDuration { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime PremiereDate { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string Image { get; set; }
        public string HeroImage { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Trailer { get; set; }
        public IQueryable<AddScheduleRequest> ScheduleRequests { get; set; }
    }
}
