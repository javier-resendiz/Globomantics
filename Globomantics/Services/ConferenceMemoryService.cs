using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Globomantics.Services
{
    public class ConferenceMemoryService:IConferenceService 
    {
        private readonly List<ConferenceModel> conferences = new List<ConferenceModel>();
        public ConferenceMemoryService()
        {
            conferences.Add(new ConferenceModel { Id=1, Name="PluralSight", Location = "NY" });
            conferences.Add(new ConferenceModel { Id = 2, Name = "Geeks Coding", Location = "CA" });
        }

        public Task Add(ConferenceModel model)
        {
            model.Id = conferences.Max(c => c.Id) + 1;
            conferences.Add(model);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<ConferenceModel>> GetAll()
        {
            return Task.Run(() => conferences.AsEnumerable());
        }

        public Task<ConferenceModel> GetById(int id)
        {
            return Task.Run(() => conferences.First( c => c.Id==id));
        }

        public Task<StatisticsModel> GetStatistics()
        {
            return Task.Run(()=>
            {
                return new StatisticsModel
                {
                    NumberOfAttendees = conferences.Sum(c => c.AttendeeTotal),
                    AverageConferenceAttendees = (int)conferences.Average(c => c.AttendeeTotal)
                };
            });
        }
    }
}
