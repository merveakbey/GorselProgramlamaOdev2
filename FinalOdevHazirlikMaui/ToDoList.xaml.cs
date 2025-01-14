
using FinalOdevHazirlikMaui.Models;
using Firebase.Auth;
using Firebase.Database;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace FinalOdevHazirlikMaui;

public partial class ToDoList : ContentPage
{
	public ToDoList()
	{
		InitializeComponent();

        BL.LoadNotes();

        toDoList.ItemsSource = BL.Notes;
    }

    static FirebaseClient client = new FirebaseClient("https://finalodevi-86bd9-default-rtdb.firebaseio.com/");
  

    void AddNote(Notes note)
    {
        BL.AddNote(note);
    }

    private async void Edit_Clicked(object sender, EventArgs e)
    {
        var m = sender as Button;
        var note = BL.Notes.First(o => o.Id == m.CommandParameter.ToString());

        string result = await DisplayPromptAsync("Notu Düzenle", "Notunuzu giriniz: ", "Tamam", "İptal");

        if (result != null)
        {
            note.NoteContent = result;
            BL.EditNote(note);
        }
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        var m = sender as Button;
        var note = BL.Notes.First(o => o.Id == m.CommandParameter.ToString());

        bool b = await DisplayAlert("Silmeyi Onayla", $"{note.NoteContent} notu silinsin mi?", "Evet", "Hayır");

        if (b)
        {
            BL.DeleteNote(note);

        }

    }

  
    private async void NoteAdd_Clicked(object sender, EventArgs e)
    {
        Notes note;

        string noteContent = await DisplayPromptAsync("Not Ekle", "Notunuzu giriniz: ", "Tamam", "İptal");

        if (noteContent != null)
        {
            note = new Notes()
            {
                NoteContent = noteContent
            };

            AddNote(note);
        }
    }
}