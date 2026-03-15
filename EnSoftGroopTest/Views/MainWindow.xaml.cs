using EnSoftGroopTest.Models;
using EnSoftGroopTest.ViewModels;
using System.ComponentModel;
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

namespace EnSoftGroopTest.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
        private void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GridViewColumnHeader header && header.Column != null)
            {
                string propertyName = header.Column.Header.ToString() switch
                {
                    "Название" => nameof(TaskItem.Title),
                    "Приоритет" => nameof(TaskItem.Priority),
                    "Срок" => nameof(TaskItem.DueDate),
                    "Создана" => nameof(TaskItem.CreatedAt),
                    _ => null
                };

                if (propertyName != null && DataContext is MainViewModel vm)
                {
                    vm.TasksView.SortDescriptions.Clear();
                    vm.TasksView.SortDescriptions.Add(new SortDescription(propertyName, ListSortDirection.Ascending));
                }
            }
        }
    }
}