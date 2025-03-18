using API.DTOs;
using API.Model;
using System.Collections.Generic;

/// <summary>
/// The <c>IAppointmentLogic</c> interface defines the business logic operations for handling appointments in the system.
/// It includes methods to manage appointment data, such as retrieving, updating, inserting, and deleting appointments.
/// </summary>
/// <remarks>
/// The methods interact with DTOs (Data Transfer Objects) like <c>ReadAppointmentDTO</c>, 
/// <c>CreateAppointmentDTO</c>, and the <c>Appointment</c> model.
/// </remarks>

namespace API.BusinessLogicLayer
{
    public interface IAppointmentLogic
    {
        public List<Appointment> GetAppointments();
        public bool InsertAppointment(Appointment appointment, int donorId);
        public bool DeleteAppointment(Appointment appointment);
        Appointment GetAppointmentById(int id);
        public List<Appointment> GetAppointmentsByDonorId(int donorId);
        public bool DeleteAppointmentByStartTime(DateTime startTime);
    }
}
