﻿namespace movie_project.Entities
{
    public class Movie : BaseEntity
    {
        public int MovieDuration {  get; set; }
        public DateTime EndTime {  get; set; }
        public DateTime PremiereDate {  get; set; }
        public string Description {  get; set; }
        public string Director {  get; set; }
        public string Image {  get; set; }
        public string HeroImage {  get; set; }
        public string Language {  get; set; }
        public int MovieTypeID {  get; set; }
        public string Name {  get; set; }
        public int RateID {  get; set; }
        public string Trailer {  get; set; }
        public bool IsActive {  get; set; }
        public MovieType MovieType { get; set; }
        public Rate Rate { get; set; }
        public IQueryable<Schedule> Schedules { get; set; }
    }
}
