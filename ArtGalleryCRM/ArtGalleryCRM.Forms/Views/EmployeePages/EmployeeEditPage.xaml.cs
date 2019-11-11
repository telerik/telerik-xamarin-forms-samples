using ArtGalleryCRM.Forms.Interfaces;
using ArtGalleryCRM.Forms.Models;
using Telerik.XamarinForms.Input;

namespace ArtGalleryCRM.Forms.Views.EmployeePages
{
    public partial class EmployeeEditPage : IDataFormView
    {
        public EmployeeEditPage()
        {
            this.InitializeComponent();

            this.ViewModel.DataFormView = this;
            this.ViewModel.IsNewEmployee = true;
            this.ViewModel.SelectedEmployee = new Employee();
            this.ViewModel.DataFormView.ConfigureDataFormEditors();

            this.DataForm.Source = this.ViewModel.SelectedEmployee;
        }

        public EmployeeEditPage(Employee employee)
        {
            this.InitializeComponent();

            this.ViewModel.DataFormView = this;
            this.ViewModel.SelectedEmployee = employee;
            this.ViewModel.DataFormView.ConfigureDataFormEditors();

            this.DataForm.Source = this.ViewModel.SelectedEmployee;
        }

        public void ConfigureDataFormEditors()
        {
            this.DataForm.RegisterEditor(nameof(Employee.Name), EditorType.TextEditor);
            this.DataForm.RegisterEditor(nameof(Employee.OfficeLocation), EditorType.TextEditor);
            this.DataForm.RegisterEditor(nameof(Employee.HireDate), EditorType.DateEditor);
            this.DataForm.RegisterEditor(nameof(Employee.Salary), EditorType.DecimalEditor);
            this.DataForm.RegisterEditor(nameof(Employee.VacationBalance), EditorType.IntegerEditor);
            this.DataForm.RegisterEditor(nameof(Employee.VacationUsed), EditorType.IntegerEditor);
            this.DataForm.RegisterEditor(nameof(Employee.Notes), EditorType.TextEditor);
        }

        public void CommitChanges()
        {
            this.DataForm.CommitAll();
        }
    }
}