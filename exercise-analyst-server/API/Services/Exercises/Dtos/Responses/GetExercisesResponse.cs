using System;
using System.Collections.Generic;

namespace API.Services.Exercises.Dtos.Responses
{
    public class GetExercisesResponse
    {
        public ICollection<ExerciseForGetExercises> Exercises { get; set; }
    }

    public class ExerciseForGetExercises
    {
        public int Id { get; set; }
        public string Activity { get; set; }
        public int Repetitions { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
    }
}
