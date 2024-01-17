namespace Appointments.Features.Appointments.Domain;

public enum AppointmentState
{
    Scheduled, // Created automatically by the system most likely.
    Booked,
    CancelledByVenue,
    CancelledByUser,
    Completed,
    NoShow, // User didn't show up without cancelling
}
