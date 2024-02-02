using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System;




namespace Testktra1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<employee> employees = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem tất cả các trường dữ liệu có được nhập hay không
            if (string.IsNullOrWhiteSpace(txtId.Text) ||
                string.IsNullOrWhiteSpace(Name.Text) ||
                Date.SelectedDate == null ||
                (!NamGender.IsChecked.Value && !NuGender.IsChecked.Value) ||
                string.IsNullOrWhiteSpace(DepartmentName.Text) ||
                string.IsNullOrWhiteSpace(Salary.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // Kiểm tra tuổi của nhân viên
            DateTime selectedDate = Date.SelectedDate.Value;
            int age = DateTime.Today.Year - selectedDate.Year;
            if (selectedDate > DateTime.Today.AddYears(-age)) age--;

            if (age < 18)
            {
                MessageBox.Show("Tuổi của nhân viên phải lớn hơn hoặc bằng 18.");
                return;
            }

            // Kiểm tra hệ số lương là số thực và lớn hơn 0
            if (!decimal.TryParse(Salary.Text, out decimal salary) || salary <= 0)
            {
                MessageBox.Show("Hệ số lương phải là số thực và lớn hơn 0.");
                return;
            }

            // Nếu tất cả các điều kiện đều đúng, thêm nhân viên vào danh sách và cập nhật DataGrid
            employee employee = new()
            {
                ID = txtId.Text,
                Name = Name.Text,
                Date = Date.SelectedDate.Value,
                Gender = NamGender.IsChecked.Value ? "Nam" : "Nữ",
                DepartmentName = DepartmentName.Text,
                Salary = salary,
            };
            employees.Add(employee);
            dgvEmployeeInfo.ItemsSource = null;
            dgvEmployeeInfo.ItemsSource = employees;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Date.SelectedDate = DateTime.Today;
        }
        
        private void btnWindow2_Click(object sender, RoutedEventArgs e)
        {
            decimal maxSalary = employees.Max(em => em.Salary);
            List<employee> maxEmployees = employees.Where(emp => emp.Salary == maxSalary).ToList();
            new Window2(maxEmployees).ShowDialog();
        }
    }
}