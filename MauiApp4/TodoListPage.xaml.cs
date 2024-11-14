using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using Microsoft.Maui.Controls;

namespace MauiApp4
{
    public partial class ToDoListPage : ContentPage
    {
        private const string FileName = "todo.json";
        private readonly string filePath;
        public ObservableCollection<ToDoItem> ToDoItems { get; set; }

        public ToDoListPage()
        {
            InitializeComponent();
            filePath = Path.Combine(FileSystem.AppDataDirectory, FileName);
            ToDoItems = LoadToDoItems();
            ToDoCollectionView.ItemsSource = ToDoItems;
        }

        private ObservableCollection<ToDoItem> LoadToDoItems()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<ObservableCollection<ToDoItem>>(json) ?? new ObservableCollection<ToDoItem>();
            }
            return new ObservableCollection<ToDoItem>();
        }

        private void SaveToDoItems()
        {
            var json = JsonSerializer.Serialize(ToDoItems);
            File.WriteAllText(filePath, json);
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Yeni Todo", "Yeni Todo Ekle:");
            if (!string.IsNullOrWhiteSpace(result))
            {
                ToDoItems.Add(new ToDoItem { Task = result });
            }
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            SaveToDoItems();
            DisplayAlert("Kaydedildi", "Todo baþarýyla kaydedildi.", "OK");
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton deleteButton && deleteButton.BindingContext is ToDoItem item)
            {
                ToDoItems.Remove(item);
            }
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton editButton && editButton.BindingContext is ToDoItem item)
            {
                string result = await DisplayPromptAsync("Todo'yu Güncelle", "Todo'yu güncelle:", initialValue: item.Task);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    item.Task = result;
                }
            }
        }
    }

    public class ToDoItem : INotifyPropertyChanged
    {
        private string task;
        public string Task
        {
            get => task;
            set
            {
                if (task != value)
                {
                    task = value;
                    OnPropertyChanged(nameof(Task));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
