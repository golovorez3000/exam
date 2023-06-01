using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Books_DB_Lemetti
{
    public partial class FormBooks : Form
    {
        // Создаем объект для подключения к БД
        BookContext db = new BookContext();
        public FormBooks()
        {
            InitializeComponent();
            db.Books.Load(); // читаем таблицу Books
            // Источник данных для DataGrid - БД, таблица Books
            dataGridViewBooks.DataSource = db.Books.ToList();
            // Настраиваем свойства DataGrid - можно делать через свойства
            dataGridViewBooks.ReadOnly = true;
            dataGridViewBooks.AllowUserToAddRows = false;
            dataGridViewBooks.AllowUserToDeleteRows = false;
            dataGridViewBooks.AllowUserToOrderColumns = false;
            dataGridViewBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewBooks.MultiSelect = false;
            dataGridViewBooks.Columns[0].HeaderText = "№ п/п";
            dataGridViewBooks.Columns[0].Width = 45;
            dataGridViewBooks.Columns["Title"].HeaderText = "Название";
            dataGridViewBooks.Columns[1].Width = 200;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // номер выденной строки в Grid
            int num = dataGridViewBooks.SelectedRows[0].Index;
            // id книги, которая выделена
            int idBook = int.Parse(dataGridViewBooks[0, num].Value.ToString());
            // Найти в БД книгу по ее id
            Book book = db.Books.Find(idBook);
            if (MessageBox.Show("Вы действительно хотите удалить книгу" + book.Title + "?",
                "Подтвердите удаление",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning) != DialogResult.Yes) return;

            // удалить книгу из БД
            db.Books.Remove(book);
            // сохранить изменения
            db.SaveChanges();
            // обновить Grid
            dataGridViewBooks.DataSource = db.Books.ToList();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Создать форму-диалог
            FormAddBook addBook = new FormAddBook();
            // Показать форму и узнать чем закончился диалог
            DialogResult result = addBook.ShowDialog();
            // если не Ок - ничего делать не надо
            if (result != DialogResult.OK) return;

            // создаем новую книгу
            Book book = new Book();
            // Заполняем ее поля
            book.Title = addBook.textBoxTitle.Text;
            book.Author = addBook.textBoxAuthor.Text;
            book.Genre = addBook.textBoxGenre.Text;
            book.Pages = (int)addBook.numericUpDownPages.Value;
            book.Cost = double.Parse(addBook.textBoxCost.Text);
            // Добавить в БД
            db.Books.Add(book);
            // Сохранить изменения в БД
            db.SaveChanges();
            // Обновить Grid
            dataGridViewBooks.DataSource = db.Books.ToList();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            // номер выденной строки в Grid
            int num = dataGridViewBooks.SelectedRows[0].Index;
            // id книги, которая выделена
            int idBook = int.Parse(dataGridViewBooks[0, num].Value.ToString());
            // Найти в БД книгу по ее id
            Book book = db.Books.Find(idBook);

            // Создать форму-диалог
            FormAddBook addBook = new FormAddBook();
            // Заполнить форму данными книги
            addBook.textBoxTitle.Text = book.Title;
            addBook.textBoxAuthor.Text = book.Author;
            addBook.textBoxGenre.Text = book.Genre;
            addBook.numericUpDownPages.Value = book.Pages;
            addBook.textBoxCost.Text = book.Cost.ToString();
            // Показать форму и узнать чем закончился диалог
            DialogResult result = addBook.ShowDialog();
            // если не Ок - ничего делать не надо
            if (result != DialogResult.OK) return;

            // Заполняем ее поля
            book.Title = addBook.textBoxTitle.Text;
            book.Author = addBook.textBoxAuthor.Text;
            book.Genre = addBook.textBoxGenre.Text;
            book.Pages = (int)addBook.numericUpDownPages.Value;
            book.Cost = double.Parse(addBook.textBoxCost.Text);

            // Сохранить изменения в БД
            db.SaveChanges();
            // Обновить Grid
            dataGridViewBooks.DataSource = db.Books.ToList();
        }

        private void dataGridViewBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
