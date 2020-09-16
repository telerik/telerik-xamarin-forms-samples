using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.SchedulingUICustomizationExample
{
    public class SchedulingUICustomizationViewModel : ExampleViewModel
    {
        private static DateTime CurrentDate = DateTime.Today;
        private bool isInternalChange;
        private List<CustomAppointment> allAppointments;
        private HashSet<int> locations = new HashSet<int>();
        private bool isRoom301Checked;
        private bool isRoom302Checked;
        private bool isRoom201Checked;

        public SchedulingUICustomizationViewModel()
        {
            List<OfficeRoom> defaultLocations = SourceProvider.GetLocations();
            this.allAppointments = new List<CustomAppointment>()
            {
                new CustomAppointment()
                {
                    Title = "ComboBox API Discussion",
                    StartDate = CurrentDate.AddHours(15),
                    EndDate = CurrentDate.AddHours(16),
                    Priority = Priority.VeryHigh,
                    Location = defaultLocations[0],
                    Alert = Alert.ThirtyMinutes
                },

                new CustomAppointment()
                {
                    Title = "Retrospective Meeting",
                    StartDate = CurrentDate.AddHours(15),
                    EndDate = CurrentDate.AddHours(16),
                    Priority = Priority.Low,
                    Location = defaultLocations[1]
                },

                new CustomAppointment()
                {
                    Title = "Pdf Viewer Discussion",
                    StartDate = CurrentDate.AddHours(16),
                    EndDate = CurrentDate.AddHours(17),
                    Priority = Priority.High,
                    Location = defaultLocations[2]
                }
            };

            foreach (CustomAppointment appointment in this.allAppointments)
            {
                appointment.PropertyChanged += this.Appointment_PropertyChanged;
            }

            this.IsRoom201Checked = true;
            this.IsRoom301Checked = true;
            this.IsRoom302Checked = true;

            this.AppointmentsSource.CollectionChanged += this.AppointmentsSource_CollectionChanged;
        }

        public ObservableCollection<CustomAppointment> AppointmentsSource { get; set; } = new ObservableCollection<CustomAppointment>();

        public bool IsRoom301Checked
        {
            get
            {
                return this.isRoom301Checked;
            }
            set
            {
                if (this.isRoom301Checked != value)
                {
                    this.isRoom301Checked = value;
                    this.OnPropertyChanged();

                    if (this.isRoom301Checked)
                    {
                        this.locations.Add(301);
                    }
                    else
                    {
                        this.locations.Remove(301);
                    }

                    this.UpdateVisibleAppointments();
                }
            }
        }

        public bool IsRoom302Checked
        {
            get
            {
                return this.isRoom302Checked;
            }
            set
            {
                if (value != this.isRoom302Checked)
                {
                    this.isRoom302Checked = value;
                    this.OnPropertyChanged();

                    if (this.isRoom302Checked)
                    {
                        this.locations.Add(302);
                    }
                    else
                    {
                        this.locations.Remove(302);
                    }

                    this.UpdateVisibleAppointments();
                }
            }
        }

        public bool IsRoom201Checked
        {
            get
            {
                return this.isRoom201Checked;
            }
            set
            {
                if (value != this.isRoom201Checked)
                {
                    this.isRoom201Checked = value;
                    this.OnPropertyChanged();

                    if (this.IsRoom201Checked)
                    {
                        this.locations.Add(201);
                    }
                    else
                    {
                        this.locations.Remove(201);
                    }

                    this.UpdateVisibleAppointments();
                }
            }
        }

        private void UpdateVisibleAppointments()
        {
            this.isInternalChange = true;

            this.AppointmentsSource.Clear();
            var newAppointments = this.allAppointments.Where(x => locations.Contains(x.Location.Number));

            foreach (var appointment in newAppointments)
            {
                this.AppointmentsSource.Add(appointment);
            }

            this.isInternalChange = false;
        }

        private void AppointmentsSource_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (this.isInternalChange)
            {
                return;
            }

            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    var newAppointment = (CustomAppointment)e.NewItems[0];
                    newAppointment.PropertyChanged += this.Appointment_PropertyChanged;
                    this.allAppointments.Add(newAppointment);
                    if (!locations.Contains(newAppointment.Location.Number))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            this.isInternalChange = true;
                            this.AppointmentsSource.Remove(newAppointment);
                            this.isInternalChange = false;
                        });
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    var removedAppointment = (CustomAppointment)e.OldItems[0];
                    this.allAppointments.Remove(removedAppointment);
                    removedAppointment.PropertyChanged -= this.Appointment_PropertyChanged;
                    break;
                default:
                    break;
            }
        }

        private void Appointment_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Location")
            {
                this.UpdateVisibleAppointments();
            }
        }
    }
}
